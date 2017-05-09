using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using GM.Orm;
using GM.Orm.Db;
using GM.Service.InterfaceServerControl;
using GM.Utilities;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class SchoolRoomService : EntityService<EntityKey, SchoolRoom, SchoolRoomService> {

        private string _providerid;
        private string _playUrlPort;
        private readonly object _lockObject = new object();
        private string cacheLrsIp = string.Empty;
        bool _blInit;
        public  void Init()
        {
            if (_blInit)
                return;

            _playUrlPort = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playUrlPort.txt");
            if (!File.Exists(_playUrlPort))
                File.WriteAllText(_playUrlPort, "50000");

        }
    
        public override SchoolRoom Create(SchoolRoom entity)
        {
            var module = new SchoolRoom();

            try
            {
                var numPool = NumberPoolService.Instance.GetNumberPoolByNum(entity.SchoolRoomNum);
                if (numPool.IsAllot)
                    throw new Exception(string.Format("教室号:{0} 已经分配到其他教室，不能重复分配", entity.SchoolRoomNum));

                numPool.IsAllot = true;
                module = base.Create(entity);

                bool flag = SettingConfig.Instance.IsCretePhysicalChannel;
                if (flag)
                {
                    var channel = CreatePhysicalChannel(string.Format("{0}[{1}]", module.SchoolRoomName, module.Id));
                    if (channel.ResultCode < 0)
                        throw new Exception(channel.Desc);

                    var domainname = SettingConfig.Instance.DomainName;
                    var providerId = GetProviderbyCmsid(domainname);

                    foreach (var playurl in channel.PalyUrls)
                    {
                        Uri ui = new Uri(playurl.Replace(domainname, providerId).Replace(".flv", "").Replace(".mp4", ""));

                        module.PlayStreamUrl += string.Format("http://{0}/locallive{1}", ui.Authority, ui.AbsolutePath);

                        if (channel.PalyUrls.Length > 1)
                            module.PlayStreamUrl += ",";
                    }

                    module.PushStreamUrl = channel.StreamPushUrl.Substring(0, channel.StreamPushUrl.IndexOf('?'));
                    module.PhysicalChannelOuterId = string.Format("{0}/{1}", channel.ProviderID, channel.ChannelName);

                    (module as IEntity).Update();
                }

                NumberPoolService.Instance.Update(numPool);
                return module;
            }
            catch (Exception ex)
            {
                if (module.Id > 0)
                {
                    var ens = Get(new EntityKey() {Id = module.Id});
                    Delete(ens);
                }

                throw ex;
            }
        }

        public override void Update(SchoolRoom entity)
        {
            var numPool = NumberPoolService.Instance.GetNumberPoolByNum(entity.SchoolRoomNum);
            numPool.IsAllot = true;
            NumberPoolService.Instance.Update(numPool);

            base.Update(entity);
        }

        public SchoolRoom GetByImei(string imei)
        {
            var ens = new Entities<SchoolRoom>("school_room_imei='{0}'", imei).Cache;
            if (ens.Count > 0)
            {
                ens[0].Token = Guid.NewGuid().ToString();
                base.Update(ens[0]);

                return ens[0];
            }

            return  null;
        }

        public SchoolRoom GetByImeiForPcClient(string imei) {
            var ens = new Entities<SchoolRoom>("school_room_imei='{0}'", imei).Cache;
            if (ens.Count > 0) {
                return ens[0];
            }

            return null;
        }

        public SchoolRoom GetByToken(string token)
        {
            var ens = new Entities<SchoolRoom>("token='{0}'", token).Cache;
            return ens.Count>0?ens[0]:null;
        }

        public SchoolRoom GetByNubeNumber(string nubeNumber)
        {
            var ens = new Entities<SchoolRoom>("school_roomnum='{0}'", nubeNumber).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public PagingQueryEntityResult<SchoolRoom> GetList(List<string> orderFields, bool orderFlag, int startIndex,int pageSize)
        {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<SchoolRoom>(context).PagingCache;
        }

        public override void Delete(SchoolRoom entity)
        {
            try
            {
                var numPool = NumberPoolService.Instance.GetNumberPoolByNum(entity.SchoolRoomNum);
                numPool.IsAllot = false;

                if (!string.IsNullOrEmpty(entity.PhysicalChannelOuterId))
                {
                    PhysicalChannelManager.Instance.DeleteLiveChannels(new LiveChannelDeleteReplyParam()
                    {
                        DomainName = SettingConfig.Instance.DomainName,
                        ChannelNames = new[] { entity.PhysicalChannelOuterId.Split('/')[1] }
                    });
                }

                using (DbTranscationScope scope = new DbTranscationScope())
                {
                    var recordList = RecordFileService.Instance.GetByClassroomId(entity.Id);
                    recordList.ForEach(r =>
                    {
                        RecordFileService.Instance.Delete(r);
                        Logger.WriteInfoFmt(LogCatalogs.OperationHits, "教室[classroomId:{0}]删除,同时删除教室录制文件{1}", entity.Id,r.Id);
                    });

                    base.Delete(entity);
                    NumberPoolService.Instance.Update(numPool);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public int GetIdByName(string name) {

            var entity = GetAll().Find(ss => ss.SchoolRoomName == name);
            if (entity != null)
                return entity.Id;
            return 0;
        }

        public List<SchoolRoom> GetRooms(string ids) 
        {
            if (string.IsNullOrEmpty(ids))
                return null;

            return new Entities<SchoolRoom>("id in ({0})",ids).Cache;
        }

        public string GetRoomNames(string ids) 
        {
            var cache = GetRooms(ids);
            if (cache == null)
                return "";

            return string.Join(",",cache.ConvertAll<string>(x => x.SchoolRoomName).ToArray());
        }

        /// <summary>
        /// 创建物理频道
        /// </summary>
        /// <param name="schoolRoomId">学校主键id</param>
        /// <param name="playUrlPort">物理频道 rtmp 播放url端口</param>
        LiveChannelReply CreatePhysicalChannel(string schoolRoomId)
        {
            Init();

            int playurlport = GetPortByTxtInfo();
            try {
                var liveParams = new LiveChannelCreateReplyParam() {
                    DomainName = SettingConfig.Instance.DomainName,
                    ChannelName = schoolRoomId,
                    StreamOutputTypes = GetTypes(SettingConfig.Instance.StreamOutputTypes),
                    RecordFileTypes = GetTypes(SettingConfig.Instance.RecordFileTypes),
                    RecordFileSaveDays = 7,
                    Bitrate = SettingConfig.Instance.Bitrate,
                    LiveType = 3,
                    UseTcp = 0,
                    NeddAudio = 1,
                    ServerIp =GetIesIp(), //SettingConfig.Instance.LrsServerIp,//TODO 中继Ip从数据库获取
                    ServerPort = playurlport
                };

                return PhysicalChannelManager.Instance.CreatePhysicalChannel(liveParams);
            } catch (Exception ex) {
                throw ex;
            } finally {
                SetProtFromTextInfo(playurlport);
            }
        }


        string GetIesIp()
        {
            if (string.IsNullOrEmpty(cacheLrsIp))
            {
                string sql = "SELECT outer_ip FROM equipment WHERE type_id=2";
                var ds = DB.DBManager.GetDs(null, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return cacheLrsIp=ds.Tables[0].Rows[0][0].ToString();
            }
            return cacheLrsIp;
        }

        private string GetProviderbyCmsid(string domainName) {

            if (string.IsNullOrEmpty(_providerid))
            {
                var ens = new Entities<MediaService>("playback_url_prefix='{0}'", domainName).Cache;

                return ens.Count != 0 ? ens[0].ProviderID : null;
            }
            else
            {
                return _providerid;
            }
        }

        string[] GetTypes(string types)
        {
            return types.Split(',');
        }

        private int GetPortByTxtInfo() {
            lock (_lockObject)
            {
                int idx = int.Parse(File.ReadAllText(_playUrlPort));
                return idx;
            }
        }

        private void SetProtFromTextInfo(int port) {
            lock (_lockObject) {
                port++;
                File.WriteAllText(_playUrlPort, port.ToString());
            }
        }
    }
}

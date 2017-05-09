using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GM.Business.Module;
using GM.Orm;
using GM.Utilities;

namespace GM.Service.InterfaceServerControl
{
    class ConnectEquipmentChecker<T> where T : Equipment, new()
    {
        List<T> _canCheckedEquipments = new List<T>();
        Thread _thread;

        object _lock = new object();
        object _lockobject = new object();
        const int _sleepInterval = 3000;
        private int currentEq = 0;

        public void Start()
        {
            if (_thread != null)
                return;

            _thread = new Thread(ThreadFunc);
            _thread.IsBackground = true;
            _thread.Start();
            Logger.WriteInfoFmt(LogCatalogs.OperationHits, "启动可用{0}服务器查询线程", typeof(T).Name);
        }

        public void Stop()
        {
            if (_thread != null)
            {
                try
                {
                    _thread.Abort();
                }
                finally
                {
                    _thread = null;
                    Logger.WriteInfoFmt(LogCatalogs.OperationHits, "停止服务器状态查询线程");
                }
            }
        }

        public List<T> CanCheckedEquipments
        {
            get
            {
                return new List<T>(_canCheckedEquipments);
            }
        }

        public T GetOneCanCheckEquipment()
        {
            var eqlist = CanCheckedEquipments[currentEq];
            lock (_lockobject)
            {
                if (currentEq == CanCheckedEquipments.Count - 1)
                    currentEq = 0;
                else
                    currentEq++;
            }

            return eqlist;
        }


        void ThreadFunc()
        {
            while (true)
            {
                try
                {
                    _canCheckedEquipments = GetCanCheckedEquipments();
                    Thread.Sleep(_sleepInterval);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "检测空闲服务器发送异常");
                    Thread.Sleep(_sleepInterval);
                }
            }
        }

        List<T> GetCanCheckedEquipments()
        {
            List<T> cache = new List<T>();
            try
            {
                cache = new Entities<T>("type_id={0}", new T().TypeID).Cache;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "从数据库获取{0}类型服务器失败", typeof(T).Name);
                throw;
            }

            return cache.FindAll(gw =>
            {
                try
                {
                    gw.IEquipmentControl.CheckConnect();
                    return true;
                }
                catch { return false; }
            });
        }
    }

    public class GwPublishPointControl
    {
        static ConnectEquipmentChecker<LiveStreamSpliteServer> _connectedLiveStreamSpliteServerChecker = new ConnectEquipmentChecker<LiveStreamSpliteServer>();

        public static void Start()
        {
            _connectedLiveStreamSpliteServerChecker.Start();
        }

        public static void Stop()
        {
            _connectedLiveStreamSpliteServerChecker.Stop();
        }

        public static string CurrentAddRsEq
        {
            get;
            set;
        }

        public static void CreateChannelResouce(string contentid,string serverIp,int serverPort)
        {
            try
            {
                CreateLiveStreamServerResource(contentid, serverIp, serverPort);
            }
            catch (Exception ex)
            {
                Logger.WriteWarningFmt(LogCatalogs.OperationHits, ex, "创建频道分发点出现异常");
                throw ex;
            }
        }

        public static void RemoveChannel(string contentid)
        {
            RemoveResourceFromService<LiveStreamSpliteServer>(contentid);
        }

        public static void StartRecord(StartRecordParam param)
        {
            try
            {
                var matchedServer = LiveStreamSpliteServer(param.ServerIp);

                (matchedServer.IEquipmentControl as ILiveStreamSpliteServerControl).StartRecordFile(new RecordFileOrder()
                {
                    LiveChannelPid=param.LiveChannelPid,
                    LiveChannelCid=param.LiveChannelCid,
                    RecordFilePid=param.RecordFilePid,
                    RecordFileCid=param.RecordFileCid,
                    Context=param.Context
                });
            }
            catch (Exception ex)
            {
                Logger.WriteWarningFmt(LogCatalogs.OperationHits, ex, "启动录制频道失败,[livepid:{0},livecid:{1}]", param.LiveChannelPid, param.LiveChannelCid);
                throw;
            }
        }

        public static void StopRecord(StopRecordParam param)
        {
            try
            {
                var matchedServer = LiveStreamSpliteServer(param.ServerIp);
                (matchedServer.IEquipmentControl as ILiveStreamSpliteServerControl).StopRecord(param.LivechannelPid, param.LiveChannelCid);
            }
            catch (Exception ex)
            {
                Logger.WriteWarningFmt(LogCatalogs.OperationHits, ex, "停止录制频道失败,[livepid:{0},livecid:{1}]", param.LivechannelPid, param.LiveChannelCid);
                throw;
            }
        }

        private static LiveStreamSpliteServer LiveStreamSpliteServer(string serverIp)
        {
            var matchedServer = _connectedLiveStreamSpliteServerChecker.CanCheckedEquipments.Find(server => server.IPAddress == serverIp);
            if (matchedServer == null)
                throw new KeyNotFoundException(string.Format("未找到ServerIp:{0}对应的中继服务器", serverIp));

            return matchedServer;
        }

        public static void RestartChannel(string contentid)
        {
            try
            {
                var resources = GetResources<LiveStreamSpliteServer>(contentid);
                LiveStreamSpliteServer lss=null;

                foreach (var resource in resources)
                {
                    try
                    {
                          lss= GetLssServerControl();
                        (lss.IEquipmentControl as ILiveStreamSpliteServerControl).StopResource(resource.ResourceConfig);
                        (lss.IEquipmentControl as ILiveStreamSpliteServerControl).StartResource(resource.ResourceConfig);

                        Logger.WriteInfoFmt(LogCatalogs.OperationHits, "重启物理频道[Id:{0}]在中继服务器[Id:{1}  Ip:{2}]上的发布点",
                            resource.ChannelID, lss.Id, lss.IPAddress);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "重启中继服务器[Id:{0} Ip:{1}]上物理频道[Id:{2}]发布点失败",
                            lss.Id, lss.IPAddress, resource.ChannelID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "重启中继服务器发布点失败");
                throw new CdnProcessException(CdnProcessResultCode.数据库错误);
            }
        }

        public static void StartChannel(string contentid)
        {
            try
            {
                var resources = GetResources<LiveStreamSpliteServer>(contentid);
                LiveStreamSpliteServer lss = null;

                foreach (var resource in resources)
                {
                    try
                    {
                        lss = GetLssServerControl();
                        (lss.IEquipmentControl as ILiveStreamSpliteServerControl).StartResource(resource.ResourceConfig);

                        Logger.WriteInfoFmt(LogCatalogs.OperationHits, "启动物理频道[Id:{0}]在中继服务器[Id:{1}  Ip:{2}]上的发布点",
                            resource.ChannelID, lss.Id, lss.IPAddress);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "启动中继服务器[Id:{0} Ip:{1}]上物理频道[Id:{2}]发布点失败",
                            lss.Id, lss.IPAddress, resource.ChannelID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "启动中继服务器发布点失败");
                throw new CdnProcessException(CdnProcessResultCode.数据库错误);
            }
        }

        /// <summary>
        /// 获取直播中继服务器，当前平台只有一个直播中继
        /// </summary>
        /// <returns></returns>
        public static LiveStreamSpliteServer GetLssServerControl()
        {
            var cdd = new Entities<LiveStreamSpliteServer>("type_id = {0}", new LiveStreamSpliteServer().TypeID).Cache;
            return cdd.Count > 0 ? cdd[0] : null;
        }

        //private static void CreateLiveStreamServerResource(string contentid)
        //{
        //    var channel = GetPhysicalChannelByoutId(contentid);
        //    var liveStreamServers = _connectedLiveStreamSpliteServerChecker.CanCheckedEquipments;

        //    //如果某个LiveStreamServer已经有相应的直播频道资源，则不在此LiveStreamServer上添加直播频道信息
        //    FilterEquipmentWhoHasResources(GetResources<LiveStreamSpliteServer>(contentid), liveStreamServers);

        //    foreach (var liveStream in liveStreamServers)
        //    {
        //        try
        //        {
        //            CreateBindLiveStreamResource(liveStream, channel);
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "向中继服务器[Name:{0} Id:{1} Ip:{2}添加物理频道[Name:{3} opid:{4} Id:{5}]失败", liveStream.Name,liveStream.Id, liveStream.IPAddress, channel.ChannelName, channel.Opid, channel.Id);
        //        }
        //    }
        //}

        private static void CreateLiveStreamServerResource(string contentid,string serverIp,int serverPort)
        {
            var channel = GetPhysicalChannelByoutId(contentid);
            var matchedServer =
                _connectedLiveStreamSpliteServerChecker.CanCheckedEquipments.Find(
                    server => server.IPAddress == serverIp);

            if (matchedServer == null)
                throw new KeyNotFoundException(string.Format("未找到ServerIp:{0}、Port:{1}对应的中继服务器", serverIp, serverPort));

            try
            {

                CreateBindLiveStreamResource(matchedServer, channel);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "向中继服务器[Name:{0} Id:{1} Ip:{2}添加物理频道[Name:{3} opid:{4} Id:{5}]失败", matchedServer.Name, matchedServer.Id, matchedServer.IPAddress, channel.ChannelName, channel.Opid, channel.Id);
                throw;
            }
        }


        private static void FilterEquipmentWhoHasResources<T>(List<LiveStreamSpliteServerResource> allchannelresources,
            List<T> idlegws) where T : Equipment
        {
            foreach (var resource in allchannelresources)
            {
                for (int i = idlegws.Count - 1; i > -1; i--)
                {
                    if (resource.EquipmentID == idlegws[i].Id)
                        idlegws.RemoveAt(i);
                }
            }
        }

        public static void UpdateChannel(string contentid, ChannelRecordParam recordParam)
        {
            try
            {
                var resources = GetResources<LiveStreamSpliteServer>(contentid);
                Equipment ls = null;

                foreach (var resource in resources)
                {
                    try
                    {
 
                        //由于中继服务器不支持实时修改，需要先将频道停止，完成修改后再启动
                        var control = ls.IEquipmentControl as ILiveStreamSpliteServerControl;
                        //control.StopResource(resource.ResourceConfig);

                        resource.ResourceConfig.RecordConfig.EnableTs = recordParam.TimeShift;
                        resource.ResourceConfig.RecordConfig.TimeShiftRange = recordParam.TimeShiftDuration * 60;//网管下发参数中单位为分钟
                        resource.ResourceConfig.RecordConfig.EnableTvod = recordParam.TvodStatus;
                        resource.ResourceConfig.RecordConfig.DWBackSeeTime = recordParam.StorageDuration;

                        (resource as IEntity).Update();
                        control.UpdateResource(resource.ResourceConfig);
                        //control.StartResource(resource.ResourceConfig);

                        Logger.WriteInfoFmt(LogCatalogs.OperationHits, "更新物理频道[Id:{0}]在中继服务器[Id:{1}  Ip:{2}]上的发布点的录制参数",
                                            resource.ChannelID, ls.Id, ls.IPAddress);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新中继服务器[Id:{0} Ip:{1}]上物理频道[Id:{2}]发布点的录制参数失败", ls.Id, ls.IPAddress, resource.ChannelID);
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新中继服务器发布点录制参数失败");
                throw new CdnProcessException(CdnProcessResultCode.系统内部错误);
            }
        }


        private static void CreateBindLiveStreamResource(Equipment livestreamsvr, PhysicalChannel physicalchannel)
        {
            bool isRecord = true;
            var bindStorageSvr = FindRecordStorageServer(livestreamsvr, physicalchannel);
            //如果没有找到合适的存储设备，新添加的物理频道不许要设置录制标记
            if (bindStorageSvr == null)
                isRecord = false;

            LiveStreamSpliteServerResource resource = new LiveStreamSpliteServerResource();
            resource.EquipmentID = livestreamsvr.Id;
            resource.Status = LssResourceStatus.Published;
            LiveStreamSpliteServerResourceConfig channelConfig = new LiveStreamSpliteServerResourceConfig();
            channelConfig.ChannelId = physicalchannel.Id;
            channelConfig.ChannelName = physicalchannel.ChannelName;
            channelConfig.ChannelOpid = physicalchannel.ChannelOpid;
            channelConfig.IsAutoRun = true;
            channelConfig.MaxConnNum = 6000;
            channelConfig.ResourceId = physicalchannel.ChannelOpid + ",1," + physicalchannel.Id;
            channelConfig.StreamType = StreamType.IPTVRtsp;

            channelConfig.RecordConfig = new LiveStreamRecordConfig();
            channelConfig.RecordConfig.DWBackSeeTime = 5;
            channelConfig.RecordConfig.DwSlicetime = 5;
            channelConfig.RecordConfig.EnableTs = isRecord;
            channelConfig.RecordConfig.EnableTvod = isRecord;
            channelConfig.NasId = isRecord ? bindStorageSvr.Id : 0;
            channelConfig.ProviderId = GetProviderId(physicalchannel.OuterId);
            channelConfig.StoragePath = isRecord
                ? (bindStorageSvr.ServiceConfig as StorageServerConfig).StorageBasePath
                : "";
            channelConfig.RecordConfig.TimeShiftRange = 3600;
            string contentId = physicalchannel.OuterId;
            channelConfig.PhysicalChannelContentId = contentId.Substring(contentId.IndexOf("/") + 1, contentId.Length - contentId.IndexOf("/") - 1);
            resource.ResourceConfig = channelConfig;

            livestreamsvr.AddResource(resource);

            if (!isRecord)
                Logger.WriteWarningFmt(LogCatalogs.OperationHits,
                    "在中继服务器[Id:{0} Ip:{1} Name:{2}]添加物理频道[Opid:{3} Id:{4} Name:{5} OuterId:{6}]。由于没有合适的存储设备，不会设置物理频道录制属性",
                    livestreamsvr.Id, livestreamsvr.IPAddress, livestreamsvr.Name, physicalchannel.Opid,
                    physicalchannel.Id, physicalchannel.ChannelName, physicalchannel.OuterId);
            else
                Logger.WriteWarningFmt(LogCatalogs.OperationHits,
                    "在中继服务器[Id:{0} Ip:{1} Name:{2}]添加物理频道[Opid:{3} Id:{4} Name:{5} OuterId:{6}]，并启用录制。设置录制存储设备为:[Id:{7} Ip:{8} Name:{9}]",
                    livestreamsvr.Id, livestreamsvr.IPAddress, livestreamsvr.Name, physicalchannel.Opid,
                    physicalchannel.Id, physicalchannel.ChannelName, physicalchannel.OuterId,
                    bindStorageSvr.Id, bindStorageSvr.IPAddress, bindStorageSvr.Name);
        }

        private static StorageServer FindRecordStorageServer(Equipment livestreamsvr, PhysicalChannel physicalchannel)
        {
            //查找和中继服务器在同一台物理主机上的存储设备
            var storageSvrs =
                new Entities<StorageServer>("type_id={0} and device_id='{1}'", new StorageServer().TypeID,
                    livestreamsvr.Deviceid).Cache;
            if (storageSvrs.Count == 0)
            {
                Logger.WriteWarningFmt(LogCatalogs.OperationHits,
                    "在尝试在中继服务器[Id:{0} Ip:{1} Name:{2}]创建物理频道[Opid:{3} Id:{4} Name:{5} OuterId:{6}]时，无法在中继服务器所在主机找到DeviceId[{7}]相同的存储服务器。",
                    livestreamsvr.Id, livestreamsvr.IPAddress, livestreamsvr.Name, physicalchannel.Opid,
                    physicalchannel.Id, physicalchannel.ChannelName, physicalchannel.OuterId,
                    livestreamsvr.Deviceid);

                return null;
            }

            //获取已经添加该频道录制的存储设备
            var existResources =
                new Entities<LiveStreamSpliteServerResource>(
                    "content_opid='{0}' and content_id={1} and storage_id in ({2})", physicalchannel.Opid,
                    physicalchannel.Id,
                    string.Join(",", storageSvrs.ConvertAll<string>(svr => { return svr.Id.ToString(); }).ToArray()))
                    .Cache;
            var notRecordCurrentPhysicalChannelStorageSvr = storageSvrs.Find(svr =>
            {
                //返回没有和该物理频道录制绑定的本机存储设备
                return !existResources.Exists(resource => { return resource.StorageID == svr.Id; });
            });

            if (notRecordCurrentPhysicalChannelStorageSvr == null)
                Logger.WriteWarningFmt(LogCatalogs.OperationHits,
                    "与中继服务器[Id:{0} Name:{1} Ip:{2}]在同一台物理主机上的所有存储设备都已作为物理频道[Opid:{3} Id:{4} Name:{5} OuterId:{6}]的录制源。",
                    livestreamsvr.Id, livestreamsvr.Name, livestreamsvr.IPAddress, physicalchannel.Opid,
                    physicalchannel.Id, physicalchannel.ChannelName, physicalchannel.OuterId);

            return notRecordCurrentPhysicalChannelStorageSvr;
        }

        private static string GetProviderId(string outerid)
        {
            return outerid.Split('/')[0];
        }

        private static List<LiveStreamSpliteServerResource> GetResources<T>(string physicalchanneloutid)
            where T : Equipment, new()
        {
            PhysicalChannel channel = GetPhysicalChannelByoutId(physicalchanneloutid);

            return
                new Entities<LiveStreamSpliteServerResource>("content_id={0} and content_opid='{1}'", channel.Id,
                    channel.Opid).Cache;
        }

        private static List<int> GetLiveStreamEquipmentIds<T>() where T : Equipment, new()
        {
            List<T> list = new Entities<T>("type_id={0}", new T().TypeID).Cache;
            return list.ConvertAll<int>(gw => { return gw.Id; });
        }

        private static string BuildEquipmentIds(List<int> equipmentids)
        {
            StringBuilder ids = new StringBuilder();
            foreach (int gwid in equipmentids)
            {
                ids.Append(gwid.ToString());
                ids.Append(",");
            }
            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);

            return ids.ToString();
        }

        private static PhysicalChannel GetPhysicalChannelByoutId(string outid)
        {
            var entities = new Entities<PhysicalChannel>("outer_id='{0}'", outid);
            return GetEntity(entities, string.Format("从PhysicalChannel表获取outer_id={0}的记录失败", outid)) as PhysicalChannel;
        }

        private static void RemoveResourceFromService<T>(string outerid) where T : Equipment, new()
        {
            try
            {
                var resources = GetResources<T>(outerid);
                Equipment ls = null;
                foreach (var resource in resources)
                {
                    try
                    {
                        ls = new Entities<T>("id={0}", resource.EquipmentID).Cache[0];
                        ls.RemoveResource(resource);
                        Logger.WriteWarningFmt(LogCatalogs.OperationHits, "删除物理频道[Id:{0}]在中继服务器[Id:{1} Ip:{2}]上的发布点",
                            resource.ChannelID, ls.Id, ls.IPAddress);
                    }
                    catch (RemotingException ex)
                    {
                        Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex,
                            "从中继服务器[Id:{0} Ip:{1} Name:{2}]删除物理频道[Id:{3}]的发布点失败。无法连接远程中继服务器", ls.Id, ls.IPAddress,
                            ls.Name, resource.ChannelID);
                        throw new Exception("无法连接远程中继服务器");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex,
                            "从中继服务器[Id:{0} Ip:{1} Name:{2}]删除物理频道[Id:{3}]的发布点失败。系统异常", ls.Id, ls.IPAddress, ls.Name,
                            resource.ChannelID);
                        throw new CdnProcessException(CdnProcessResultCode.系统内部错误);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "删除中继服务器发布点失败");
                throw new Exception("删除中继服务器发布点失败");
            }
        }

        public static T GetEntity<T>(Entities<T> entities, string errmsg) where T : Entity
        {
            try
            {
                if (entities.Cache.Count > 0)
                    return entities.Cache[0];
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "查询数据库发生异常");
            }

            Logger.WriteErrorFmt(LogCatalogs.OperationHits, errmsg);
            throw new Exception("对象不存在或无效");
        }
    }

    public class ChannelRecordParam
    {
        public ChannelRecordParam(string tvodStatus, string storageDuration, string timeShift, string timeShiftDuration, int domainId)
        {
            TvodStatus = tvodStatus == "1";
            StorageDuration = string.IsNullOrEmpty(storageDuration) ? 0 : uint.Parse(storageDuration);
            TimeShift = timeShift == "1";
            TimeShiftDuration = string.IsNullOrEmpty(timeShiftDuration) ? 0 : uint.Parse(timeShiftDuration);
            InnerDomain = domainId;
        }

        public bool TvodStatus { get; private set; }
        public uint StorageDuration { get; private set; }
        public bool TimeShift { get; private set; }
        public uint TimeShiftDuration { get; private set; }
        public int InnerDomain { get; set; }
    }
}

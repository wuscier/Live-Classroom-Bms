using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using GM.Business.Module;
using GM.Orm;
using GM.Services;
using GM.Utilities;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    public class PhysicalChannelManager
    {
        private readonly  static PhysicalChannelManager _instance=new PhysicalChannelManager();

        public static PhysicalChannelManager Instance{get { return _instance; }}

        private List<WrapperFlag> _contentFormatFlagCache;
        private List<WrapperFlag> _protocolTypeFlagCache; 
        private readonly object _lockObject = new object();
        private string _rrUrl;
        private string _playUrlPort;
        private string iesIpPort = string.Empty;


        private PhysicalChannelManager()
        {
            _contentFormatFlagCache = new List<WrapperFlag>();
            _protocolTypeFlagCache = new List<WrapperFlag>();

            InitContentFormat();
         // InitDefaultPlayUrlPort();

            FillFlagCache<ProtocolTypeFlag>(_protocolTypeFlagCache);
        }

        #region 私有方法

        private void InitContentFormat()
        {
            _rrUrl = GetIesIpAndMediaPort(); //InterfaceServiceConfig.Instance.RRIPPort;

            var mappings = new Entities<ContentFormatMapping>().Cache;
            mappings.ForEach(mapping =>
            {
                Logger.WriteInfoFmt(LogCatalogs.OperationHits, "source:{0},value:{1}", mapping.Format.ToLower(), int.Parse(mapping.Value));
                _contentFormatFlagCache.Add(new WrapperFlag() { Source = mapping.Format.ToLower() == "hls-ts" ? "hls" : mapping.Format.ToLower(), IntValue = int.Parse(mapping.Value) });
            });
        }

        string GetIesIpAndMediaPort()
        {
            if (string.IsNullOrEmpty(iesIpPort))
            {
                var eqs = new Entities<IESServer>("type_id={0}", new IESServer().TypeID).Cache;
                if (eqs.Count > 0)
                {
                    var config = eqs[0].ServiceConfig as IESServerConfig;
                    iesIpPort = string.Format("http://{0}:{1}", eqs[0].OuterIpAddress, config.MediaHttpPort);
                }
            }
            return iesIpPort;
        }

        private void InitDefaultPlayUrlPort()
        {
            _playUrlPort = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playUrlPort.txt");
            if(!File.Exists(_playUrlPort))
                 File.WriteAllText(_playUrlPort, "50000");
        }

        private void FillFlagCache<T>(List<WrapperFlag> array)
        {
            if (array.Count == 0)
            {
                var arrayEnums = Enum.GetValues(typeof(T)) as T[];

                foreach (var item in arrayEnums)
                    array.Add(new WrapperFlag() { Source = item, IntValue = (int)Enum.Parse(typeof(T), item.ToString()) });

                array.Sort((x, y) => { return x.IntValue - y.IntValue; });
            }
        }

        private string GetOuterId(string pid,string cid)
        {
            return string.Format("{0}/{1}",pid,cid);
        }

        private string GetProviderbyCmsid(string domainName)
        {
            var ens = new Entities<MediaService>("playback_url_prefix='{0}'", domainName).Cache;

            return ens.Count != 0 ? ens[0].ProviderID : null;
        }

        BitRateType GetBitRate(int bitrate)
        {
            BitRateType bitRateType = BitRateType.OneDotThreeM;
            switch (bitrate)
            {
                case 3:
                    bitRateType = BitRateType.OneDotThreeM;
                    break;
                case 4:
                    bitRateType = BitRateType.TwoM;
                    break;
                case 5:
                    bitRateType = BitRateType.TwoDotFiveM;
                    break;
                case 6:
                    bitRateType = BitRateType.EightM;
                    break;
                case 7:
                    bitRateType = BitRateType.TenM;
                    break;

            }

            return bitRateType;
        }

        int GetContentFormatValue(string[] recordFileType)
        {
            int value = 0;

            var list = new List<string>(recordFileType);
            foreach (WrapperFlag flag in _contentFormatFlagCache)
            {
                string type = list.Find(s => s.ToLower() == flag.Source.ToString().ToLower());
                if (!string.IsNullOrEmpty(type))
                    value = value | flag.IntValue;
            }
            return value;
        }

        int GetProtocolTypeValue(string[] streamOutputTypes)
        {
            int value = 0;

            var list = new List<string>(streamOutputTypes);
            foreach (WrapperFlag flag in _protocolTypeFlagCache)
            {
                string type = list.Find(s => s.ToLower() == flag.Source.ToString().ToLower());
                if (!string.IsNullOrEmpty(type))
                    value = value | flag.IntValue;
            }
            return value;
        }


        string CreateCid()
        {
            var cid = Guid.NewGuid().ToString().Split('-');
            return cid[4];
        }

        private PhysicalChannel ConstructChannelProperty(LiveChannelCreateReplyParam requestParam)
        {
            var pid=GetProviderbyCmsid(requestParam.DomainName);
            if(string.IsNullOrEmpty(pid))
                throw new Exception("域名在CDN系统不存在");

            var outerId = GetOuterId(pid, CreateCid());
            var domain = GetProtocolTypeValue(requestParam.StreamOutputTypes);
            var channel = new PhysicalChannel()
            {
                Opid = "ss01",
                ChannelOpid = "ss01",
                StreamType = StreamType.IPTVRtsp,
                ChannelName = requestParam.ChannelName,
                OuterId = outerId,
                TimeShiftFlag = 1,
                TSTVLengthNumber = 7200,
                RecordContentFormat = GetContentFormatValue(requestParam.RecordFileTypes),
                UnicastUrl = GetPushUrlByLiveType(requestParam),
                Domain = domain
            };
            channel.StreamProperty = new StreamProperty()
            {
                AudioType = AudioType.AAC,
                BitRateType =BitRateType.OneDotThreeM,
                Resolution = Resolution.D1,
                SystemLayer = SystemLayer.TS,
                VideoProfile = VideoProfile.Main,
                VideoType = VideoType.AVS,
            };

            return channel;
        }

        double GetChannelBiteRate(string bitate)
        {
            return TranslateBitRate(bitate);
        }

        double TranslateBitRate(string bitrate)
        {
            if (string.IsNullOrEmpty(bitrate) || bitrate=="0")
                return 0;
            else if (bitrate == "1")
                return 400;
            else if (bitrate == "2")
                return 700;
            else if (bitrate == "3")//1.3M
                return Math.Ceiling(1.3 * (double)1024);
            else if (bitrate == "4")//2M
                return Math.Ceiling(2 * (double)1024);
            else if (bitrate == "5")//2.5M
                return Math.Ceiling(2.5 * (double)1024);
            else if (bitrate == "6")//8M
                return Math.Ceiling(8 * (double)1024);
            else if (bitrate == "7")//10M
                return Math.Ceiling(10 * (double)1024);
            else if (bitrate == "8")//4M
                return Math.Ceiling(4 * (double)1024);

            throw new ArgumentOutOfRangeException("无法根据bitrate:" + bitrate + " 还原对应的直播流码率");
        }

        string[] ResultPlayUrls(LiveChannelCreateReplyParam requestParam, string outerId)
        {
            var list = new List<string>();
            foreach (string stype in requestParam.StreamOutputTypes)
            {
                string prefix = string.Format("{0}/{1}", requestParam.DomainName, outerId.Split('/')[1]);

                //TODO _rrUrl 替换从数据库获取Ies 配置iesip 加 时移端口 格式 http://127.0.0.1:20418
                list.Add(string.Format("{0}/{1}.{2}", _rrUrl, prefix, stype.ToLower() == "hls" ? "m3u8" : stype.ToLower()));
            }
            
            return list.ToArray();
        }

        private int GetPortByTxtInfo()
        {
            lock (_lockObject)
            {
                int idx = int.Parse(File.ReadAllText(_playUrlPort));
                return idx;
            }
        }

        private void SetProtFromTextInfo(int port)
        {
            lock (_lockObject)
            {
                port++;
                File.WriteAllText(_playUrlPort,port.ToString());
            }
        }


        string GetPushUrlByLiveType(LiveChannelCreateReplyParam requestParam)
        {
            string pushurl = string.Empty;
            switch (requestParam.LiveType)
            {
                case 2:
                  pushurl=GetRFCRTSPPushUrl(requestParam);
                    break;
                case 3:
                    pushurl=GetStreamPushUrl(requestParam);
                    break;
                case 16:
                    pushurl=GetIpcStreamPushUrl(requestParam);
                    break;
                default:
                    throw new Exception(string.Format("不支持livetype:{0}类型",requestParam.LiveType));
            }

            return pushurl;
        }

        /// <summary>
        /// 获取用于PC直播注入
        /// </summary>
        /// <param name="bitrate"></param>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        string GetStreamPushUrl(LiveChannelCreateReplyParam requestParam)
        {
           // int port = GetPortByTxtInfo();
            var playUrl = string.Format("rtmp://{0}:{1}/live/{2}?Bitrate={4}&livetype={3}", requestParam.ServerIp, requestParam.ServerPort, requestParam.ChannelName, requestParam.LiveType, requestParam.Bitrate);
          //  SetProtFromTextInfo(port);

            return playUrl;
        }

        /// <summary>
        /// 获取用于摄像头SDK直播注入推流地址
        /// </summary>
        /// <param name="bitrate"></param>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        string GetIpcStreamPushUrl(LiveChannelCreateReplyParam requestParam)
        {
            if(requestParam.CameraSetting==null||string.IsNullOrEmpty(requestParam.CameraSetting.CameraIp))
                throw new Exception("摄像头信息不完整");

          //  int port = GetPortByTxtInfo();
            var playUrl =
                string.Format(
                    "rtmpipc://{0}:{1}/live/{2}?Bitrate={3}&livetype={4}&pareframe=1&cip={5}&cport={6}&cname={7}&cpwd={8}&bestvideo={9}&needaudio={10}&ctype={11}",
                    requestParam.ServerIp, requestParam.ServerPort, requestParam.ChannelName, requestParam.Bitrate,requestParam.LiveType, requestParam.CameraSetting.CameraIp,
                    requestParam.CameraSetting.CameraPort, requestParam.CameraSetting.CameraName,
                    requestParam.CameraSetting.CameraPwd, requestParam.CameraSetting.BestVideo, requestParam.NeddAudio, (int)requestParam.CameraSetting.CameraType);
          //  SetProtFromTextInfo(port);

            return playUrl;
        }

        string GetRFCRTSPPushUrl(LiveChannelCreateReplyParam requestParam)
        {
            if (requestParam.CameraSetting == null || string.IsNullOrEmpty(requestParam.CameraSetting.CameraIp))
                throw new Exception("未提供摄像头ip");

            var playUrl = string.Format("rtsp://{0}:{1}/{2}?livetype={3}&usetcp={4}&needaudio={5}", requestParam.ServerIp, requestParam.ServerPort, requestParam.ChannelName, requestParam.LiveType, requestParam.UseTcp, requestParam.NeddAudio);
            return playUrl;
        }

        #endregion

        public LiveChannelReply CreatePhysicalChannel(LiveChannelCreateReplyParam requestParam)
        {
            var reply = new LiveChannelReply(){ResultCode = 0,Desc = "创建直播频道成功"};

            try
            {
                using (DbTranscationScope scope = new DbTranscationScope())
                {
                    var channel = new Entities<PhysicalChannel>().Add(ConstructChannelProperty(requestParam));
                    GwPublishPointControl.CreateChannelResouce(channel.OuterId, requestParam.ServerIp,requestParam.ServerPort);

                    //var ens = new Entities<PhysicalChannel>("outer_id='{0}'", channel.OuterId).Cache;
                    //if (ens.Count > 0)
                    //{
                    //    ens[0].UnicastUrl = channel.UnicastUrl.Replace("127.0.0.1", equipmentIp);
                    //    (ens[0] as IEntity).Update();
                    //}

                    reply.PalyUrls = ResultPlayUrls(requestParam, channel.OuterId);
                    reply.StreamPushUrl = channel.UnicastUrl;
                    reply.ChannelName = channel.OuterId.Split('/')[1];
                    reply.ProviderID = channel.OuterId.Split('/')[0];
                    scope.Complete();

                    Logger.WriteDebugingFmt(Log.CreateLiveChannel, "创建直播频道【ouerId:{0}】成功", channel.OuterId);
                }

            }
            catch (Exception ex)
            {
                reply.ResultCode = -100;
                reply.Desc =string.Format("{0},{1}","创建直播频道失败",ex.Message);
                Logger.WriteError(Log.CreateLiveChannel, "频道创建异常", ex);
            }

            return reply;
        }

        public LiveChannelReply DeleteLiveChannels(LiveChannelDeleteReplyParam requestParam)
        {
            var errorInfos = new List<string>();

            var reply = new LiveChannelReply(){ResultCode = 0,Desc ="删除频道成功" };
            try
            {
             
                foreach (string channelName in requestParam.ChannelNames)
                {
                    var pid = GetProviderbyCmsid(requestParam.DomainName);
                    if(string.IsNullOrEmpty(pid))
                        throw new Exception("域名在CDN系统不存在");

                    try
                    {
                        var ens = new Entities<PhysicalChannel>("outer_id= '{0}'", string.Format("{0}/{1}", pid,channelName)).Cache;
                        if (ens.Count > 0)
                        {
                            string outerId = ens[0].OuterId;

                            using (DbTranscationScope scope = new DbTranscationScope())
                            {
                                GwPublishPointControl.RemoveChannel(ens[0].OuterId);
                                new Entities<PhysicalChannel>().Remove(ens[0]);

                                scope.Complete();

                                Logger.WriteDebugingFmt(Log.DeleteLiveChannel, "删除直播频道【ouerId:{0}】成功", outerId);
                            }
                        }
                        else
                            errorInfos.Add(string.Format("domainname:{0},channelName:{1},error:{2}",
                                requestParam.DomainName, channelName, "频道不存在或已经删除"));
                    }
                    catch (Exception ex)
                    {
                        errorInfos.Add(string.Format("domainname:{0},channelName:{1},error:{2}",
                            requestParam.DomainName, channelName, ex.Message));
                    }
                }

                if (errorInfos.Count > 0)
                    throw new Exception("频道不存在或已删除");

                reply.ResultCode = 0;
            }
            catch (Exception ex)
            {
                reply.ResultCode = -101;
                reply.Desc = string.Format("{0},{1}", "删除直播频道失败", ex.Message);
                Logger.WriteErrorFmt(Log.DeleteLiveChannel, ex, "删除频道异常,{0}", string.Join("|",new List<string>(errorInfos).ConvertAll(o => {return o.ToString(); }).ToArray()));
            }

            return reply;
        }
    }

    [Flags]
    public enum ProtocolTypeFlag
    {
        Non = 0,
        IPTV_TS_RTSP = 1 << 0,
        HPD = 1 << 1,
        ISMA_RTSP = 1 << 2,
        HLS = 1 << 3,
        FLV = 1 << 4
    }

    class WrapperFlag
    {
        public object Source { get; set; }
        public int IntValue { get; set; }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}

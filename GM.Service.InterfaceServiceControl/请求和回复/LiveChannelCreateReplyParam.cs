using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    public class RequestParam
    {
        [JsonProperty("domainname")]
        public string DomainName { get; set; }
    }

    public class LiveChannelCreateReplyParam : RequestParam
    {
        [JsonProperty("channelname")]
        public string ChannelName { get; set; }

        [JsonProperty("streamoutputtypes")]
        public string[] StreamOutputTypes { get; set; }

        [JsonProperty("recordfiletypes")]
        public string[] RecordFileTypes { get; set; }

        [JsonProperty("bitrate")]
        public double Bitrate { get; set; }

        [JsonProperty("recordfilesaveDays")]
        public int RecordFileSaveDays { get; set; }

        //直播类型，
        /*输入类型：
        2：(RFC_RTSP,协议头:rtsp)、
        3：(ADOBE_RTMP,协议头:rtmp)、
        16：(ADOBE_RTMP_IPC,协议头:rtmpipc) */
        [JsonProperty("livetype")]
        public int LiveType { get; set; }

        //是否使用tcp，1使用tcp，0使用udp
        [JsonProperty("usetcp")]
        public int UseTcp { get; set; }

        //是否需要音频，1需要，0不需要
        [JsonProperty("neddaudio")]
        public int NeddAudio { get; set; }

        [JsonProperty("camerasetting")]
        public CameraSetting CameraSetting { get; set; }

        /// <summary>
        /// 中继服务器Ip
        /// </summary>
        [JsonProperty("serverip")]
        public string ServerIp { get; set; }
        /// <summary>
        /// 频道收流端口
        /// </summary>
        [JsonProperty("serverport")]
        public int ServerPort { get; set; }

    }

    public class CameraSetting
    {
        //摄像头ip
        [JsonProperty("cameraip")]
        public string CameraIp { get; set; }

        //摄像头端口
        [JsonProperty("cameraport")]
        public int CameraPort { get; set; }

        //摄像头登录名称摄像头登录名称
        [JsonProperty("cameraname")]
        public string CameraName { get; set; }

        //摄像头密码
        [JsonProperty("camerapwd")]
        public string CameraPwd { get; set; }

        //是否使用高清视频，1高清，0普通
        [JsonProperty("bestvideo")]
        public int BestVideo { get; set; }

        ////是否需要音频，1需要，0不需要
        //[JsonProperty("neddaudio")]
        //public int NeddAudio { get; set; }

        public CameraType CameraType { get; set; }
    }

    public enum CameraType
    {
        HBGK=1,
        TDWY,
        CWAF
    }
}

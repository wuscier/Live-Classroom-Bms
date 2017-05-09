using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Utilities;

namespace Redcdn.ClassRoom.Service {
    public class SettingConfig
    {
        private static SettingConfig _instance;

        public static SettingConfig Instance
        {
            get {
                if (_instance == null) {
                    _instance = SettingManager.Instance.GetSetting<SettingConfig>();

                }
                return _instance;
            }
        }

        /// <summary>
        /// 是否创建物理频道
        /// </summary>
        [XmlSetting("IsCretePhysicalChannel", "true")]
        public bool IsCretePhysicalChannel;

        [XmlSetting("DomainName", "www.redcdn.cn")]
        public string DomainName { get; set; }

        /// <summary>
        /// 中继Ip
        /// </summary>
        [XmlSetting("LrsServerIp", "127.0.0.1")]
        public string LrsServerIp;

        /// <summary>
        /// 输出流格式 flv,hls 多个使用英文"," 号分割
        /// </summary>
        [XmlSetting("StreamOutputTypes", "flv")]
        public string StreamOutputTypes;

        /// <summary>
        /// 文件录制格式，flv,mp4,hls 多个使用英文"," 号分割
        /// </summary>
        [XmlSetting("RecordFileTypes", "flv")]
        public string RecordFileTypes;

        /// <summary>
        /// 码率
        /// </summary>
        [XmlSetting("Bitrate", "2621440")]
        public long Bitrate;

        //为兼容互联网cdn接口服务器设置推流地址
        [XmlSetting("RRIPPort", @"http://127.0.0.1:80")]
        public string RRIPPort;

        /// <summary>
        /// 直播课堂正在直播超时间隔，超过 LiveTimeout 时间，则认为课堂直播超时，单位秒
        /// </summary>
        [XmlSetting("LiveTimeout", "30")]
        public int LiveTimeout;
    }
}

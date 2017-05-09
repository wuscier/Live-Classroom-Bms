using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS {
    public class SettingConfig {

        private readonly static object _syncObj;
        private static SettingConfig _instance;
        static SettingConfig() {

            _syncObj = new object();
        }

        public static SettingConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObj)
                    {
                        if (null == _instance)
                        {
                            _instance = SettingManager.Instance.GetSetting<SettingConfig>();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 中继Ip
        /// </summary>
        [XmlSetting("LrsServerIp", "127.0.0.1")]
        public string LrsServerIp;

        /// <summary>
        /// IES服务器outer_Ip
        /// </summary>
        [XmlSetting("IESIp", "127.0.0.1")]
        public string IESIp;

        /// <summary>
        /// IES时移http端口
        /// </summary>
        [XmlSetting("MediaHttpPort", "20456")]
        public string MediaHttpPort;

        /// <summary>
        /// 分页列表数量
        /// </summary>
        [XmlSetting("PageSize", "10")]
        public int PageSize=10;

        [XmlSetting("DomainName", "www.redcdn.cn")]
        public string DomainName { get; set; }


        /// <summary>
        /// 预约会议Url,调用会议提供http接口
        /// </summary>
        [XmlSetting("MeetingApiUrl", "http://127.0.0.1")]
        public string MeetingApiUrl;

        /// <summary>
        /// 会议管理员用户名
        /// </summary>
        [XmlSetting("MeetingUserName", "admin1")]
        public string MeetingUserName;

        /// <summary>
        /// 会议管理员密码
        /// </summary>
        [XmlSetting("MeetingUserPwd", "admin1")]
        public string MeetingUserPwd;
    }
}
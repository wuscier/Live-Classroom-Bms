using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Service.InterfaceServerControl
{
    public class StopRecordParam
    {
        /// <summary>
        /// 直播频道pid
        /// </summary>
        public string LivechannelPid { get; set; }

        /// <summary>
        /// 直播频道cid
        /// </summary>
        public string LiveChannelCid { get; set; }

        /// <summary>
        /// 直播中继IP
        /// </summary>
        public string ServerIp { get; set; }
    }
}

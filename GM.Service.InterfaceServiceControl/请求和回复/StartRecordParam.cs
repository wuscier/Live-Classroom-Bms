using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Service.InterfaceServerControl
{
    public class StartRecordParam
    {
        /// <summary>
        /// 录制物理频道pid
        /// </summary>
        public string LiveChannelPid { get; set; }

        /// <summary>
        /// 录制物理频道cid
        /// </summary>
        public string LiveChannelCid { get; set; }

        /// <summary>
        /// 录制文件的pid
        /// </summary>
        public string RecordFilePid { get; set; }

        /// <summary>
        /// 录制文件的cid
        /// </summary>
        public string RecordFileCid { get; set; }

        /// <summary>
        /// 录制上下文信息，内容及其格式由各业务决定，这里只做透传
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 中继服务器Ip
        /// </summary>
        public string ServerIp { get; set; }

    }
}

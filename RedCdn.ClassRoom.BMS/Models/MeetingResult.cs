using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS
{
    /// <summary>
    /// 会议接口结果
    /// </summary>
    public class MeetingResult
    {
        [JsonProperty("result")]
        public MeetingResultCode ResultStatus { get; set; }
    }
    
    /// <summary>
    /// 会议接口操作结果
    /// </summary>
    public class MeetingResultCode
    {
        /// <summary>
        /// 结果码
        /// </summary>
        public string RC { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string RD { get; set; }
    }
}

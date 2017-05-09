using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS
{
    /// <summary>
    /// 创建会议请求参数
    /// </summary>
    public class CreateMeetingParams
    {
        /// <summary>
        /// 会议类型，1为即时会议，2为预约会议
        /// </summary>
        [JsonProperty("meetingType")]
        public int MeetingType { get; set; }

        /// <summary>
        /// 会议开始时间，开始时间与1970-1-1相差的秒数（UTC时区）
        /// </summary>
        [JsonProperty("beginDateTime")]
        public string BeginDateTime { get; set; }

        /// <summary>
        /// 管理员用户名
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// 管理员密码
        /// </summary>
        [JsonProperty("pwd")]
        public string Pwd { get; set; }

        /// <summary>
        /// 会议持续时间
        /// </summary>
        [JsonProperty("effectiveHour")]
        public int EffectiveHour { get; set; }
    }

    public class ResultInfo {
        /// <summary>
        /// 结果码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string Desc { get; set; }
    }
}

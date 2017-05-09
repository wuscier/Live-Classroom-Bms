using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS
{
    /// <summary>
    /// 创建会议请求结果
    /// </summary>
    public class CreateMeetingResult:MeetingResult
    {
        [JsonProperty("response")]
        public CreateMeetingResponse Response { get; set; }
    }

    /// <summary>
    /// 创建会议响应数据
    /// </summary>
    public class CreateMeetingResponse
    {
        /// <summary>
        /// 会议编号
        /// </summary>
        public int MeetingId { get; set; }

        /// <summary>
        /// 会议管理员账号
        /// </summary>
        public string AdminPhoneId { get; set; }
    }
}

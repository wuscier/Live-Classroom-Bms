using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class RequestUpdateCurriculumMeetingNoParam
    {
        /// <summary>
        /// 课表Id
        /// </summary>
        [JsonProperty("curriculumid")]
        public int CurriculumId { get; set; }

        /// <summary>
        /// 教室串号
        /// </summary>
        [JsonProperty("classroomimie")]
        public string ClassRoomimie { get; set; }

        [JsonProperty("beginDateTime")]
        public string BeginDateTime { get; set; }

        /// <summary>
        /// 更新课堂号接口,请求参数
        /// </summary>
        [JsonProperty("meetingid")]
        public int MeetingId { get; set; }
    }
}
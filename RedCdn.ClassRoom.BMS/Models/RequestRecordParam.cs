using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS
{
    public class RequestRecordParam : BaseParam
    {
        /// <summary>
        /// 录制文件数据库主键Id
        /// </summary>
        [JsonProperty("recordId")]
        public int RecordId { get; set; }

        /// <summary>
        /// 课表Id
        /// </summary>
        [JsonProperty("curriculumid")]
        public int CurriculumId { get; set; }

        /// <summary>
        /// 会议号(课堂号)
        /// </summary>
        [JsonProperty("classno")]
        public string ClassNo { get; set; }

        /// <summary>
        /// 录制文件cid
        /// </summary>
        [JsonProperty("recordfilecid")]
        public string RecordFileCid { get; set; }
    }
}
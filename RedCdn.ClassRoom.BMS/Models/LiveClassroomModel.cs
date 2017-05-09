using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using AutoMapper;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class LiveClassroomModel {

        [JsonIgnore]
        public int Id { get; set; }

        //教室名称
        [JsonProperty("classroomname")]
        public string ClassRoomName { get; set; }

        //教室串号
         [JsonProperty("classroomimie")]
        public string ClassRoomImie { get; set; }

        //年级
          [JsonProperty("gradename")]
        public string GradeName { get; set; }

        //课程名称
         [JsonProperty("curriculumname")]
        public string CurriculumName { get; set; }

        //直播开始时间
         [JsonProperty("livestreambegintime")]
        public DateTime LiveStreamBeginTime { get; set; }

        //直播地址
         [JsonProperty("livestreamurl")]
        public string LiveStreamUrl { get; set; }

        //上课时间，单位秒
         public string Duration { get; set; }

        //public class LiveClassroomStratTimeStringFormatter : ValueFormatter<DateTime> {
        //    protected override string FormatValueCore(DateTime value) {
        //        return string.Format("{0:yyyy/MM/dd/ HH:mm:ss}", value);
        //    }
        //}

        public class LiveClassroomDurationStringFormatter : ValueFormatter<int> {
            protected override string FormatValueCore(int value) {

                string date = string.Empty;
                var ts = new TimeSpan(0, 0, value);
                if (ts.Hours != 0)
                    date += ts.Hours + "小时";
                if (ts.Minutes != 0)
                    date += ts.Minutes + "分钟";
                if (ts.Seconds != 0)
                    date += ts.Seconds + "秒";

                return date;
            }
        }
    }
}
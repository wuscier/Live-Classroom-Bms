using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class Course {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 课时Id
        /// </summary>
        [JsonProperty("courseid")]
        public int CourseId { get; set; }

        /// <summary>
        /// 课时名称
        /// </summary>
        [JsonProperty("coursename")]
        public string CourseName { get; set; }

        /// <summary>
        /// 课时开始时间,格式：yyyy-mm-dd hh:mi:ss
        /// </summary>
        [JsonProperty("coursestarttime")]
        public DateTime CourseStartTime { get; set; }

        /// <summary>
        /// 课时结束时间,格式：yyyy-mm-dd hh:mi:ss
        /// </summary>
        [JsonProperty("courseendtime")]
        public DateTime CourseEndTime { get; set; }

        /// <summary>
        /// 课时序号id
        /// </summary>
         [JsonProperty("curriculumnumber")]
        public int CurriculumNumber { get; set; }

        /// <summary>
         /// 课时序号名称。
        /// </summary>
        [JsonProperty("curriculumname")]
         public string CurriculumName { get; set; }

        [JsonProperty("gradeid")]
        public int GradeId { get; set; }

        [JsonProperty("gradename")]
        public string GradeName { get; set; }

        /// <summary>
        /// 星期id
        /// </summary>
        [JsonProperty("weekid")]
         public int WeekId { get; set; }

        /// <summary>
        /// 星期名称
        /// </summary>
        [JsonProperty("weekname")]
        public string WeekName { get; set; }

        /// <summary>
        /// 是否推流
        /// </summary>
        [JsonProperty("ispush")]
        public bool IsPush { get; set; }
        /// <summary>
        /// 是否自动录制
        /// </summary>
        [JsonProperty("isrecord")]
        public bool IsRecord { get; set; }


        /// <summary>
        /// 是否公网推流
        /// </summary>
        [JsonProperty("ispushremote")]
        public bool IsPushRemote { get; set; }
        /// <summary>
        /// 是否公网录制
        /// </summary>
        [JsonProperty("isrecordremote")]
        public bool IsRecordRemote { get; set; }


        [JsonProperty("islocalrecord")]
        public bool IsLocalRecord { get; set; }


        /// <summary>
        /// 会议号
        /// </summary>
         [JsonProperty("mettingid")]
        public int MettingId { get; set; }

        /// <summary>
        /// 主讲教室号
        /// </summary>
        [JsonProperty("mainclassroomid")]
        public int MainclassRoomId { get; set; }

        [JsonProperty("mainclassroomname")]
        public string Mainclassroomname { get; set; }

        [JsonProperty("listenclassroomids")]
        public string ListenClassRoomIds { get; set; }

        [JsonProperty("listenclassroomnames")]
        public string ListenClassRoomnames { get; set; }

        [JsonProperty("mettingbegindatetime")]
        public long MettingBeginDateTime { get; set; }
       
    }

    public class CourseCollection
    {
         [JsonProperty("courselist")]
        public List<Course> Courses { get; set; }

        [JsonProperty("count")]
         public int Count { get; set; }

        public CourseCollection()
        {
            Courses=new List<Course>();
        }
    }

}
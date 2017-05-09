using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    /// <summary>
    /// 课表
    /// </summary>
    [DBTable("curriculum")]
    public class Curriculum:Entity,IDeleteflag {
        [DBField("name")]
        public string Name { get; set; }
        /// <summary>
        /// 会议号
        /// </summary>
        [DBField("metting_id")]
        public int MettingId { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        [DBField("weekday_id")]
        public int WeekDayId { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        [DBField("curriculum_nameid")]
        public int CurriculumNameId { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
         [DBField("grade_id")]
        public int GradeId { get; set; }

        /// <summary>
         /// 课序
        /// </summary>
        [DBField("curriculum_numberid")]
         public int CurriculumNumberId { get; set; }

        /// <summary>
        /// 主讲教室
        /// </summary>
        [DBField("main_classroom_id")]
        public int MainClassRoomId { get; set; }

        /// <summary>
        /// 听课教室id集合。多个以逗号分隔
        /// </summary>
        [DBField("listen_classroom_ids")]
        public string ListenClassRoomIds { get; set; }


        [DBField("islocalrecord")]
        public bool IsLocalRecord { get; set; }
        [DBField("ispush")]
        public bool IsPush { get; set; }
        /// <summary>
        /// 是否录制
        /// </summary>
        [DBField("isrecord")]
        public bool IsRecord { get; set; }
        [DBField("ispushremote")]
        public bool IsPushRemote { get; set; }
        [DBField("isrecordremote")]
        public bool IsRecordRemote { get; set; }

        [DBField("delete_flag")]
        public bool Deleteflag { get; set; }

        [DBField("metting_beginDatetime")]
        public long MettingBeginDateTime { get; set; }
    }
}

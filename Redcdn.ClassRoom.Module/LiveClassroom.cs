using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    [DBTable("live_classroom")]
    public class LiveClassroom:Entity {

        /// <summary>
        /// 教室名称
        /// </summary>
        [DBField("classroom_name")]
        public string ClassRoomName { get; set; }

        /// <summary>
        /// 教室串号
        /// </summary>
        [DBField("classroom_imie")]
        public string ClassRoomImie { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        [DBField("grade_name")]
        public string GradeName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [DBField("curriculum_name")]
        public string CurriculumName { get; set; }

        /// <summary>
        /// 直播开始时间
        /// </summary>
        [DBField("livestream_begintime")]
        public DateTime LiveStreamBeginTime { get; set; }

        /// <summary>
        /// 直播地址
        /// </summary>
        [DBField("livestreamurl")]
        public string LiveStreamUrl { get; set; }
    }
}

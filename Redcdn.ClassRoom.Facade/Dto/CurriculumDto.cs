using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public class CurriculumDto {

        public int Id { get; set; }
        public string Name { get; set; }

        public int MettingId { get; set; }

        /// <summary>
        /// 课序id
        /// </summary>
        public int CurriculumNumberId { get; set; }

        /// <summary>
        /// 课序号名称,如，第一节
        /// </summary>
        public string CurriculumNumberName { get; set; }

        /// <summary>
        /// 课时Id
        /// </summary>
        public int CurriculumNameId { get; set; }

        /// <summary>
        /// 课时名称,如,语文
        /// </summary>
        public string CurriculumNameName { get; set; }

        /// <summary>
        /// 年级Id
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 主讲教室id
        /// </summary>
        public int MainclassRoomId { get; set; }

        /// <summary>
        /// 主讲教室
        /// </summary>
        public string MainClassRoomName { get; set; }

        /// <summary>
        /// 听讲教室Id集合。多个以逗号分隔
        /// </summary>
        public string ListenClassRoomIds { get; set; }

        /// <summary>
        /// 听讲教室名称集合。多个以逗号分隔
        /// </summary>
        public string ListenClassRoomNames { get; set; }

        /// <summary>
        /// 星期Id
        /// </summary>
        public int WeekDayId { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string WeekDayName { get; set; }
        /// <summary>
        /// 是否本地录制
        /// </summary>
        public bool IsLocalRecord { get; set; }
        /// <summary>
        /// 是否推流直播
        /// </summary>
        public bool IsPush { get; set; }
        /// <summary>
        /// 是否录制
        /// </summary>
        public bool IsRecord { get; set; }
        /// <summary>
        /// 是否公网推流直播
        /// </summary>
        public bool IsPushRemote { get; set; }
        /// <summary>
        /// 是否公网录制
        /// </summary>
        public bool IsRecordRemote { get; set; }

        public DateTime UpdateTime { get; set; }

        public long MettingBeginDateTime { get; set; }
    }
}

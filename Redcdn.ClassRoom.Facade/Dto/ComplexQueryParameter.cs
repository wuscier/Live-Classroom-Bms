using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {

    public class ComplexQueryParameter {

        /// <summary>
        /// 星期映射Id
        /// </summary>
        public int WeekDayId { get; set; }

        /// <summary>
        /// 主讲教室id
        /// </summary>
        public int MainClassRoomId { get; set; }

        /// <summary>
        /// 课序映射Id
        /// </summary>
        public int CurriculumNumberId { get; set; }

        /// <summary>
        /// 年级映射Id
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// 课程名映射Id
        /// </summary>
        public int CurriculumNameId { get; set; }
    }
}

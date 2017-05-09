using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    /// <summary>
    /// 课序Ui  dto
    /// </summary>
    public class CurriculumNumberDto {

        /// <summary>
        /// 课序Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 课表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课序开始时间
        /// </summary>
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 课序时长
        /// </summary>
        public int Duration { get; set; }
    }
}

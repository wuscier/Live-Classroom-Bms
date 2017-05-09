using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    /// <summary>
    /// 课序字典表
    /// </summary>
    [DBTable("curriculum_number")] 
    public class CurriculumNumber:BaseMapping,IDeleteflag {

        /// <summary>
        /// 课序开始时间
        /// </summary>
        [DBField("start_time")]
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 课序时长
        /// </summary>
        [DBField("duration")]
        public int Duration { get; set; }

        public override DateTime CreateTime {
            get;
            set;
        }

        public override DateTime UpdateTime {
            get;
            set;
        }

        [DBField("delete_flag")]
        public bool Deleteflag { get; set; }
    }
}

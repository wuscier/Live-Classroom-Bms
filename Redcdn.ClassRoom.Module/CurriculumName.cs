using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    /// <summary>
    /// 课时字典表
    /// </summary>
      [DBTable("course")]
    public class CurriculumName:BaseMapping {

        public override DateTime CreateTime {
            get;
            set;
        }

        public override DateTime UpdateTime {
            get;
            set;
        }
    }
}

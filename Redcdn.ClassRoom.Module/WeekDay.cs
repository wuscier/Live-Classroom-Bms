using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {

    /// <summary>
    /// 星期字典表
    /// </summary>
    [DBTable("weekday")]
    public class WeekDay:BaseMapping {

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

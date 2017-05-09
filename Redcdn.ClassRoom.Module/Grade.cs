using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    /// <summary>
    /// 年级字典表
    /// </summary>
    [DBTable("grade")]
    public class Grade :BaseMapping,IDeleteflag{
        public override DateTime CreateTime {
            get; set;
        }

        public override DateTime UpdateTime {
            get; set;
        }

        [DBField("DELETE_FLAG")]
        public bool Deleteflag { get; set; }
    }
}

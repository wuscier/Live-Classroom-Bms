using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    public abstract class BaseMapping : Entity {

        [DBField("name")]
        public virtual string Name { get; set; }
    }
}

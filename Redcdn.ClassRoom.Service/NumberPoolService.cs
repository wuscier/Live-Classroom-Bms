using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class NumberPoolService : EntityService<EntityKey, NumberPool, NumberPoolService>
    {
        public List<NumberPool> GetNotAllocatedNumPool()
        {
           return new Entities<NumberPool>("isallot=0").Cache;
        }

        public NumberPool GetNumberPoolByNum(string classno)
        {
            var ens = new Entities<NumberPool>("ClassNo='{0}'", classno).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public void CannelNumberPool2SchoolRoomMapping(string classno)
        {
            var entity= GetNumberPoolByNum(classno);
            entity.IsAllot = false;

            (entity as IEntity).Update();
        }
    }
}

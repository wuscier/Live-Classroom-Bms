using GM.Orm;
using Redcdn.ClassRoom.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Service {
    public class WeekDayService : EntityService<EntityKey, WeekDay, WeekDayService> {

        public int GetIdByName(string name) {
       
             var entity= WeekDayService.Instance.GetAll().Find(wk => wk.Name == name);
            if (entity != null)
                return entity.Id;
            return 0;
        }
    }
}

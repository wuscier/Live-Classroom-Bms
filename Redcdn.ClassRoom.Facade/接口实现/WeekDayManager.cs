using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class WeekDayManager:IWeekDayManager {
        public IList<WeekDayDto> GetAll()
        {
            var dtoList = new List<WeekDayDto>();

            try{
                var list = WeekDayService.Instance.GetAll();
                foreach (WeekDay weekDay in list){
                    var dto = new WeekDayDto();
                    AutoMapperWrapper.Instance.Map(weekDay, dto);
                    dtoList.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtoList;
        }

        public WeekDayDto Get(int id)
        {
            var dto = new WeekDayDto();
            try
            {
                var entity = WeekDayService.Instance.Get(new EntityKey() { Id = id });
                return AutoMapperWrapper.Instance.Map(entity, dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

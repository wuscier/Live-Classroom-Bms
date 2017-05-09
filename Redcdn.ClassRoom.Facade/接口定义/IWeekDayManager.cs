using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface IWeekDayManager {

        /// <summary>
        /// 获取日起字典表数据，周一至周日
        /// </summary>
        /// <returns></returns>
        IList<WeekDayDto> GetAll();
    }
}

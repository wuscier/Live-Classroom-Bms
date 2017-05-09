using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GM.Utilities;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Services {
    public class CacheService : SingletonBase<CacheService>
    {

        public List<WeekDayDto> WeekDayList {
            get {
                return FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().GetWeekDayListCache();
            }
        }

        public List<GradeDto> GradeDtoList {
            get {
                return FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().GetGradeListCache();
            }
        }

        public List<CurriculumNameDto> CurriculumNameDtoList {
            get {
                return FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().GetCurriculumNameListCache();
            }
        }

        public List<CurriculumNumberDto> CurriculumNumberDtoList {
            get {
                return FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().GetCurriculumNumbersListCache();
            }
        }
    }
}
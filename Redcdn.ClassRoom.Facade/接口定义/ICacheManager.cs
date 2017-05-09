using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface ICacheManager
    {
        void Load();

        void ReLoadAll();

        List<GradeDto> GetGradeListCache();
        List<WeekDayDto> GetWeekDayListCache();

        List<CurriculumNameDto> GetCurriculumNameListCache();

        List<CurriculumNumberDto> GetCurriculumNumbersListCache();
    }
}

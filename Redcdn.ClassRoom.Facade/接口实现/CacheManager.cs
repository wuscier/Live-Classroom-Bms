using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class CacheManager:ICacheManager
    {

        private List<GradeDto> _grades;
        private List<WeekDayDto> _weekdays;
        private List<CurriculumNameDto> _curriculumNames;
        private List<CurriculumNumberDto> _curriculumNumbers;

        void InitData()
        {
            ClearList();
            try {
                CacheService.Instance.Grades.ForEach(module => {
                    var dto = new GradeDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    _grades.Add(dto);
                });

                CacheService.Instance.WeekDays.ForEach(module => {
                    var dto = new WeekDayDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    _weekdays.Add(dto);
                });

                CacheService.Instance.CurriculumNames.ForEach(module => {
                    var dto = new CurriculumNameDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    _curriculumNames.Add(dto);
                });

                CacheService.Instance.CurriculumNumbers.ForEach(module => {
                    var dto = new CurriculumNumberDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    _curriculumNumbers.Add(dto);
                });

            } catch (Exception ex) {
                throw new Exception("缓存字典表异常: " + ex);
            }
        }

        void ClearList()
        {
            _grades = new List<GradeDto>();
            _weekdays = new List<WeekDayDto>();
            _curriculumNames = new List<CurriculumNameDto>();
            _curriculumNumbers = new List<CurriculumNumberDto>();
        }

        public void Load()
        {
            CacheService.Instance.Load();
            InitData();
        }

        public void ReLoadAll() {

            InitData();
        }

        public List<GradeDto> GetGradeListCache()
        {
            return _grades;
        }

        public List<WeekDayDto> GetWeekDayListCache()
        {
            return _weekdays;
        }

        public List<CurriculumNameDto> GetCurriculumNameListCache()
        {
            return _curriculumNames;
        }

        public List<CurriculumNumberDto> GetCurriculumNumbersListCache()
        {
            return _curriculumNumbers;
        }
    }
}

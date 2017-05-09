using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class CacheService {

        private readonly  static CacheService _instace=new CacheService();

        public static CacheService Instance
        {
            get { return _instace; }
        }


        public List<Grade> Grades { get; set; }

        public List<WeekDay> WeekDays { get; set; }
        public List<CurriculumName> CurriculumNames { get; set; }

        public List<CurriculumNumber> CurriculumNumbers { get; set; }

        private readonly object _lockObject;

        private CacheService()
        {
            _lockObject = new object();
        }



        public void Load()
        {
            lock (_lockObject)
            {
                Grades = new List<Grade>(GradeService.Instance.GetAll());
                WeekDays = new List<WeekDay>(WeekDayService.Instance.GetAll());
                CurriculumNames = new List<CurriculumName>(CurriculumNameService.Instance.GetAll());
                CurriculumNumbers = new List<CurriculumNumber>(CurriculumNumberService.Instance.GetAll());
            }
        }

        public void ReLoad<T>(T dicObj)where T:class
        {
            lock (_lockObject)
            {
                if (dicObj is Grade)
                    Grades = new List<Grade>(GradeService.Instance.GetAll());
                if (dicObj is CurriculumName)
                    CurriculumNames = new List<CurriculumName>(CurriculumNameService.Instance.GetAll());
                if (dicObj is CurriculumNumber)
                    CurriculumNumbers = new List<CurriculumNumber>(CurriculumNumberService.Instance.GetAll());
            }
        }
    }


     public abstract class  EntityCache
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class GradesCache : EntityCache
    {
        
    }

    
}

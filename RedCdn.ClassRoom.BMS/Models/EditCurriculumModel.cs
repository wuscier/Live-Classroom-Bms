using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RedCdn.ClassRoom.BMS.Services;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Models
{
    public class EditCurriculumModel
    {
        /// <summary>
        /// 当前编辑的课程
        /// </summary>
        public CurriculumDto Curriculum { get; set; }
        public CurriculumEidtInfo CurriculumEidtInfo { get; set; }
    }

    public class IndexPaging <T> where T: class
    {
        public CurriculumEidtInfo CurriculumEidtInfo { get; set; }
        public PagingQueryResultDto<T> Paging { get; set; }

        public IndexPaging() 
        {
            CurriculumEidtInfo = new CurriculumEidtInfo();
            Paging = new PagingQueryResultDto<T>();
        }
    }

    public class CurriculumEidtInfo
    {
        /// <summary>
        /// 星期枚举
        /// </summary>
        public IList<WeekDayDto> WeekDayList { get; set; }
        /// <summary>
        /// 年级枚举
        /// </summary>
        public IList<GradeDto> GradeDtoList { get; set; }
        /// <summary>
        /// 课程名称枚举
        /// </summary>
        public IList<CurriculumNameDto> CurriculumNameDtoList { get; set; }
        /// <summary>
        /// 课程序号枚举
        /// </summary>
        public IList<CurriculumNumberDto> CurriculumNumberDtoList { get; set; }
        /// <summary>
        /// 教室列表
        /// </summary>
        public IList<SchoolRoomDto> ClassRoomDtoList { get; set; }

        public CurriculumEidtInfo()
        {
            WeekDayList = new List<WeekDayDto>();
            GradeDtoList = new List<GradeDto>();
            CurriculumNameDtoList = new List<CurriculumNameDto>();
            CurriculumNumberDtoList = new List<CurriculumNumberDto>();
            ClassRoomDtoList = new List<SchoolRoomDto>();
        }

        public static CurriculumEidtInfo Load()
        {
            //刷新缓存，更新字典表信息
            FacadeFactory.Instance.Get<ICacheManager>().ReLoadAll();

            var tmp = new CurriculumEidtInfo();
            tmp.CurriculumNameDtoList = CacheService.Instance.CurriculumNameDtoList;// FacadeFactory.Instance.Get<ICurriculumNameManager>().GetAll();
            tmp.CurriculumNumberDtoList = CacheService.Instance.CurriculumNumberDtoList;// FacadeFactory.Instance.Get<ICurriculumNumberManager>().GetAll();
            tmp.GradeDtoList = CacheService.Instance.GradeDtoList; //FacadeFactory.Instance.Get<IGradeManager>().GetAll();
            tmp.WeekDayList = CacheService.Instance.WeekDayList;// FacadeFactory.Instance.Get<IWeekDayManager>().GetAll();
            tmp.ClassRoomDtoList = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetAll();
            return tmp;
        }
    }
}
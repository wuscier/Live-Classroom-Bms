using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS {
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Initialize();

            //FacadeFactory.Instance.Get<IInterfaceServerControl>().Start();
        }

        protected void Initialize()
        {
            Mapper.CreateMap<RecordFileDto, RecordFileModel>()
                                     .ForMember(rf => rf.StartTime,
                                         rfm => rfm.AddFormatter<StartTimeStringFormatter>())
                                     .ForMember(rf => rf.Duration, rfm => rfm.AddFormatter<DurationStringFormatter>())
                                     .ForMember(rf => rf.FilePlayUrl, rfm => rfm.AddFormatter<FilePlayUrlFormatter>());

            Mapper.CreateMap<LiveClassRoomDto, LiveClassroomModel>() .ForMember(rf => rf.Duration, rfm => rfm.AddFormatter<LiveClassroomModel.LiveClassroomDurationStringFormatter>());
            Mapper.CreateMap<GradeModel, GradeDto>();
            Mapper.CreateMap<GradeDto, GradeModel>();
      
            FacadeFactory.Instance.Get<ICacheManager>().Load();
        }
    }

    public class GlobalSetting {
        public int PageSize = SettingConfig.Instance.PageSize;
    }
}
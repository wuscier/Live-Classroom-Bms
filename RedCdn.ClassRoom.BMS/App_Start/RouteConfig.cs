using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RedCdn.ClassRoom.BMS {
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "gettop",
               url: "{action}.aspx",
               defaults: new { controller = "SupperSchool", action = "gettopographics", id = UrlParameter.Optional });


            routes.MapRoute(
              name: "live",
              url: "live/{action}",
              defaults: new { controller = "LiveClassRoom", action = "index" });

            routes.MapRoute(
            name: "ccnumber",
            url: "ccnumber/{action}",
            defaults: new { controller = "CurriculumNumber", action = "index" });


            routes.MapRoute(
          name: "ccname",
          url: "ccname/{action}",
          defaults: new { controller = "CurriculumName", action = "index" });


            //routes.MapRoute(
            //    "PCStartRecord", // Pc端调启动录制
            //    "startrecord",
            //    new {controller = "SupperSchool", action = "StartRecord"} // 参数默认值
            //    );

            //routes.MapRoute(
            //    "PCStopRecord", // Pc端调用停止录制
            //    "stoprecord",
            //    new {controller = "SupperSchool", action = "StopRecord"} // 参数默认值
            //    );

           //   http://localhost/GetCourseList?classroomid=123
            routes.MapRoute(
                "PCGetCourseList", // 获取课表
                "getcourselist",
                new {controller = "SupperSchool", action = "GetCourseList", classroomid = UrlParameter.Optional}
                // 参数默认值
                );

            routes.MapRoute(
             "PCAuthentication", // 鉴权接口
             "AuthenticationClassRoom",
             new { controller = "SupperSchool", action = "AuthenticationClassRoom", classRoomImei = UrlParameter.Optional }
                // 参数默认值
             );

            routes.MapRoute(
              "MettingExternal",
              "EnterpriseUserCenter/{action}", // 带有参数的 URL
              new { controller = "MettingExternalApi", action = "EucService" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Classroom", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}
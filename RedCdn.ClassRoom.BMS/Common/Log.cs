using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS {
    public class Log:LogCatalogs
    {

        public static string StartRecord = "启动录制";
        public static string StopRecord = "停止录制";
        public static string ReportFile = "录制汇报";
        public static string EucService = "会议外部接口";
        public static string LogoManger = "pclogo管理";
        public static string GetClassList = "获取教室列表";
        public static string LiveTopographics = "获取直播拓扑信息";
        public static string LiveChannelInfo = "获取物理频道信息";
        public static string ReportContentfile = "ReportContentfile";
        public static string ReserveMetting = "预约会议";

        static Log() {
            RegisteCatalogs(typeof(Log));
        }
    }
}
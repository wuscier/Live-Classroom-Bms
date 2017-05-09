using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GM.Business.Module;
using GM.Orm;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS {
    public class RecordFileModel {
        public int Id { get; set; }

        public string WeekDayName { get; set; }

        // 课序
        public string CurriculumNumber { get; set; }

        //课时
        public string CurriCulumName { get; set; }

        //年级
        public string GradeName { get; set; }

        //文件播放uRL，录制文件存储磁盘相对路径
        public string FilePlayUrl { get; set; }

        //主听讲教室
        public string MainClassRoomName { get; set; }

        //听课教室
        public string ListenClassRooms { get; set; }

        //录入文件开始时间
        public string StartTime { get; set; }

        //时长  eg:10分09秒
        public string Duration { get; set; }

        //课堂号
        public string ClassNo { get; set; }

    }

    public class StartTimeStringFormatter : ValueFormatter<DateTime>
    {
        protected override string FormatValueCore(DateTime value)
        {
            return string.Format("{0:yyyy/MM/dd/ HH:mm:ss}", value);
        }
    }

    public class DurationStringFormatter : ValueFormatter<int> {
        protected override string FormatValueCore(int value)
        {

            string date = string.Empty;
            var ts = new TimeSpan(0, 0, value);
            if (ts.Hours != 0)
                date += ts.Hours + "小时";
            if(ts.Minutes!=0)
                date += ts.Minutes + "分钟";
            if (ts.Seconds != 0)
                date += ts.Seconds + "秒";

            return date;
        }
    }

    public class FilePlayUrlFormatter : ValueFormatter<string>
    {
        protected override string FormatValueCore(string value)
        {
            Dictionary<string, int> iesdic = IesServerInfo.Instance.GetIesIpAndMediaPort();
            KeyValuePair<string, int> pair = iesdic.First();

            return string.Format("{0}//{1}:{2}/localfile/{3}", "http:", pair.Key, pair.Value, value.Replace("\\", "/"));
        }
    }

    public class IesServerInfo
    {
        private  readonly  static IesServerInfo _instance=new IesServerInfo();

        public static IesServerInfo Instance { get { return _instance; }}



        Dictionary<string,int> dic=new Dictionary<string, int>(); 

        private IesServerInfo()
        {
        }

        public Dictionary<string, int> GetIesIpAndMediaPort() {
            if (dic.Count==0) {
                var eqs = new Entities<IESServer>("type_id={0}", new IESServer().TypeID).Cache;
                if (eqs.Count > 0) {
                    var config = eqs[0].ServiceConfig as IESServerConfig;
                    dic[eqs[0].OuterIpAddress] = config.MediaHttpPort;
                    return dic;
                }
            }
            return dic;
        }
    }
}
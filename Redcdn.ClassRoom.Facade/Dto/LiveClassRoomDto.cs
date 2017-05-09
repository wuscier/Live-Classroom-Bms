using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade
{
    public class LiveClassRoomDto {

        #region old property
        //public int Id { get; set; }  

        ///// <summary>
        /////课表ID
        ///// </summary>
        //public int CurriculumId { get; set; }

        ///// <summary>
        ///// 课程名称
        ///// </summary>
        //public string CurriCulumName { get; set; }

        ///// <summary>
        ///// 科系
        ///// </summary>
        //public string CurriculumNumber { get; set; }

        ///// <summary>
        ///// 年级
        ///// </summary>
        //public string GradeName { get; set; }
       
        ///// <summary>
        ///// 主讲教室名称
        ///// </summary>
        //public string MainClassRoomName { get; set; }

        ///// <summary>
        ///// 听课教室名称
        ///// </summary>
        //public string ListenClassRooms { get; set; }      

        ///// <summary>
        ///// 时长
        ///// </summary>
        //public int Duration { get; set; }

        ///// <summary>
        ///// 日期
        ///// </summary>
        //public DateTime StartTime { get; set; }

        ///// <summary>
        ///// 直播URL
        ///// </summary>
        //public string LiveStreamPlayUrl { get; set; }
        #endregion

        public int Id { get; set; }

        //教室名称
        public string ClassRoomName { get; set; }

        //教室串号
        public string ClassRoomImie { get; set; }

        //年级
        public string GradeName { get; set; }

        //课程名称
        public string CurriculumName { get; set; }

        //直播开始时间
        public DateTime LiveStreamBeginTime { get; set; }

        //直播地址
        public string LiveStreamUrl { get; set; }

        // 上课时长  单位 秒
        public int Duration { get; set; }
    }
}

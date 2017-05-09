using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade
{
    public class RecordFileDto
    {

        public int Id { get; set; }  

        /// <summary>
        ///课表ID
        /// </summary>
        public int CurriculumId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CurriCulumName { get; set; }

        /// <summary>
        /// 课序
        /// </summary>
        public string CurriculumNumber { get; set; }

        public string WeekDayName { get; set; }

        public string PhysicalFileImOuterId { get; set; }

        public int FileSequence { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 主讲教室id
        /// </summary>
        public int ClassroomId { get; set; }

        /// <summary>
        /// 主讲教室名称
        /// </summary>
        public string MainClassRoomName { get; set; }

        /// <summary>
        /// 听课教室名称
        /// </summary>
        public string ListenClassRooms { get; set; }      

        /// <summary>
        /// 时长,单位：秒
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 录制文件播放地址,文件相对路径
        /// </summary>
        public string FilePlayUrl { get; set; }

        /// <summary>
        /// 课堂号
        /// </summary>
        public string ClassNo { get; set; }


        /// <summary>
        /// 录制状态 0开始录制，1停止录制
        /// </summary>
        public int RecordStatus { get; set; }

    }


}

using GM.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Module
{
    [DBTable("Record_File")]
    public class RecordFile : BaseMapping,IDeleteflag,IRecycleflag
    {
        /// <summary>
        /// 课表Id
        /// </summary>
        [DBField("Curriculum_Id")]
        public int CurriculumId { get; set; }

        /// <summary>
        /// CDN物理文件映射ContentId
        /// </summary>
        [DBField("Physical_File_Im_outerd")]
        public string PhysicalFileImOuterId { get; set; }

        /// <summary>
        /// 录制状态 0开始录制，1停止录制
        /// </summary>
        [DBField("Record_Status")]
        public int RecordStatus { get; set; }

        /// <summary>
        /// 文件播放uRL，录制文件存储磁盘相对路径
        /// </summary>
        [DBField("Record_File_Play_Url")]
        public string RecordFilePlayUrl { get; set; }

        /// <summary>
        /// 教室id
        /// </summary>
        [DBField("classroom_id")]
        public int ClassroomId { get; set; }

        /// <summary>
        /// 录制开始时间
        /// </summary>
        [DBField("Start_Time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 录制文件时常  单位:秒
        /// </summary>
        [DBField("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///课堂号
        /// </summary>
        [DBField("Class_No")]
        public string ClassNo { get; set; }

        /// <summary>
        /// 文件序号，标识同一个课堂号录制多个文件使用
        /// </summary>
        [DBField("file_sequence")]
        public int FileSequence { get; set; }

        [DBField("RECYCLE_FLAG")]
        public bool Recycleflag{ get; set; }

        [DBField("DELETE_FLAG")]
        public bool Deleteflag { get; set; }

        public override string Name { get; set; }
    }
}

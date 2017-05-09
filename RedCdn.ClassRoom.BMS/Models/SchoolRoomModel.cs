using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedCdn.ClassRoom.BMS
{
    public class SchoolRoomModel
    {
        [Required(ErrorMessage = "请输入教室地址")]
        [StringLength(64, ErrorMessage = "教室地址不能超过64个字符", MinimumLength = 0)]
        public string SchoolRoomAddress { get; set; }

        [Required(ErrorMessage = "请输入教室串号")]
        [StringLength(32, ErrorMessage = "教室串号不能超过32个字符", MinimumLength = 1)]
        public string SchoolRoomIMEI { get; set; }

        [Required(ErrorMessage = "请输入教室名称")]
        [StringLength(16, ErrorMessage = "教室名称不能超过16个字符", MinimumLength = 0)]
        public string SchoolRoomName { get; set; }

        [Required]
        public string SchoolRoomNum { get; set; }

        public string PhysicalChannelOuterId { get; set; }

        public string PlayStreamUrl { get; set; }

        public string PushStreamUrl { get; set; }

        [StringLength(255, ErrorMessage = "备注信息不能超过255个字符", MinimumLength = 0)]
        public string Remark { get; set; }

        public int Id { get; set; }
    }
}
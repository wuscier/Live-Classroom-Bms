using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedCdn.ClassRoom.BMS {
    public class CurriculumNumberModel
    {

        public int? Id { get; set; }

        [Required(ErrorMessage = "请输入课序名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请选择课序上课开始时间")]

        public DateTime StarTime { get; set; }

        [Required(ErrorMessage = "请输入课序时长")]
        public int Duration { get; set; }
    }
}
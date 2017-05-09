using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedCdn.ClassRoom.BMS {
    public class CurriculumNameModel {

        public int? Id { get; set; }

        [Required(ErrorMessage = "请输入课时")]
        public string Name { get; set; }
    }
}
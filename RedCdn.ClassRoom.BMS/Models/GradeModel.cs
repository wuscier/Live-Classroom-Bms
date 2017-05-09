using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedCdn.ClassRoom.BMS {
    public class GradeModel {

        public int? Id { get; set; }

        [Required(ErrorMessage = "请输入年级")]
        public string Name { get; set; }
    }
}
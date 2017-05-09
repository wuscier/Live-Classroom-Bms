using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedCdn.ClassRoom.BMS {
    public class LogonModel {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class PersonalModel
    {
        public int Id { get; set; }

        public string ManagerAccount { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string NewPassWord { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassWord",ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassWord { get; set; }
    }
}
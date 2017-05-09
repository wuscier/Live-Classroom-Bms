using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {

    /// <summary>
    /// 系统管理Ui dto对象
    /// </summary>
    public class SystemManagerDto {

        public int Id { get; set; }

        public string SchoolManagerAccount { get; set; }

        public string SchoolManagerName { get; set; }

        public int AccountType { get; set; }

        public string SchoolManagerPassWord { get; set; }

        public string[] NumberPool { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {

    /// <summary>
    /// 系统管理
    /// </summary>
    [DBTable("system_manager")]
    public class SystemManager : Entity
    {
        /// <summary>
        /// 校园管理员帐号
        /// </summary>
        [DBField("schoolmanager_account")]
        public string SchoolManagerAccount { get; set; }

        /// <summary>
        /// 帐号类型 0=运维人员，1=校园管理员
        /// </summary>
        [DBField("account_type")]
        public int AccountType { get; set; }

       /// <summary>
       /// 校园管理员显示名称
       /// </summary>
        [DBField("schoolmanager_name")]
        public string SchoolManagerName { get; set; }

        /// <summary>
        /// 校园管理员密码
        /// </summary>
        [DBField("schoolmanager_password")]
        public string SchoolManagerPassWord { get; set; }

        public string[] NumberPool { get; set; }

    }
}

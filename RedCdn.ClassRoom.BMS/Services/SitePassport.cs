using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCdn.ClassRoom.BMS.Services {
    public class SitePassport {

        public static SitePassport Empty=new SitePassport();

        private SitePassport() { }

        public int UserId { get; private set; }

        /// <summary>
        /// 0 ：运维人员，1:校园管理员
        /// </summary>
        public int AccountType { get; private set; }

        public string UserName { get; private set; }

        public string NickName { get; private set; }

        public SitePassport(int userId,string userName,int accountType, string nickName)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.AccountType = accountType;
            this.NickName = nickName;
        }
    }
}
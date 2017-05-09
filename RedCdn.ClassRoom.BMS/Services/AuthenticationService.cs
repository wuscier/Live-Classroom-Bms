using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using GM.Utilities;
using Newtonsoft.Json;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Services {
    public class AuthenticationService : SingletonBase<AuthenticationService>
    {
        public void SignIn(SystemManagerDto dto, bool createPersistentCookie)
        {
            var passport = new SitePassport(dto.Id, dto.SchoolManagerAccount,dto.AccountType,dto.SchoolManagerName);

            var now = DateTime.Now;
            var ticket = new FormsAuthenticationTicket(1, passport.UserName, now,
                createPersistentCookie ? now.AddDays(1) : now.Add(FormsAuthentication.Timeout), createPersistentCookie,
                JsonConvert.SerializeObject(passport));

            var encrytedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrytedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };
            if (createPersistentCookie)
            {
                cookie.Expires = ticket.Expiration;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Expires = new DateTime(1999,1,1),
                Domain = FormsAuthentication.CookieDomain,
                Path=FormsAuthentication.FormsCookiePath
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
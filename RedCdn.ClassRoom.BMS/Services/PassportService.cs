using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS.Services {
    public class PassportService {

        public SitePassport GetCurrentPassport() {
            return GetCurrentPassport(HttpContext.Current.User);
        }

        public SitePassport GetCurrentPassport(IPrincipal user)
        {
            if (user != null && user.Identity!=null&&user.Identity.IsAuthenticated&&user.Identity is FormsIdentity)
            {
                var passportString = ((FormsIdentity) user.Identity).Ticket.UserData;
                return JsonConvert.DeserializeObject<SitePassport>(passportString);
            }

            return null;
        }
    }
}
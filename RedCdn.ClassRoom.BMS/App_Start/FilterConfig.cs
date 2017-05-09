using System.Web;
using System.Web.Mvc;

namespace RedCdn.ClassRoom.BMS {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
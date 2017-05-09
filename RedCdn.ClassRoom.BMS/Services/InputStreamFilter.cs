using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS.Services {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InputStreamFilter : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            Logger.WriteDebugingFmt(LogCatalogs.OperationHits, "收到Action请求 actionname:{0}", (filterContext.ActionDescriptor).ActionName);
            if (filterContext.RequestContext.HttpContext.Request.InputStream.Length == 0)
            {
                var actionname = (filterContext.ActionDescriptor).ActionName;
                Logger.WriteWarningFmt(LogCatalogs.OperationHits, "actionname [{0}] 接收inputstream数据长度为0", actionname);
                filterContext.Result=new JsonResult { Data = new { status = -1, message = "接收inputstream数据长度为0" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
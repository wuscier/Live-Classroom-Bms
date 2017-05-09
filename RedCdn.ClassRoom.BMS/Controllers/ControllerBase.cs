using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GM.Utilities;
using Newtonsoft.Json;
using RedCdn.ClassRoom.BMS.Services;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    public abstract class ControllerBase : Controller {
        private readonly ConcurrentDictionary<Type, object> services = new ConcurrentDictionary<Type, object>();
        protected TService GetService<TService>()
            where TService : class, new() {
            return (TService)services.GetOrAdd(typeof(TService), serviceType => {
                return new TService();
            });
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing) {
                services.Clear();
            }
        }

        protected override void OnException(ExceptionContext filterContext) {
            if (filterContext.IsChildAction) {
                return;
            }


            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled) {
            //    return;
            //}

            Exception exception = filterContext.Exception;

            Logger.WriteErrorFmt("RedCdn.ClassRoom.BMS", exception, "执行[{0}]控制器的[{1}]方法遇到错误",
                filterContext.RouteData.Values["controller"], filterContext.RouteData.Values["action"]);


            if (new HttpException(null, exception).GetHttpCode() != 500) {
                return;
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.Result = new JsonResult {
                    Data = new {
                        title = "错误",
                        message = "系统错误，请稍候再试。"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            } else {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                HandleErrorInfo model = new HandleErrorInfo(exception, controllerName, actionName);
                filterContext.Result = new ViewResult {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }


        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            return new NewJsonResult {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        protected SitePassport Passport { get; private set; }

        protected override void Initialize(RequestContext requestContext) {
            base.Initialize(requestContext);

            this.Passport = GetService<PassportService>().GetCurrentPassport(requestContext.HttpContext.User) ?? SitePassport.Empty;

            ViewData["accountType"] = Passport.AccountType;
        }

        class NewJsonResult : JsonResult {
            public override void ExecuteResult(ControllerContext context) {
                if (context == null) {
                    throw new ArgumentNullException("context");
                }

                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                    String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                    throw new InvalidOperationException("若要允许 GET 请求，请将 JsonRequestBehavior 设置为 AllowGet。");
                }

                HttpResponseBase response = context.HttpContext.Response;

                if (!String.IsNullOrEmpty(ContentType)) {
                    response.ContentType = ContentType;
                } else {
                       response.ContentType = "application/json";
                }

                if (ContentEncoding != null) {
                    response.ContentEncoding = ContentEncoding;
                } else {
                    response.ContentEncoding = Encoding.UTF8;
                }

                if (Data != null) {
                    JsonSerializerSettings js = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

                    var result = JsonConvert.SerializeObject(Data, js);
                    response.Write(result);

                    Logger.WriteDebugingFmt("RedCdn.ClassRoom.BMS", "返回给客户端Json:{0}", result);
                }
            }
        }
    }
}

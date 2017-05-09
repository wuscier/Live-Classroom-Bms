using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using RedCdn.ClassRoom.BMS.Services;
using Redcdn.ClassRoom.Facade;
using WebGrease;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    public class AccountController : ControllerBase
    {
        //
        // GET: /Account/

        public ActionResult Logon()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name)) {

                FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().Load();

                return RedirectToAction("index", "Classroom");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logon(LogonModel model, string returnUrl)
        {
            if (ModelState.IsValid) {
                try
                {
                    var user = FacadeFactory.Instance.Get<ISystemManager>().Login(model.UserName, model.Password);
                    AuthenticationService.Instance.SignIn(user, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") &&
                        !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        FacadeFactory.Instance.Get<Redcdn.ClassRoom.Facade.ICacheManager>().Load();

                        Logger.WriteInfoFmt(LogCatalogs.OperationHits, "用户{0}登录成功", model.UserName);

                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (user.AccountType==1)//校管理员
                            return RedirectToAction("index", "classroom");
                        else
                            return RedirectToAction("Edit", "SystemManager");
                    }

                } catch (Exception ex) {

                    ViewBag.LoginInfo = ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationService.Instance.SignOut();

            return RedirectToAction("logon");
        }
    }
}

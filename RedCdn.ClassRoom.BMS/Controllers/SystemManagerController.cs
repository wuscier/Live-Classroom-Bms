using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using RedCdn.ClassRoom.BMS.Services;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
   //[Authorize(Users = "admin")]
    public class SystemManagerController : ControllerBase
    {
        //
        // GET: /SystemManager/

        [Authorize(Users = "admin")]
        public ActionResult Edit()
        {
            ViewBag.TitleDesc = "系统管理";
            try {
                var dto = FacadeFactory.Instance.Get<ISystemManager>().GetSchoolManager();
                if (dto == null)
                    return View();

                Mapper.CreateMap<SystemManagerDto, SystemManagerModule>().ConvertUsing<NumberPoolsConverter>();
                var viewModel = Mapper.Map<SystemManagerDto, SystemManagerModule>(dto);
                return View(viewModel);
            } catch (Exception) {
                throw;
            }
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult EditInfo(SystemManagerModule model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var listNum = new List<string>();
                    if (model.NumberPools != null)
                    {
                        if (model.NumberPools.Contains("\n"))
                            listNum = model.NumberPools.Split('\n').ToList();
                        else
                            listNum.Add(model.NumberPools);
                    }

                    var sysManager = new SystemManagerDto()
                    {
                       
                        SchoolManagerAccount = model.SchoolManagerAccount,
                        SchoolManagerPassWord = model.SchoolManagerPassWord,
                        SchoolManagerName = model.SchoolManagerName,
                        NumberPool = listNum.ToArray()
                    };

                    if (model.Id==null)
                        FacadeFactory.Instance.Get<ISystemManager>().Create(sysManager);
                    else
                    {
                        sysManager.Id = model.Id.Value;
                        FacadeFactory.Instance.Get<ISystemManager>().Update(sysManager);
                    }

                    return Json(new { success = true, message = "修改成功。" });
                }

                return Json(new { success = false, message = "提交的数据不正确。" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult MyInfo()
        {
            var dto = FacadeFactory.Instance.Get<ISystemManager>().GetById(Passport.UserId);
            if (dto != null) {
                var model = new PersonalModel() {
                    Id = dto.Id,
                    ManagerAccount = dto.SchoolManagerAccount,
                    ManagerName = dto.SchoolManagerName,
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ModifyInfo(PersonalModel model)
        {
            if (ModelState.IsValid) {

                try
                {
                    FacadeFactory.Instance.Get<ISystemManager>().ModifyPersonalInfo(Passport.UserId, model.ManagerName, model.NewPassWord, model.ConfirmPassWord);

                    return Json(new { success = true, message = "修改个人信息成功。" });
                }
                catch (Exception ex)
                {
                    Logger.WriteError(LogCatalogs.OperationHits, "修改个人信息失败",ex);
                    return Json(new { success = false, message = "修改个人信息失败。" });
                }
            }
            return Json(new { success = false, message = "修改个人信息失败。" });
        }

        public ActionResult Logout() {
            AuthenticationService.Instance.SignOut();

            return RedirectToAction("logon");
        }
    }
}

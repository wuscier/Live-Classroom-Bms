using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class CurriculumNameController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;

        public ActionResult Index(int page = 0, int count = 0)
        {
            var pagep = CreateQueryPageParameter(page, pageSize);
            return GetIndexView("index", pagep);
        }

        ActionResult GetIndexView(string viewname, ContentQueryPageParameter pagep) {
            var index = new IndexPaging<CurriculumNameModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            var queryList = FacadeFactory.Instance.Get<ICurriculumNameManager>().GetList(pagep);
            QueryExtracted(index.Paging, queryList);
            index.Paging.TotalCount = queryList.TotalCount;

            return View(viewname, index);
        }

        public ActionResult Query(int page = 0, int count = 0) {
            var pagep = CreateQueryPageParameter(page, count);

            var paging = new PagingQueryResultDto<CurriculumNameModel>();
            var queryList = FacadeFactory.Instance.Get<ICurriculumNameManager>().GetList(pagep);
            QueryExtracted(paging, queryList);
            paging.TotalCount = queryList.TotalCount;

            return PartialView("_CurriculumNameList", paging);
        }

        [HttpPost]
        public ActionResult Create(CurriculumNameModel model) {
            ViewBag.Title = "创建课程";
            try
            {
                ViewBag.Page = 0;
                var viewModel = ConvertToDto(model);
                FacadeFactory.Instance.Get<ICurriculumNameManager>().Create(viewModel);
                return Json(new { success = true, message = "添加成功" });
            }
            catch (Exception ex)
            {
                Logger.WriteError(LogCatalogs.OperationHits, "添加课程失败", ex);
                return Json(new { success = true, message = "添加课程失败" }); 
            }
        }

        [HttpPost]
        public ActionResult Edit(CurriculumNameModel model, int page = 0) {

            try
            {
                ViewBag.Page = page;
                FacadeFactory.Instance.Get<ICurriculumNameManager>().Update(ConvertToDto(model));
                return Json(new { success = true, message = "编辑成功" });
            }
            catch (Exception ex)
            {
                Logger.WriteError(LogCatalogs.OperationHits, "更新课程失败", ex);
                return Json(new { success = false, message = "编辑失败" });
            }
        }

        public ActionResult Edit(int id=0, int page=0) {

            ViewBag.Page = page;
            if (id == 0)
            {
                ViewBag.TitleDesc = "创建课程";
                ViewBag.IsCreate = true;
            }
            else
            {
                ViewBag.TitleDesc = "编辑课程";
                ViewBag.IsCreate = false;
            }

            var dto = FacadeFactory.Instance.Get<ICurriculumNameManager>().Get(id);
            if (dto == null)
                return View(new CurriculumNameModel());

            var viewModel = ConvertToModel(dto);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            try {
                FacadeFactory.Instance.Get<ICurriculumNameManager>().Delete(id);
                return Json(new { success = true, message = "删除课程成功" });
                 
            } catch (Exception ex) {
                Logger.WriteError(LogCatalogs.OperationHits, "删除课程失败", ex);
                return Json(new { success = false, message = "删除课程失败" });
            }
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count, OrderFields = new List<string> { "id" }, OrderFlag = true };
        }

        int GetInt(int num) {
            return num < 0 ? 0 : num;
        }

        private void QueryExtracted(PagingQueryResultDto<CurriculumNameModel> paging, PagingQueryResultDto<CurriculumNameDto> queryList) {
            queryList.Result.ForEach(r => {
                var viewModel = ConvertToModel(r);
                paging.Result.Add(viewModel);
            });
        }

        CurriculumNameDto ConvertToDto(CurriculumNameModel model) {
            var result = new CurriculumNameDto();
            AutoMapperWrapper.Instance.Map<CurriculumNameModel, CurriculumNameDto>(model, result);
            return result;
        }

        CurriculumNameModel ConvertToModel(CurriculumNameDto model)
        {
            var result = new CurriculumNameModel();
            AutoMapperWrapper.Instance.Map<CurriculumNameDto, CurriculumNameModel>(model, result);
            return result;
        }

        private JsonResult JsonResult(int code, string msg, bool isallowGet) {

            if (isallowGet)
                return Json(new { status = code, message = msg }, JsonRequestBehavior.AllowGet);

            return Json(new { status = code, message = msg });
        }
    }
}

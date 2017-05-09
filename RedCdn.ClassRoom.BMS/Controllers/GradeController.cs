using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class GradeController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;

        public ActionResult Index(int page=0,int count=0)
        {
            var pagep = CreateQueryPageParameter(page, pageSize);

            return GetIndexView("index", pagep);
        }

        ActionResult GetIndexView(string viewname, ContentQueryPageParameter pagep)
        {
            var index = new IndexPaging<GradeModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            var queryList = FacadeFactory.Instance.Get<IGradeManager>().GetList(pagep);
            QueryExtracted(index.Paging, queryList);
            index.Paging.TotalCount = queryList.TotalCount;

            return View(viewname, index);
        }

        public ActionResult Query(int page = 0, int count = 0) {
            var pagep = CreateQueryPageParameter(page, count);

            var paging = new PagingQueryResultDto<GradeModel>();
            var queryList = FacadeFactory.Instance.Get<IGradeManager>().GetList(pagep);
            QueryExtracted(paging, queryList);
            paging.TotalCount = queryList.TotalCount;

            return PartialView("_GradeList", paging);
        }

        [HttpPost]
        public ActionResult Create(GradeModel model)
        {
            ViewBag.Title = "创建年级";

            try
            {
                ViewBag.Page = 0;

                var viewModel = Mapper.Map<GradeModel, GradeDto>(model);
                FacadeFactory.Instance.Get<IGradeManager>().Create(viewModel);
                return Json(new { success = true, message = "添加成功" });
            }
            catch (Exception ex)
            {
                Logger.WriteError(LogCatalogs.OperationHits, "添加失败", ex);
                return Json(new { success = false, message = "添加失败" + ex.Message });
            }
        }

         [HttpPost]
        public ActionResult Edit(GradeModel model,int page=0)
        {
            try {
                ViewBag.Page = page;

                FacadeFactory.Instance.Get<IGradeManager>().Update(ConvertToDto(model));

                return Json(new { success = true, message = "编辑成功" });
            } catch (Exception ex) {
                Logger.WriteError(LogCatalogs.OperationHits, "更新年级失败", ex);
                return Json(new { success = false, message = "编辑失败,"+ex.Message });
            }
        }

         public ActionResult Edit(int id=0, int page = 0) {
             ViewBag.Page = page;
             if (id == 0)
             {
                 ViewBag.TitleDesc = "创建年级";
                 ViewBag.IsCreate = true;
             }
             else
             {
                 ViewBag.TitleDesc = "编辑年级";
                 ViewBag.IsCreate = false;
             }

             var dto = FacadeFactory.Instance.Get<IGradeManager>().Get(id);
             if (dto == null)
                 return View(new GradeModel());

             var viewModel = ConvertToModel(dto);
             return View(viewModel);
         }

         [HttpPost]
        public ActionResult Delete(int id)
        {
             try
             {
                 FacadeFactory.Instance.Get<IGradeManager>().Delete(id);
                 return Json(new { success = true, message = "删除成功" });
             }
             catch (Exception ex)
             {
                 Logger.WriteError(LogCatalogs.OperationHits, "删除年级失败", ex);
                 return Json(new { success = false, message = ex.Message });
             }
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count, OrderFields = new List<string> { "id" }, OrderFlag = true };
        }

        private void QueryExtracted(PagingQueryResultDto<GradeModel> paging, PagingQueryResultDto<GradeDto> queryList) {
            queryList.Result.ForEach(r => {
                var viewModel = Mapper.Map<GradeDto, GradeModel>(r);
                paging.Result.Add(viewModel);
            });
        }

        private JsonResult JsonResult(int code, string msg, bool isallowGet) {

            if (isallowGet)
                return Json(new { status = code, message = msg }, JsonRequestBehavior.AllowGet);

            return Json(new { status = code, message = msg });
        }

        int GetInt(int num) {
            return num < 0 ? 0 : num;
        }

        GradeDto ConvertToDto(GradeModel model) {
            var result = new GradeDto();
            AutoMapperWrapper.Instance.Map<GradeModel, GradeDto>(model, result);
            return result;
        }

        GradeModel ConvertToModel(GradeDto model) {
            var result = new GradeModel();
            AutoMapperWrapper.Instance.Map<GradeDto, GradeModel>(model, result);
            return result;
        }
    }
}

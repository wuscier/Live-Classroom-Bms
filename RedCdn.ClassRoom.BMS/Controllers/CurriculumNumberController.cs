using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class CurriculumNumberController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;
        public ActionResult Index(int page = 0, int count = 0)
        {
            var pagep = CreateQueryPageParameter(page, pageSize);
            return GetIndexView("index", pagep);
        }

        ActionResult GetIndexView(string viewname, ContentQueryPageParameter pagep) {
            var index = new IndexPaging<CurriculumNumberModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            var queryList = FacadeFactory.Instance.Get<ICurriculumNumberManager>().GetList(pagep);
            QueryExtracted(index.Paging, queryList);
            index.Paging.TotalCount = queryList.TotalCount;

            return View(viewname, index);
        }

        public ActionResult Query(int page = 0, int count = 0) {
            var pagep = CreateQueryPageParameter(page, count);

            var paging = new PagingQueryResultDto<CurriculumNumberModel>();
            var queryList = FacadeFactory.Instance.Get<ICurriculumNumberManager>().GetList(pagep);
            QueryExtracted(paging, queryList);
            paging.TotalCount = queryList.TotalCount;

            return PartialView("_CurriculumNumberList", paging);
        }

        [HttpPost]
        public ActionResult Create(CurriculumNumberModel model) {

            try
            {
                DateTime classStarttime = Convert.ToDateTime(string.Format("{0} {1}", "2001-1-1", model.StarTime.GetDateTimeFormats('t')[0]));
                model.StarTime = classStarttime;

                if (model.Duration == 0)
                    model.Duration = 45;
                var viewModel = ConvertToDto(model);
                FacadeFactory.Instance.Get<ICurriculumNumberManager>().Create(viewModel);

                return Json(new { success = true, message = "添加成功" });
            }
            catch (Exception ex)
            {
                Logger.WriteError(LogCatalogs.OperationHits, "添加课序失败", ex);
                return Json(new { success = false, message = "添加失败" });
            }
        }

        [HttpPost]
        public ActionResult Edit(CurriculumNumberModel model, int page = 0) {
            try
            {
                ViewBag.Page = page;
                DateTime classStarttime = Convert.ToDateTime(string.Format("{0} {1}", "2001-1-1", model.StarTime.GetDateTimeFormats('t')[0]));
                model.StarTime = classStarttime;

                if (model.Duration == 0)
                    model.Duration = 45;
                FacadeFactory.Instance.Get<ICurriculumNumberManager>().Update(ConvertToDto(model));

                return Json(new { success = true, message = "编辑成功" });
            }
            catch (Exception ex)
            {
                Logger.WriteError(LogCatalogs.OperationHits, "更新课序失败", ex);
                return Json(new { success = false, message = "编辑失败" });
            }
        }

        public ActionResult Edit(int id=0,int page=0)
        {
            ViewBag.Page = page;
            if (id == 0)
            {
                ViewBag.TitleDesc = "创建课序";
                ViewBag.IsCreate = true;
            }
            else
            {
                ViewBag.TitleDesc = "编辑课序";
                ViewBag.IsCreate = false;
            }

            var dto = FacadeFactory.Instance.Get<ICurriculumNumberManager>().Get(id);
            if (dto == null)
                return View(new CurriculumNumberModel());

            var viewModel = ConvertToModel(dto);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            try {
                FacadeFactory.Instance.Get<ICurriculumNumberManager>().Delete(id);
                return Json(new { success = true, message = "删除课序成功" });
            } catch (Exception ex) {
                return Json(new { success = false, message = "课序已绑定课表,不允许删除" });
            }
        }

        int GetInt(int num) {
            return num < 0 ? 0 : num;
        }

        private void QueryExtracted(PagingQueryResultDto<CurriculumNumberModel> paging, PagingQueryResultDto<CurriculumNumberDto> queryList) {
            queryList.Result.ForEach(r => {
                var viewModel = ConvertToModel(r);
                paging.Result.Add(viewModel);
            });
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count, OrderFields = new List<string> { "id" }, OrderFlag = true };
        }

        CurriculumNumberDto ConvertToDto(CurriculumNumberModel model) {
            var result = new CurriculumNumberDto();
            AutoMapperWrapper.Instance.Map<CurriculumNumberModel, CurriculumNumberDto>(model, result);
            return result;
        }

        CurriculumNumberModel ConvertToModel(CurriculumNumberDto model) {
            var result = new CurriculumNumberModel();
            AutoMapperWrapper.Instance.Map<CurriculumNumberDto, CurriculumNumberModel>(model, result);
            return result;
        }

        private JsonResult JsonResult(int code, string msg, bool isallowGet) {

            if (isallowGet)
                return Json(new { status = code, message = msg }, JsonRequestBehavior.AllowGet);

            return Json(new { status = code, message = msg });
        }
    }
}

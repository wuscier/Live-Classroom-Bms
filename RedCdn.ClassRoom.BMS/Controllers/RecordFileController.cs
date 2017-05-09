using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class RecordFileController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;
        public ActionResult Index(int page = 0, int count = 0, int weekdayid = 0, int mainclassroomid = 0, int curriculumnumberid = 0, int gradeid = 0, int curriculumnameid = 0)
        {
            var curp = CreateRecordFileQueryParamter(weekdayid, mainclassroomid, curriculumnumberid, gradeid, curriculumnameid);

            var pagep = CreateQueryPageParameter(page, pageSize);

            return GetIndexView("~/Views/RecordFile/index.cshtml",curp,pagep);
        }

        public ActionResult Query(int page = 0, int count = 0, int weekdayid = 0, int mainclassroomid = 0,int curriculumnumberid = 0, int gradeid = 0, int curriculumnameid = 0)
        {
            var curp = CreateRecordFileQueryParamter(weekdayid, mainclassroomid, curriculumnumberid, gradeid, curriculumnameid);
            var pagep = CreateQueryPageParameter(page, count);

            var paging = new PagingQueryResultDto<RecordFileModel>();
            var queryList = FacadeFactory.Instance.Get<IRecordFileManager>().GetList(curp, pagep);
            queryList.Result.ForEach(r => {
                var viewModel = Mapper.Map<RecordFileDto, RecordFileModel>(r);
                paging.Result.Add(viewModel);

            });
            paging.TotalCount = queryList.TotalCount;

            return PartialView("~/Views/RecordFile/_RecordFileList.cshtml", paging);
        }

        public ActionResult Detail(int id,int page=0)
        {
            ViewBag.Title = "查看录制课堂";
            ViewBag.Page = GetInt(page);

            var dto= FacadeFactory.Instance.Get<IRecordFileManager>().Get(id);
            var viewModel = Mapper.Map<RecordFileDto, RecordFileModel>(dto);
            ViewBag.PlayUrl = viewModel.FilePlayUrl;

            return View("Detail", viewModel);
        }

        public ActionResult DownLoadFile(string url)
        {
            try
            {
                var stream = new WebClient().OpenRead(url);
                return File(stream, "application/octet-stream", Path.GetFileName(url));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "下载录制视频失败:{0}", url);
                return new EmptyResult();
            }
           
          //  string path = AppDomain.CurrentDomain.BaseDirectory + "file\\";
          //  return File(new FileStream(path + "千与千寻.mp4", FileMode.Open), "application/octet-stream", "千与千寻.mp4");
        }

       [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                FacadeFactory.Instance.Get<IRecordFileManager>().Delete(id);
                Logger.WriteInfoFmt(LogCatalogs.OperationHits, "删除录制文件redoreId:{0}", id);
               return Json(new { success = true, message = "删除成功。" });
            }
            catch (Exception ex) 
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits,ex, "删除录制文件redoreId:{0},失败", id);
                return Json(new { success = false, message = "删除失败。" }); 
            }
        }

        PagingQueryResultDto<RecordFileModel> QueryRecordFile(ContentQueryPageParameter parameter)
        {
            var list = FacadeFactory.Instance.Get<IRecordFileManager>().GetList(null, parameter);
            return ConverToPagingQueryModel(list);
        }

        PagingQueryResultDto<RecordFileModel> ConverToPagingQueryModel(PagingQueryResultDto<RecordFileDto> arg)
        {
            var result = new PagingQueryResultDto<RecordFileModel>() {TotalCount = arg.TotalCount};

            arg.Result.ForEach(x => {
                var viewModel = Mapper.Map<RecordFileDto, RecordFileModel>(x);
                result.Result.Add(viewModel);
            });

            return result;
        }

        int GetInt(int num) {
            return num < 0 ? 0 : num;
        }

        ActionResult GetIndexView(string viewName, ComplexQueryParameter query, ContentQueryPageParameter pagep) {
            var index = new IndexPaging<RecordFileModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            var queryList= FacadeFactory.Instance.Get<IRecordFileManager>().GetList(query, pagep);
            queryList.Result.ForEach(r =>
            {
                var viewModel = Mapper.Map<RecordFileDto, RecordFileModel>(r);
                index.Paging.Result.Add(viewModel);

            });
            index.Paging.TotalCount = queryList.TotalCount;

            return View(viewName, index);
        }

        ComplexQueryParameter CreateRecordFileQueryParamter(int weekdayid = 0, int mainclassroomid = 0, int curriculumnumberid = 0, int gradeid = 0, int curriculumnameid = 0) {
            return new ComplexQueryParameter() {
                CurriculumNameId = curriculumnameid,
                CurriculumNumberId = curriculumnumberid,
                GradeId = gradeid,
                MainClassRoomId = mainclassroomid,
                WeekDayId = weekdayid
            };
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count, OrderFields = new List<string> { "id" }, OrderFlag = true };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Redcdn.ClassRoom.Facade;
using RedCdn.ClassRoom.BMS.Models;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class CurriculumController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;
        //
        // GET: /Curriculum/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">分页</param>
        /// <param name="count">查询个数</param>
        /// <param name="weekdayid">星期id</param>
        /// <param name="mainclassroomid">主讲教室id</param>
        /// <param name="curriculumnumberid">课序id</param>
        /// <param name="gradeid">年级id</param>
        /// <param name="curriculumnameid">课程名id</param>
        /// <returns></returns>
        public ActionResult Index(int page=0,int count=0,int weekdayid=0,int mainclassroomid=0,int curriculumnumberid=0,int gradeid=0,int curriculumnameid=0)
        {
            var curp = CreateCurriculumQueryParamter(weekdayid, mainclassroomid, curriculumnumberid, gradeid, curriculumnameid);
            var pagep = CreateQueryPageParameter(page, GetInt(pageSize));

            return GetIndexView(curp, pagep);
        }

        public ActionResult Query(int page=0,int count=0,int weekdayid=0,int mainclassroomid=0,int curriculumnumberid=0,int gradeid=0,int curriculumnameid=0)
        {
            var curp = CreateCurriculumQueryParamter(weekdayid, mainclassroomid, curriculumnumberid, gradeid, curriculumnameid);
            var pagep = CreateQueryPageParameter(page, count);

            return PartialView("~/Views/Curriculum/_CurriculumList.cshtml", FacadeFactory.Instance.Get<ICurriculumManager>().GetList(curp, pagep));            
        }

        public ActionResult Create() 
        {
            ViewBag.Title = "创建课表";
            ViewBag.Page = 0;
            ViewBag.IsCreate = true;

            var model = GetCurriculum(0);
            return View("~/Views/Curriculum/Edit.cshtml",model);
        }

        public ActionResult Edit(int id, int page = 0) 
        {
            ViewBag.Title = "修改课表";
            ViewBag.Page = GetInt(page);
            ViewBag.IsCreate = false;

            var model = GetCurriculum(id);
            return View("~/Views/Curriculum/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateSubmit(CurriculumDto cur) 
        {
            return CreateCurriculum(cur);
        }

        [HttpPost]
        public ActionResult EditSubmit(CurriculumDto cur, int page = 0) 
        {
            return UpdateCurriculum(cur);
        }

        public ActionResult Delete(int id) 
        {
            return DeleteCurriculunm(id);
        }

        ActionResult DeleteCurriculunm(int id)
        {
            FacadeFactory.Instance.Get<ICurriculumManager>().Delete(id);
            return CreateJsonResult(null);
        }

        JsonResult CreateCurriculum(CurriculumDto cur) 
        {
            try
            {
                FacadeFactory.Instance.Get<ICurriculumManager>().Create(cur);
                return CreateJsonResult(null);
            }
            catch (Exception ex) {
                return CreateJsonResult(ex);
            }
        }

        JsonResult CreateJsonResult(Exception ex)
        {
            if (ex == null)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        success = true,
                        message = ""
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        success = false,
                        message = ex.Message
                    }
                };
            }
        }

        JsonResult UpdateCurriculum(CurriculumDto cur) 
        {
            try
            {
                FacadeFactory.Instance.Get<ICurriculumManager>().Update(cur);
                return CreateJsonResult(null);
            }
            catch (Exception ex) {
                return CreateJsonResult(ex);
            }
        }

        ActionResult RedirectIndex(int page)
        {
            return Redirect(Url.Action("index", "curriculum", new { page=page,count=pageSize}));
        }

        EditCurriculumModel GetCurriculum(int id) 
        {
            var curriculum = FacadeFactory.Instance.Get<ICurriculumManager>().Get(id);
            if (curriculum == null)
                curriculum = new CurriculumDto();

            var model = new EditCurriculumModel();
            model.Curriculum = curriculum;
            model.CurriculumEidtInfo = CurriculumEidtInfo.Load();

            return model;
        }

        int GetInt(int num)
        {
            return num < 0 ? 0 : num;
        }

        ActionResult GetIndexView(ComplexQueryParameter curp, ContentQueryPageParameter pagep) 
        {
            var model = QueryCurriculumDto(curp, pagep);
            return View("~/Views/Curriculum/index.cshtml", model);
        }

        IndexPaging<CurriculumDto> QueryCurriculumDto(ComplexQueryParameter curparamter, ContentQueryPageParameter paramter)
        {
            var index = new IndexPaging<CurriculumDto>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            index.Paging = FacadeFactory.Instance.Get<ICurriculumManager>().GetList(curparamter, paramter);
            return index;
        }

        ComplexQueryParameter CreateCurriculumQueryParamter(int weekdayid = 0, int mainclassroomid = 0, int curriculumnumberid = 0, int gradeid = 0, int curriculumnameid = 0) 
        {
            return new ComplexQueryParameter() { CurriculumNameId = curriculumnameid,
                CurriculumNumberId = curriculumnumberid,
                GradeId = gradeid,
                MainClassRoomId = mainclassroomid,
                WeekDayId=weekdayid};
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page,int count) 
        {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count,OrderFields = new List<string> { "id" }, OrderFlag = true };
        }
    }  
}

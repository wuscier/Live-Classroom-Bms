using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]
    public class ClassroomController : ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;

        #region httpmethod
        // GET: /Classroom/
        public ActionResult Index(int page=0 , int count=0 )
        {
            return GetIndexView(CreateQueryPageParameter(GetInt(page), GetInt(pageSize)));
        }

        public ActionResult Query(int page=0 , int count=0 )
        {
            return GetQueryPageView(CreateQueryPageParameter(GetInt(page), GetInt(count)));
        }

        public ActionResult Create()
        {
            ViewBag.TitleDesc = "创建教室";
            ViewBag.CurrentPage = 0;
            ViewBag.Page = 0;
            ViewBag.IsCreate = true;
            return GetDetailView(0, true);
        }

        public ActionResult Detail(int id, int page)
        {
            ViewBag.TitleDesc = "查看教室";
            ViewBag.CurrentPage = GetInt(page);
            return GetDetailView(id, false);
        }

        public ActionResult Edit(int id,int page) 
        {
            ViewBag.TitleDesc = "修改教室";
            ViewBag.CurrentPage = page;
            ViewBag.Page = GetInt(page);
            ViewBag.IsCreate = false;

            var model = RemoteChannelService.Instance.GetById(id);
            if (model != null)
            {
                var channel = new RemoteChannelModel();
                AutoMapperWrapper.Instance.Map(model, channel);
                ViewBag.channel = channel;
            }

            return GetDetailView(id,true);
        }

        [HttpPost]
        public ActionResult EditSubmit(SchoolRoomModel roommodel, RemoteChannelModel channel, int page = 0) 
        {
            UpdateRoom(roommodel);
            if (roommodel.Id > 0)
            {
                channel.Id = roommodel.Id;
                EnsureCreateOrUpdateChannel(channel);
            }
            return Redirect("/classroom/index?page=" + GetInt(page) + "&count=" + pageSize);
        }

        [HttpPost]
        public ActionResult CreateSubmit(SchoolRoomModel roommodel, RemoteChannelModel channel)
        {
            var dto = AddSchoolRoom(roommodel);
            if (dto?.Id > 0)
            {
                channel.Id = dto.Id;
                EnsureCreateOrUpdateChannel(channel);
            }
            return Redirect("/classroom/index");
        }

        void EnsureCreateOrUpdateChannel(RemoteChannelModel channel)
        {
            try
            {
                var model = RemoteChannelService.Instance.GetById(channel.Id);
                if (model != null)
                {
                    model.PushStreamUrl = channel.PushStreamUrl;
                    model.PlayStreamUrl = channel.PlayStreamUrl;
                    RemoteChannelService.Instance.Update(model);
                }
                else
                {
                    model = new RemoteChannel();
                    AutoMapperWrapper.Instance.Map(channel, model);
                    RemoteChannelService.Instance.Create(model);
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }

        [HttpPost]
        public ActionResult Delete(int id) 
        {
            return DeleteRoom(id);
        }
        #endregion

        #region 模型curd
        int GetInt(int num)
        {
            return num < 0 ? 0 : num;
        }

        ActionResult DeleteRoom(int id) 
        {
            try
            {
                FacadeFactory.Instance.Get<ISchoolRoomManager>().Delete(id);

                return CreateJsonResult(null);
            }
            catch (Exception ex) {
                return CreateJsonResult(ex);
            }
        }

        JsonResult CreateJsonResult(Exception ex) {
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

        void UpdateRoom(SchoolRoomModel roommodel)
        {
            roommodel.SchoolRoomName = roommodel.SchoolRoomName.Trim();
            FacadeFactory.Instance.Get<ISchoolRoomManager>().Update(ConvertToDto(roommodel));
        }

        IndexPaging<SchoolRoomModel> QuerySchoolRoom(ContentQueryPageParameter paramter) 
        {
            var list = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetAll(paramter);
            return ConverToPagingQuerySchoolModel(list);
        }

        List<NumberPoolDto> GetNotAllocatedNumPool() 
        {
            return FacadeFactory.Instance.Get<INumberPoolManager>().GetNotAllocatedNumPool();
        }

        SchoolRoomModel GetSchoolRoomDetail(int id) 
        {
            var room = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetSchoolRoomById(id);
            if (room == null)
                return new SchoolRoomModel();

            return ConvertToModel(room);
        }

        ActionResult GetDetailView(int id,bool isedit)
        {
            ViewBag.NotAllcatedNumPool = GetNotAllocatedNumPool();
            ViewBag.Isedit = isedit;
           
            var room = GetSchoolRoomDetail(id);
            return View("~/Views/Classroom/Edit.cshtml",room);
        }

        SchoolRoomDto AddSchoolRoom(SchoolRoomModel roommodel)
        {
            roommodel.SchoolRoomName = roommodel.SchoolRoomName.Trim();
            return FacadeFactory.Instance.Get<ISchoolRoomManager>().Create(ConvertToDto(roommodel));
        }

        ActionResult GetIndexView(ContentQueryPageParameter paramter)
        {
            return View("~/Views/Classroom/Index.cshtml", QuerySchoolRoom(paramter));
        }

        ActionResult GetQueryPageView(ContentQueryPageParameter paramter)
        {
            return PartialView("~/Views/Classroom/_ClassroomList.cshtml", QuerySchoolRoom(paramter));
        }

        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) 
        {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { OrderFields = new List<string> { "id"}, OrderFlag = true, PageSize = count, StartIndex = page };
        }

        IndexPaging<SchoolRoomModel> ConverToPagingQuerySchoolModel(PagingQueryResultDto<SchoolRoomDto> arg)
        {
           // var result = new PagingQueryResultDto<SchoolRoomModel>() { TotalCount = arg.TotalCount  };

            var index = new IndexPaging<SchoolRoomModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();

            arg.Result.ForEach(x => {
                var tmp = new SchoolRoomModel();
                index.Paging.Result.Add(AutoMapperWrapper.Instance.Map<SchoolRoomDto, SchoolRoomModel>(x, tmp));
            });
            index.Paging.TotalCount = arg.TotalCount;

            return index;
        }

        void Test() 
        {
            var source = new PagingQueryResultDto<SchoolRoomDto>();
            source.Result.Add(new SchoolRoomDto() { Id=1,PlayStreamUrl="url"});
            
            
        }

        SchoolRoomModel ConvertToModel(SchoolRoomDto dto)
        {
            var result = new SchoolRoomModel();
            AutoMapperWrapper.Instance.Map<SchoolRoomDto, SchoolRoomModel>(dto, result);
            return result;
        }

        SchoolRoomDto ConvertToDto(SchoolRoomModel model)
        {
            var result = new SchoolRoomDto();
            AutoMapperWrapper.Instance.Map<SchoolRoomModel, SchoolRoomDto>(model, result);
            return result;
        }
        #endregion
    }
}

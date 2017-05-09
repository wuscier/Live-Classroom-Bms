using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    [Authorize]

    public class LiveClassRoomController :ControllerBase
    {
        int pageSize = SettingConfig.Instance.PageSize;

        public ActionResult Index(int page = 0, int count = 0)
        {
            var pagep = CreateQueryPageParameter(page, pageSize);

            return GetIndexView("index",  pagep);
        }

        ActionResult GetIndexView(string viewname,ContentQueryPageParameter pagep)
        {
            var index = new IndexPaging<LiveClassroomModel>();
            index.CurriculumEidtInfo = CurriculumEidtInfo.Load();
            var queryList = FacadeFactory.Instance.Get<ILiveClassRoomManager>().GetList(pagep);
            QueryExtracted(index.Paging, queryList);
            index.Paging.TotalCount = queryList.TotalCount;

            return View(viewname, index);
        }

     
        public ActionResult Query(int page = 0, int count = 0) {
            var pagep = CreateQueryPageParameter(page, count);

            var paging = new PagingQueryResultDto<LiveClassroomModel>();
            var queryList = FacadeFactory.Instance.Get<ILiveClassRoomManager>().GetList(pagep);
            QueryExtracted(paging, queryList);
            paging.TotalCount = queryList.TotalCount;

            return PartialView("_LiveClassRoomList", paging);
        }

        public ActionResult Detail(int id, int page = 0)
        {
            try
            {
                ViewBag.Page = GetInt(page);
                ViewBag.TmpId = id;

                var dto = FacadeFactory.Instance.Get<ILiveClassRoomManager>().Get(id);
                var viewModel = Mapper.Map<LiveClassRoomDto, LiveClassroomModel>(dto);

               // ViewBag.PlayUrl = viewModel.LiveStreamUrl;
                string[] tmpplayurl= viewModel.LiveStreamUrl.Split(',');

                if (tmpplayurl.Length>1)
                    ViewData["playurl-pc"] = tmpplayurl[0];
                else
                    ViewData["playurl-pc"] = viewModel.LiveStreamUrl;

                return View("Detail", viewModel);
            }
            catch (Exception ex)
            {
                return View("Detail", new LiveClassroomModel());
            }
        }


      


        ContentQueryPageParameter CreateQueryPageParameter(int page, int count) {
            ViewBag.PSize = pageSize;
            return new ContentQueryPageParameter() { StartIndex = page, PageSize = count, OrderFields = new List<string> { "id" }, OrderFlag = true };
        }

        private void QueryExtracted(PagingQueryResultDto<LiveClassroomModel> paging, PagingQueryResultDto<LiveClassRoomDto> queryList) {
            queryList.Result.ForEach(r => {
                var viewModel = Mapper.Map<LiveClassRoomDto, LiveClassroomModel>(r);
                paging.Result.Add(viewModel);

            });
        }



        int GetInt(int num) {
            return num < 0 ? 0 : num;
        }
    }
}

    

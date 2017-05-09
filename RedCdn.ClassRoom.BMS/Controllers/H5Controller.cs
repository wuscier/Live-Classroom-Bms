using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers {
    public class H5Controller : ControllerBase {

        public ActionResult LivePlay(int id) {
            string iosplay_templet = "{$ios_playurl$}";
            string pcplay_templet = "{$pc_playurl$}";

            try {
                string html = ReadH5Templet();

                var dto = FacadeFactory.Instance.Get<ILiveClassRoomManager>().Get(id);
                var viewModel = Mapper.Map<LiveClassRoomDto, LiveClassroomModel>(dto);

                ViewBag.PlayUrl = viewModel.LiveStreamUrl;

                string viewhtml = html.Replace(iosplay_templet, viewModel.LiveStreamUrl + ".m3u8")
                    .Replace(pcplay_templet, viewModel.LiveStreamUrl+".flv");

                //string debugurl = "http://192.168.12.177:20017/hls.cms.com/i98878b6e79fd450da9c20c4c2df68528.m3u8";
                //string viewhtml = html.Replace(iosplay_templet, debugurl)
                //    .Replace(pcplay_templet, debugurl);

                return Content(viewhtml);
            } catch (Exception ex) {
                return View("~/Views/logo/index.cshtml");
            }
        }


        string ReadH5Templet() {
            StringBuilder sb = new StringBuilder();
            string filename = Server.MapPath("/Scripts/h5/templet.html");
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (StreamReader reader = new StreamReader(fs)) {
                    while (reader.Peek() > 0) {
                        sb.AppendLine(reader.ReadLine());
                    }
                }
            }

            return sb.ToString();
        }
    }
}
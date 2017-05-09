using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    public class LogoController :ControllerBase
    {
        //
        // GET: /Logo/

        public ActionResult Index()
        {
            string filePath = GetLogo();
            ViewBag.ImagePath = filePath;
            return View();
        }

        public ActionResult GetSchoolLogo()
        {
            string filePath = GetLogo();
            if (System.IO.File.Exists(Server.MapPath("~/" + filePath)))
                return Json(new { state = 0, path = "/"+filePath }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { state = -1, path = "pclogo不存在" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase file)
        {
            if (file == null) {
                return Json(new { success = false, message = "没有选择文件。" });
            }

            var ext = Path.GetExtension(file.FileName);
            var fileName = Path.Combine(Request.MapPath("~/UpFile"), "pclogo"+ext);
            try
            {
                DeleteFile();
                file.SaveAs(fileName);
                return Json(new { success = true, message = "修改成功。", filename = Path.GetFileName(fileName) });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.LogoManger, ex, "上传内容异常");
                return Json(new { success = false, message = ex.Message });
            }
        }

        void DeleteFile() 
        {
            new DirectoryInfo(Request.MapPath("~/UpFile")).GetFiles().ToList().ForEach(file => { System.IO.File.Delete(file.FullName); });
        }

        string GetLogo()
        {
            var list = new List<string>();
            GetSubFiles(new DirectoryInfo(Request.MapPath("~/UpFile")), list);
            if (list.Count > 0)
            {
                string logo = list[0].Substring(list[0].IndexOf("UpFile"), list[0].Length - list[0].IndexOf("UpFile"));
                return logo.Replace("\\","/");
            }
            
            return null;
        }

        private  void GetSubFiles(FileSystemInfo directory, List<string> fileList) {
            if (!directory.Exists) return;
            try {
                DirectoryInfo dir = directory as DirectoryInfo;
                //不是目录 
                if (dir == null) return;
                FileSystemInfo[] files = dir.GetFileSystemInfos();
                for (int i = 0; i < files.Length; i++) {
                    FileInfo file = files[i] as FileInfo;
                    if (file != null) {
                        fileList.Add(file.FullName);
                    } else {
                        GetSubFiles(files[i], fileList);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Models;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Controllers {
    public class MettingExternalApiController : ControllerBase {

        [HttpPost]
        public ActionResult EucService() {
            if (Request.InputStream.Length == 0) {
                Logger.WriteErrorFmt(Log.EucService, "接收inputstream数据长度为0");
                return JsonResult("-1", "从pc客户端接受到数据长度为0字节", false);
            }

            try {
                string strBody = CommonHelp.Instance.GetRequestBody(Request.InputStream);
                var dic = AnalysisQuery(strBody);
                if (!dic.ContainsKey(UrlString.Service) || !dic.ContainsKey(UrlString.Params))
                    throw new MettingExternalApiException("-439", "body 中参数名称不合法");

                var jsonObj = CommonHelp.Instance.ConvertToObj<MettingExternalApiRequestParam>(dic[UrlString.Params]);

                return Json(ProcessService(dic[UrlString.Service], jsonObj));
            } catch (Exception ex) {
                Logger.WriteError(Log.EucService, "接口处理异常", ex);
                return JsonResult("-79", "【EucService】系统异常", false);
            }
        }

        object ProcessService(string service, MettingExternalApiRequestParam jsonObj) {
            switch (service) {
                case "entmemberlogin":
                    return EntmemberLogin(jsonObj);
                    break;
                case "verifytoken":
                    return VerifyToken(jsonObj);
                    break;
                case "searchaccount":
                    return SearchAccount(jsonObj);
                    break;
                default:
                    throw new Exception("请求接口body携带参数service不合法");
            }
        }

        private EntMemberLoginResult EntmemberLogin(MettingExternalApiRequestParam jsonObj) {
            var result = new EntMemberLoginResult() { Status = "0", Message = "成功" };
            var schoolRoom = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetByImei(jsonObj.Imei);
            if (schoolRoom == null) {
                result.Status = "-439";
                result.Message = "串号对应教室不存在";
                return result;
            }

            var userInfo = new UserInfoResult() {
                AccessToken = schoolRoom.Token,
                CreateTime = DateTime.Now.ToString(),
                NubeNumber = schoolRoom.SchoolRoomNum,
                NickName = schoolRoom.SchoolRoomName
            };
            result.UserInfo = userInfo;
            return result;
        }

        private VerifyTokenResult VerifyToken(MettingExternalApiRequestParam jsonObj) {
            var result = new VerifyTokenResult() { Status = "0", Message = "成功" };
            var schoolRoom = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetByToken(jsonObj.Token);
            if (schoolRoom == null) {
                result.Status = "-201";
                result.Message = "token 无效";
                result.IsValid = false;
                return result;
            }

            result.IsValid = true;
            return result;
        }

        private SearchAccountResult SearchAccount(MettingExternalApiRequestParam jsonObj) {
            var result = new SearchAccountResult() { Status = "0", Message = "成功" };
            var schoolRoom = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetByNubeNumber(jsonObj.NubeNumbers[0]);
            if (schoolRoom == null) {
                result.Status = "-912";
                result.Message = "nubeNumber 帐号不存在";
                return result;
            }

            result.Users.Add(new User() {
                AppType = "pc",
                NickName = schoolRoom.SchoolRoomName,
                NubeNumber = jsonObj.NubeNumbers[0]
            });

            return result;
        }

        private JsonResult JsonResult(string code, string msg, bool isallowGet) {

            if (isallowGet)
                return Json(new { status = code, message = msg }, JsonRequestBehavior.AllowGet);

            return Json(new { status = code, message = msg });
        }

        /// <summary>
        /// 拆分body 中参数 eg:service=entMemberLogin&params={"imei":"BOX3417EB7D3A17"}
        /// </summary>
        /// <param name="reqParams"></param>
        /// <param name="seprarator"></param>
        /// <returns></returns>
        Dictionary<string, string> AnalysisQuery(string reqParams, params  char[] separator) {
            var dic = new Dictionary<string, string>();
            int count = reqParams.Count(c => c == '=');
            if (count != 2 || !reqParams.Contains('&'))
                throw new MettingExternalApiException("-439", "body 中参数不合法");

            string[] tmp = reqParams.Split('&', '=');
            for (int i = 0; i < tmp.Length; i += 2) {
                dic.Add(tmp[i].ToLower(), tmp[i + 1].ToLower());
            }

            return dic;
        }
    }

    public class UrlString {
        public UrlString() {

        }

        public static string Service = "service";
        public static string Params = "params";

    }

    public class MettingExternalApiException : Exception {
        public string ErrCode { get; set; }

        public MettingExternalApiException(string errcode, string msg, Exception innerexecption)
            : base(msg, innerexecption) {

            ErrCode = errcode;
        }

        public MettingExternalApiException(string errcode, string msg)
            : this(errcode, msg, null) {
            ErrCode = errcode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS.Services {
    public class MettingServer : SingletonBase<MettingServer> {

        const string service = "CreateMeeting";

        public CreateMeetingResult Create(string url, string beginDatetime, int effectiveHour) {
          
            var result = new CreateMeetingResult();
            var createParams = new CreateMeetingParams() {
                MeetingType =(int)MeetingTypeEnum.预约会议,
                Username = SettingConfig.Instance.MeetingUserName,
                Pwd = SettingConfig.Instance.MeetingUserPwd,
                BeginDateTime = beginDatetime,
                EffectiveHour = effectiveHour
            };

            try
            {
                string requestBody = CommonHelp.Instance.ConvertToJson(createParams);
                Logger.WriteDebugingFmt(Log.ReserveMetting, "请求Url[{0}]，请求内容：{1}", url, requestBody);
                string resultStr = HttpPostHelper.Instance.Post(string.Format("{0}?service={1}&params={2}", url, service, requestBody), string.Empty);
                Logger.WriteDebugingFmt(Log.ReserveMetting, "响应结果：{0}", resultStr);
                result=CommonHelp.Instance.ConvertToObj<CreateMeetingResult>(resultStr);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "处理业务请求出现异常");
                result.ResultStatus.RC = "-500";
                result.ResultStatus.RD = "接口调用失败，异常信息:" + ex.Message;
            }

            return result;
        }
    }

    /// <summary>
    /// http请求工具
    /// 1、用于进行标准的http post请求；
    /// 2、不支持文件上传，post body中可以存放自定义数据信息，目前只支持字符串 
    /// </summary>
    public class HttpPostHelper : SingletonBase<HttpPostHelper> {
        /// <summary>
        ///  发送请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="body">body内容</param>
        /// <returns>响应结果</returns>
        public string Post(string url, string body) {
            string result = string.Empty;

            try {
                var buffer = Encoding.UTF8.GetBytes(body);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;
                request.Timeout = 120000;
                using (Stream myRequestStream = request.GetRequestStream()) {
                    myRequestStream.Write(buffer, 0, buffer.Length);
                    var response = (HttpWebResponse)request.GetResponse();

                    using (Stream myResponseStream = response.GetResponseStream()) {
                        using (var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"))) {
                            result = myStreamReader.ReadToEnd();
                        }
                    }
                }
            } catch (Exception ex) {
                Logger.WriteErrorFmt(LogCatalogs.Uncatalog, ex, "向目标[{0}]发送请求出现异常", url);
                throw;
            }

            return result;
        }
    }
}
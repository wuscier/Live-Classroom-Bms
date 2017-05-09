using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using GM.Business.Module;
using GM.Business.Service;
using GM.Service.InterfaceServerControl;
using GM.Utilities;
using GM.Utilities.Web;
using GM.Orm;
using Newtonsoft.Json;


namespace GM.Services
{

    public class InterfaceProvideService : HttpMethodProcessorBase
    {

        /// <summary>
        /// 从CDN平台获取码率、文件大小、时长
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cmsId">加速域名</param>
        /// <param name="contentIds">内容ID集合</param>
        [HttpMethod]
        public void GetVodInfo(HttpMethodContext context, string cmsId, string contentIds)
        {
            try
            {
                Logger.WriteInfoFmt(Log.VodInfo, "接收到获取内容请求，cmsId：{0}，contentIds：{1}", cmsId, contentIds);
                context.HttpMethodResponse.IsCustomWriteResponse = true;
                string resultInfo = string.Empty;
                if (InterfaceServerControlSetting.Instance.CenterModel)
                {

                    resultInfo = InterfaceServerData.Instance.GetVodInfo(cmsId, contentIds);
                    context.HttpContext.Response.Write(resultInfo);
                }
                else
                {
                    InterfaceServerResponse response = new InterfaceServerResponse();
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                    response.ResponseStatus.Description = "服务器工作在非中心模式下，不支持此接口";
                    resultInfo = SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));

                }
                Logger.WriteInfoFmt(Log.VodInfo, "返回信息:{0}", resultInfo);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteErrorFmt(Log.VodInfo, ex, "获取cdn内容异常：{0}", ex.Message);
            }
        }


        /// <summary>
        /// 从CDN平台删除加速域名下的内容集合
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cmsId">加速域名</param>
        /// <param name="contentIds">内容ID集合</param>
        [HttpMethod]
        public void DeleteVodInfo(HttpMethodContext context, string cmsId, string contentIds)
        {
            try
            {
                Logger.WriteInfoFmt(Log.RemoveVodInfo, "接收到删除内容请求，cmsID:{0},contentIds:{1}", cmsId, contentIds);
                context.HttpMethodResponse.IsCustomWriteResponse = true;
                string deleteInfo = string.Empty;
                string result = string.Empty;
                bool isSuccess = false;
                if (InterfaceServerControlSetting.Instance.CenterModel)
                {
                    if (!string.IsNullOrEmpty(InterfaceServerControlSetting.Instance.EdgeInterfaceServerAddress))
                    {
                        string sdata = string.Format("cmsId={0}&ContentIds={1}", cmsId, contentIds);
                        InterfaceServerResponse response = InterfaceServerData.Instance.CascadeDeleteVod(sdata, Path.Combine(InterfaceServerControlSetting.Instance.EdgeInterfaceServerAddress, "DeleteVodInfo.aspx"));

                        if (response.ResponseStatus.ResponseCode == 0)
                            isSuccess = true;
                        else
                            deleteInfo = "级联删除失败:" + response.ResponseStatus.Description;
                    }
                    else
                        isSuccess = true;
                }
                else
                    isSuccess = true;

                if (isSuccess)
                    result = InterfaceServerData.Instance.DeleteVodInfo(cmsId, contentIds);
                else
                {
                    InterfaceServerResponse response = new InterfaceServerResponse();
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                    response.ResponseStatus.Description = deleteInfo;
                    result = SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
                }
                context.HttpContext.Response.Write(result);

                Logger.WriteInfoFmt(Log.RemoveVodInfo, "返回信息:{0}", result);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteErrorFmt(Log.RemoveVodInfo, ex, "删除内容异常：{0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除CDN系统中指定的内容或者pid下所有内容
        /// </summary>
        /// <param name="context"></param>
        [HttpMethod(SubmitType.Post)]
        public void DeleteContents(HttpMethodContext context)
        {
            try
            {
                string reqStr = GetRequestBody(context);
                Logger.WriteInfoFmt(Log.RemoveVodInfo, "接收到删除内容请求:{0}", reqStr);
                context.HttpMethodResponse.IsCustomWriteResponse = true;

                var taskInfo = (ContentDeleteTaskInfo)SerializeHelper.Deserialize(reqStr, typeof(ContentDeleteTaskInfo));
                InterfaceServerData.Instance.CreateRecycleTask(taskInfo);
                InterfaceServerData.Instance.DeleteContents(taskInfo);

                context.HttpContext.Response.StatusCode = 200;
                var response = new InterfaceServerResponse()
                {
                    ResponseStatus = new ResponseStatus()
                    {
                        ResponseCode = ErrorCode.Success,
                        Description = string.Empty
                    }
                };
                string result = SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
                context.HttpContext.Response.Write(result);

                if (InterfaceServerControlSetting.Instance.CenterModel && !string.IsNullOrEmpty(InterfaceServerControlSetting.Instance.EdgeInterfaceServerAddress))
                    InterfaceServerData.Instance.CascadeDeleteTask(reqStr, Path.Combine(InterfaceServerControlSetting.Instance.EdgeInterfaceServerAddress, "DeleteContents.aspx"));
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteErrorFmt(Log.RemoveVodInfo, ex, "删除内容异常");
            }
        }

        /// <summary>
        /// 读取请求内容
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetRequestBody(HttpMethodContext context)
        {
            try
            {
                var stream = context.HttpContext.Request.InputStream;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取请求内容出现异常", ex);
            }
        }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cmsId">加速域名</param>
        /// <param name="key">密钥内容</param>
        /// <param name="duration">有效时间</param>
        /// <param name="secretEnable">密钥是否启动</param>
        /// <param name="domains">referer区域</param>
        /// <param name="refererEnable">是否启动</param>
        [HttpMethod]
        public void SetKey(HttpMethodContext context, string cmsId, string key, long duration, bool secretEnable, string domains, bool refererEnable)
        {
            try
            {
                Logger.WriteInfoFmt(Log.refererInfo, "接收到密钥同步请求，cmsID:{0},KEY:{1},duration:{2},secretEnable：{3},domains:{4},refererEnable:{5}", cmsId, key, duration, secretEnable, domains, refererEnable);
                context.HttpMethodResponse.IsCustomWriteResponse = true;
                string kesInfo = string.Empty;
                if (InterfaceServerControlSetting.Instance.CenterModel)
                {
                    kesInfo = InterfaceServerData.Instance.SetSecretKey(cmsId, key, duration, secretEnable, domains, refererEnable);
                    context.HttpContext.Response.Write(kesInfo);
                }
                else
                {
                    InterfaceServerResponse response = new InterfaceServerResponse();
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                    response.ResponseStatus.Description = "服务器工作在非中心模式下，不支持此接口";
                    kesInfo = SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
                }
                Logger.WriteInfoFmt(Log.refererInfo, "返回信息:{0}", kesInfo);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteErrorFmt(Log.refererInfo, ex, "同步密钥异常：{0}", ex.Message);
            }
        }

        [HttpMethod(SubmitType.Post)]
        public void CreateLiveChannel(HttpMethodContext context)
        {
            context.HttpMethodResponse.IsCustomWriteResponse = true;
            try
            {
                var reqStr = GetRequestBody(context);
                var reqParam = CommonHelper.Instance.ConvertToObj<LiveChannelCreateReplyParam>(reqStr);
                Logger.WriteInfoFmt(Log.CreateLiveChannel, "接收到创建直播频道接口请求,【domianname:{0},channelName:{1}】", reqParam.DomainName, reqParam.ChannelName);
                var reply = PhysicalChannelManager.Instance.CreatePhysicalChannel(reqParam);
                var jsonResult = CommonHelper.Instance.ConvertToJson(reply);

                byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
                Stream output = context.HttpContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteError(Log.CreateLiveChannel, "创建直播频道异常", ex);
            }
        }

        [HttpMethod(SubmitType.Post)]
        public void DeleteLiveChannels(HttpMethodContext context)
        {
            context.HttpMethodResponse.IsCustomWriteResponse = true;
            try
            {
                var reqStr = GetRequestBody(context);
                var reqParam = CommonHelper.Instance.ConvertToObj<LiveChannelDeleteReplyParam>(reqStr);
                Logger.WriteDebugingFmt(Log.CreateLiveChannel, "接收到删除直播频道频道接口请求,【domianname:{0},contentIds:{1}】", reqParam.DomainName, string.Join("|", new List<string>(reqParam.ChannelNames).ConvertAll(c => c.ToString()).ToArray()));
                var reply = PhysicalChannelManager.Instance.DeleteLiveChannels(reqParam);
                var jsonResult = CommonHelper.Instance.ConvertToJson(reply);

                byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
                Stream output = context.HttpContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteError(Log.CreateLiveChannel, "删除直播频道异常", ex);
            }
        }

        [HttpMethod(SubmitType.Post)]
        public void FileDelete(HttpMethodContext context)
        {
            context.HttpMethodResponse.IsCustomWriteResponse = true;
            try
            {
                var reqStr = GetRequestBody(context);

                var reqParam = CommonHelper.Instance.ConvertToObj<FileUploadReplyParam>(reqStr);
                Logger.WriteDebugingFmt(Log.DeleteFiles, "接收删除文件接口请求,【domianname:{0}】", reqParam.DomainName);
                var reply = FileUploadManager.Instance.DeleteFiles(reqParam);
                var jsonResult = CommonHelper.Instance.ConvertToJson(reply);

                byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
                Stream output = context.HttpContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteError(Log.DeleteFiles, "删除文件异常", ex);
            }
        }


        [HttpMethod]
        public void CreateServiceConfig(HttpMethodContext context)
        {

            context.HttpMethodResponse.IsCustomWriteResponse = true;

            var reqStr = GetRequestBody(context);

            var reqParam = CommonHelper.Instance.ConvertToObj<CreateMediaServiceParams>(reqStr);
            string fileDomain = string.Format("f.{1}", reqParam.UserID, reqParam.Alise);
            string liveDomain = string.Format("l.{1}", reqParam.UserID, reqParam.Alise);

            var response = new CreateServiceConfigResponse()
            {
                ResultCode = "0",
                Desc = "操作成功",
                FileDomainName = fileDomain,
                LiveDomainName = liveDomain
            };
            try
            {
                //判断CDN系统中是否存在对应域名的配置，如果已存在不执行创建操作
                bool exist =
                    MediaServiceService.Instance.GetAll()
                        .Exists(
                            config => config.ProviderID == getPId(fileDomain) || config.ProviderID == getPId(liveDomain));

                if (!exist)
                {
                    using (var scope = new DbTranscationScope())
                    {
                        //创建文件服务配置
                        CreateMediaService(fileDomain, getPId(fileDomain),ServiceTypeEnum.Download);
                        CreateAccessService(getCmsId(fileDomain), getPId(fileDomain));
                       
                        //创建直播服务配置
                        CreateMediaService(liveDomain, getPId(liveDomain), ServiceTypeEnum.Live);
                        CreateAccessService(getCmsId(liveDomain), getPId(liveDomain));
                        scope.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                response.ResultCode = "500";
                response.Desc = "系统错误";

                Logger.WriteErrorFmt("获取cdn内容异常：{0}", e.Message);
            }

            var jsonResult = CommonHelper.Instance.ConvertToJson(response);
            byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
            Stream output = context.HttpContext.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            Logger.WriteInfoFmt(Log.VodInfo, "返回信息:{0}", jsonResult);
        }

        /// <summary>
        /// 创建媒体服务配置
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="providerId"></param>
        /// <param name="serviceType"></param>
        private void CreateMediaService(string prefix,string providerId,ServiceTypeEnum serviceType)
        {
            var media = new MediaService()
            {
                Index = 0,
                OriginServerAddr = "1.1.1.1:80",
                ProviderType = 0,
                ContentURLParseRule = "*",
                ContentForamtParseRule = "",
                ProtocolTypeParseRule = "",
                Domain = 3871,
                Config = new MediaServiceConfig()
                {
                    ServiceType = serviceType,
                    Status = true
                },
                PlaybackURLprefix = prefix,
                ProviderID = providerId,
            };

            MediaServiceService.Instance.CreateOrUpdate(media);
        }

        /// <summary>
        /// 创建内容接入配置
        /// </summary>
        /// <param name="cmsid"></param>
        /// <param name="providerId"></param>
        private void CreateAccessService(string cmsid,string providerId)
        {
            var contentAccess = new ContentAccess()
            {
                Index = 0,
                ProviderType = 0,
                Domain = 3871,
                CMSID = cmsid,
                ProviderID = providerId
            };
            
            ContentAccessService.Instance.CreateOrUpdate(contentAccess);
        }


        private string getPId(string domainName)
        {
            return domainName.Replace(".", "_");// +"_pid";
        }

        private string getCmsId(string domainName)
        {
            return domainName.Replace(".", "_");// +"_cmsid";
        }
        /// <summary>
        /// 创建节目单
        /// </summary>
        /// <param name="context"></param>
        [HttpMethod(SubmitType.Post)]
        public void CreateSchedules(HttpMethodContext context)
        {
            context.HttpMethodResponse.IsCustomWriteResponse = true;
            try
            {
                var reqStr = GetRequestBody(context);
                var reqParam = JsonConvert.DeserializeObject<CreateOrDeleteSchedulesParam>(reqStr);
                Logger.WriteInfoFmt(Log.ScheduleManager, "接收到创建节目单请求,频道id：{0},域名：{1}", reqParam.ChannelID, reqParam.DomainName);
                var obj = ScheduleManager.Instance.CreateSchedules(reqParam);
                var jsonResult = JsonConvert.SerializeObject(obj);
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
                Stream output = context.HttpContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteError(Log.CreateLiveChannel, "添加节目单异常", ex);
            }
        }

        /// <summary>
        /// 删除节目单
        /// </summary>
        /// <param name="context"></param>
        [HttpMethod(SubmitType.Post)]
        public void DeleteSchedules(HttpMethodContext context)
        {
            context.HttpMethodResponse.IsCustomWriteResponse = true;
            try
            {
                var reqStr = GetRequestBody(context);
                var reqParam = JsonConvert.DeserializeObject<CreateOrDeleteSchedulesParam>(reqStr);
                Logger.WriteInfoFmt(Log.ScheduleManager, "接收到删除节目单请求,频道id：{0},域名：{1}", reqParam.ChannelID, reqParam.DomainName);
                var obj = ScheduleManager.Instance.DeleteSchedules(reqParam);
                var jsonResult = JsonConvert.SerializeObject(obj);
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResult);
                Stream output = context.HttpContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.Write(ex.Message);
                Logger.WriteError(Log.CreateLiveChannel, "删除节目单异常", ex);
            }
        }
    }

    class Log : LogCatalogs
    {
        public static string Debug = "接口调试";
        public static string VodInfo = "获取点播文件";
        public static string RemoveVodInfo = "删除内容";
        public static string refererInfo = "密钥同步";
        public static string CreateLiveChannel = "创建直播频道";
        public static string DeleteLiveChannel = "删除直播频道";
        public static string DeleteFiles = "删除文件";
        public static string ScheduleManager = "节目单管理";

        static Log()
        {
            LogCatalogs.RegisteCatalogs(typeof(Log));
        }
    }
}

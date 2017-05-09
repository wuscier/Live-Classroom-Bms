using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using GM.Orm;
using GM.Orm.Db;
using GM.Service.InterfaceServerControl;
using GM.Utilities;
using GM.Business.Module;
using System.Threading;


namespace GM.Services
{
    public class InterfaceServerData
    {
        private InterfaceServerData()
        {
        }

        private static readonly InterfaceServerData _singleTon = new InterfaceServerData();
        public static InterfaceServerData Instance {
            get { return _singleTon; }
        }

        private Queue<QueueTask> _taskQueue = new Queue<QueueTask>();
        private Thread _transferThread;

        public void Start()
        {
            try
            {
                if (_transferThread == null)
                    _transferThread = new Thread(TransferTask);

                _transferThread.IsBackground = true;
                _transferThread.Start();

                Logger.WriteInfoFmt(Log.AppStartup, "启动任务转发线程");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits,ex,"启动任务转发线程出现异常");
            }
        }

        public void Stop()
        {
            try
            {
                Logger.WriteInfoFmt(Log.AppStartup,"停止任务转发线程");
                if(_transferThread != null)
                    _transferThread.Abort();
            }
            catch (Exception)
            {
            }
        }

        public string SetSecretKey(string cmsId, string key, long startTimeSeconds, bool secretEnable, string domains, bool refereEnable)
        {
            InterfaceServerResponse response = new InterfaceServerResponse();
            try
            {
                response.ResponseStatus = new ResponseStatus();
                DateTime dateTime = DB.DBManager.CurrentDatabaseTime;
                dateTime = dateTime.AddSeconds(startTimeSeconds);
                var privadeId = new Entities<ContentAccess>("cms_id='{0}' AND DELETE_FLAG=0", cmsId).Cache;
                if (privadeId == null || privadeId.Count <= 0)
                {
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.ParameterError;
                    response.ResponseStatus.Description = string.Format("cmsid:{0} 在CDN平台中不存在", cmsId);
                    return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
                }

                var dateTemp = new Entities<MediaService>("PROVIDER_ID='{0}' AND DELETE_FLAG=0", privadeId[0].ProviderID).Cache;
                if (dateTemp == null || dateTemp.Count <= 0)
                {
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.ParameterError;
                    response.ResponseStatus.Description = string.Format("cmsid:{0} 更新密钥失败，对应的记录CDN中不存在", cmsId);
                    return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
                }

                var data = dateTemp[0];
                data.Config.SecretkeyConfig.Enable = secretEnable;
                data.Config.SecretkeyConfig.Expires = dateTime;
                data.Config.SecretkeyConfig.Key = key;
                data.Config.RefererConfig.Enable = refereEnable;
                data.Config.RefererConfig.Domain = new List<string>(domains.Split(','));
                (data as IEntity).Update();

                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.Success;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 更新密钥成功", cmsId);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
            }
            catch (Exception ex)
            {
                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 更新密钥异常：{1}", cmsId, ex.Message);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
            }
        }

        public string DeleteVodInfo(string cmsId, string phyicalIds)
        {
            InterfaceServerResponse response = new InterfaceServerResponse();
            try
            {
                response.ResponseStatus = new ResponseStatus();

                var privadeId = new Entities<ContentAccess>("cms_id='{0}' AND DELETE_FLAG=0", cmsId).Cache;
                if (privadeId == null || privadeId.Count <= 0)
                {
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.ParameterError;
                    response.ResponseStatus.Description = string.Format("cmsid:{0} 在CDN平台中不存在", cmsId);
                    return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse)); ;
                }
                StringBuilder sb = new StringBuilder();
                if (phyicalIds.Contains(","))
                {
                    phyicalIds = phyicalIds.Replace(",", "','" + privadeId[0].ProviderID + "/");
                }
                sb.Append("'").Append(privadeId[0].ProviderID).Append("/").Append(phyicalIds).Append("'");

                var datas = new Entities<PhysicalFile>("DELETE_FLAG=0 AND outer_id in ({0})", sb.ToString()).Cache;

                if (datas != null && datas.Count > 0)
                {
                    foreach (var physicalFile in datas)
                    {
                        new Entities<PhysicalFile>().Remove(physicalFile);
                    }
                }

                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.Success;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 删除内容成功", cmsId);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
            }
            catch (Exception ex)
            {
                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 删除内容异常：{1}", cmsId, ex.Message);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
            }
        }

        public string GetVodInfo(string cmsId, string contentIds)
        {
            InterfaceServerResponse response = new InterfaceServerResponse();
            try
            {
                response.ResponseStatus = new ResponseStatus();

                var privadeId = new Entities<ContentAccess>("cms_id='{0}' AND DELETE_FLAG=0", cmsId).Cache;
                if (privadeId == null || privadeId.Count <= 0)
                {
                    response.ResponseStatus = new ResponseStatus();
                    response.ResponseStatus.ResponseCode = ErrorCode.ParameterError;
                    response.ResponseStatus.Description = string.Format("cmsid:{0} 在CDN平台中不存在", cmsId);
                    return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse)); ;
                }

                StringBuilder sb = new StringBuilder();
                if (contentIds.Contains(","))
                {
                    contentIds = contentIds.Replace(",", "','" + privadeId[0].ProviderID + "/");
                }
                sb.Append("'").Append(privadeId[0].ProviderID).Append("/").Append(contentIds).Append("'");

                var vodFiles = new Entities<PhysicalFile>("delete_flag=0 and outer_id in ({0})", sb.ToString()).Cache;

                if (vodFiles != null && vodFiles.Count > 0)
                {
                    response.ResultInfo = new ResultInfo();
                    response.ResultInfo.cacheContents = new List<CacheContent>();
                    foreach (var physicalFile in vodFiles)
                    {
                        CacheContent cacheContent = new CacheContent();
                        cacheContent.ContentID = physicalFile.OuterId.Replace(privadeId[0].ProviderID+"/","");
                        cacheContent.FileSize = physicalFile.FileSize;
                        cacheContent.Duration = (long)(physicalFile.Duration - Convert.ToDateTime("2000-1-1 0:0:0")).TotalSeconds;
                        cacheContent.Rate = physicalFile.TransferRate;
                        cacheContent.ID = physicalFile.Id;
                        cacheContent.CreateTime = physicalFile.CreateTime;
                        response.ResultInfo.cacheContents.Add(cacheContent);
                    }
                }

                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.Success;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 数据查询成功！", cmsId);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));

            }
            catch (Exception ex)
            {
                response.ResponseStatus = new ResponseStatus();
                response.ResponseStatus.ResponseCode = ErrorCode.OtherCode;
                response.ResponseStatus.Description = string.Format("cmsid:{0} 获取内容码率、文件大小、时长异常：{1}", cmsId, ex.Message);
                return SerializeHelper.Serialize(response, typeof(InterfaceServerResponse));
            }
        }

        /// <summary>
        /// 删除内容集合
        /// </summary>
        public void DeleteContents(ContentDeleteTaskInfo taskInfo)
        {
            var outerIds = new List<string>();
            foreach (var contentInfo in taskInfo.ContentInfos)
            {
                try
                {
                    string pid = GetProviderID(contentInfo.Cmsid);

                    string deleteSql = string.Empty;
                    if (taskInfo.DeleteType == ContentDeleteType.DeleteByContentIDs)
                    {
                        contentInfo.ContentIds.ForEach(cid => outerIds.Add(string.Format("{0}/{1}", pid, cid)));
                        deleteSql = string.Format("update physical_file set delete_flag=1,update_time=now() where delete_flag=0 and outer_id in ('{0}')", string.Join("','", outerIds.ToArray()));
                    }
                    else if (taskInfo.DeleteType == ContentDeleteType.DeleteByCmsids)
                        deleteSql = string.Format("update physical_file set delete_flag=1,update_time=now() where delete_flag=0 and outer_id like '{0}/%'", pid);

                    DB.DBManager.ExecuteSQL(this, deleteSql);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorFmt(Log.VodInfo, ex, "删除内容[{0}]出现异常", contentInfo.ToString());
                }
            }
        }

        /// <summary>
        /// 创建内容回收任务
        /// </summary>
        /// <param name="taskInfo"></param>
        public void CreateRecycleTask(ContentDeleteTaskInfo taskInfo)
        {
            try
            {
                var taskContext = new List<RecycleTaskContext>();
                foreach (var contentInfos in taskInfo.ContentInfos)
                {
                    string pid = GetProviderID(contentInfos.Cmsid);
                    if (taskInfo.DeleteType == ContentDeleteType.DeleteByContentIDs)
                    {
                        contentInfos.ContentIds.ForEach(cid => taskContext.Add(new RecycleTaskContext()
                        {
                            ProviderID = pid,
                            BusiType =
                                contentInfos.AccelerationServiceType == AccelerationServiceType.点播视频加速
                                    ? RecycleTaskBusiType.VOD
                                    : RecycleTaskBusiType.DownLoad,
                            ContentID = cid
                        })
                            );
                    }
                    else
                    {
                        taskContext.Add(new RecycleTaskContext()
                        {
                            ProviderID = pid,
                            BusiType =
                                contentInfos.AccelerationServiceType == AccelerationServiceType.点播视频加速
                                    ? RecycleTaskBusiType.VOD
                                    : RecycleTaskBusiType.DownLoad,
                        });
                    }
                }

                var recycleTask = new RecycleTask()
                {
                    Context = taskContext.ToArray(),
                    TaskType =
                        taskInfo.DeleteType == ContentDeleteType.DeleteByCmsids
                            ? RecycleTaskType.DeleteByProviderID
                            : RecycleTaskType.DeleteByContentID
                };

                new Entities<RecycleTask>().Add(recycleTask);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "创建内容回收任务出现异常");
                throw;
            }
        }

        /// <summary>
        /// 根据CMSID获取ProviderID
        /// </summary>
        /// <param name="cmsid"></param>
        /// <returns></returns>
        private string GetProviderID(string cmsid)
        {
            string providerID = cmsid;

            try
            {
                var serviceConfigs = new Entities<ContentAccess>("cms_id='{0}' AND DELETE_FLAG=0", cmsid).Cache;
                if (serviceConfigs != null && serviceConfigs.Count > 0)
                    providerID = serviceConfigs[0].ProviderID;
            }
            catch (Exception ex)
            {
                Logger.WriteWarningFmt(LogCatalogs.OperationHits, ex, "获取CMSID：{0}对应的ProviderID出现异常", cmsid);
            }

            return providerID;
        }

        /// <summary>
        /// 级联下发删除任务
        /// </summary>
        /// <param name="body"></param>
        /// <param name="url"></param>
        public void CascadeDeleteTask(string body,string url)
        {
            try
            {
                _taskQueue.Enqueue(new QueueTask()
                {
                    Url = url,
                    Body = body
                });
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.VodInfo,ex,"投递内容删除任务出现异常");
            }
        }

        private void TransferTask()
        {
            while (true)
            {
                try
                {
                    if (_taskQueue.Count > 0)
                    {
                        var task = _taskQueue.Dequeue();
                        CascadeDeleteVod(task.Body, task.Url);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorFmt(Log.VodInfo,ex,"执行任务转发出现异常");
                }
                
                Thread.Sleep(1000);
            }
        }


        /// <summary>
        /// 级联删除下级平台物理文件
        /// </summary>
        /// <param name="cmsId"></param>
        /// <param name="phyicalIds"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public InterfaceServerResponse CascadeDeleteVod(string content, string url)
        {
            var response = new InterfaceServerResponse();
            try
            {
                string returnValue = HttpPostData(url, content);
                if (!string.IsNullOrEmpty(returnValue))
                    response = SerializeHelper.Deserialize(returnValue, typeof(InterfaceServerResponse)) as InterfaceServerResponse;
                else
                    response.ResponseStatus = new ResponseStatus() { ResponseCode = ErrorCode.OtherCode, Description = "调用下级平台删除接口失败" };
            }
            catch (Exception ex)
            {
                response = new InterfaceServerResponse();
                response.ResponseStatus = new ResponseStatus() { ResponseCode = ErrorCode.OtherCode, Description = "调用下级平台删除接口失败" };
                Logger.WriteDebugingFmt(Log.RemoveVodInfo, ex, "调用下级平台删除内容异常,请求地址{0},内容:{1}", url, content);
            }
            Logger.WriteInfoFmt(Log.RemoveVodInfo, "调用下级平台删除接口返回状态码:{0}，描述信息为:[{1}]", response.ResponseStatus.ResponseCode, string.IsNullOrEmpty(response.ResponseStatus.Description) ? "" : response.ResponseStatus.Description);
            return response;
        }

        private string HttpPostData(string aUrl, string aData)
        {
            if (string.IsNullOrEmpty(aUrl))
                throw new ArgumentNullException("url");

            try
            {
                var encoding = Encoding.GetEncoding("UTF-8");
                byte[] postBytes = Encoding.UTF8.GetBytes(aData);
                var request = WebRequest.Create(aUrl) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    request.ContentLength = postBytes.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postBytes, 0, postBytes.Length);
                        stream.Close();
                    }
                    var respone = (HttpWebResponse)request.GetResponse();
                    string responseData;
                    using (var reader = new StreamReader(respone.GetResponseStream(), encoding))
                    {
                        responseData = reader.ReadToEnd();
                    }
                    respone.Close();
                    return responseData;

                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.RemoveVodInfo, ex, "向下级平台发起删除请求时异常,请求地址:{0},参数:{1}", aUrl, aData);
                throw ex;
            }
        }

        private class QueueTask
        {
            public string Url { get; set; }
            public string Body { get; set; }
        }

    }
}

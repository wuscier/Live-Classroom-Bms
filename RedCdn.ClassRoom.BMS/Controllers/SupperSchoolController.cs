using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GM.Business.Module;
using GM.Orm;
using GM.Orm.Db;
using GM.Utilities;
using RedCdn.ClassRoom.BMS.Services;
using Redcdn.ClassRoom.Facade;
using Redcdn.ClassRoom.Service;
using RedCdn.ClassRoom.BMS.Models;

namespace RedCdn.ClassRoom.BMS.Controllers
{
    /// <summary>
    ///	为Pc终端提供的接口
    /// </summary>
    public class SupperSchoolController : ControllerBase
    {
        [HttpPost]
        [InputStreamFilter]
        public ActionResult StartRecord()
        {
            var obj = CommonHelp.Instance.GetObjectByStream<RequestRecordParam>(Request.InputStream);
            var record = new RecordFileDto();
            try
            {
                var schoolRoomDto = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetSchoolRoomById(obj.ClassRoomId);
                if (schoolRoomDto == null)
                {
                    Logger.WriteErrorFmt(Log.StartRecord, "教室不存在或已经删除");
                     return JsonResult(-1, "教室不存在或已经删除", false);
                }

                string channelOuterId = schoolRoomDto.PhysicalChannelOuterId;
                string recordFileCidId = obj.RecordFileCid+".mp4";//录制文件增加后缀扩展名，便于pc端下载后可播放


                 record = FacadeFactory.Instance.Get<IRecordFileManager>().Create(new RecordFileDto()
                {
                    CurriculumId = obj.CurriculumId,
                    PhysicalFileImOuterId = schoolRoomDto.PhysicalChannelOuterId,
                    ClassNo = obj.ClassNo,
                    ClassroomId = obj.ClassRoomId
                });

                var context = CommonHelp.Instance.ConvertToJson(new RequestRecordParam()
                {
                    RecordId = record.Id,
                    ClassRoomId = obj.ClassRoomId,
                    ClassNo = obj.ClassNo,
                    CurriculumId = obj.CurriculumId
                });
                Logger.WriteInfoFmt(LogCatalogs.OperationHits,"录制上下文context json:{0}",context);

                FacadeFactory.Instance.Get<IRecordFileManager>().StartRecord(new TerminalRecordParamDto
                {
                    ChannelOuterId = channelOuterId,
                    RecordFileOuterId =string.Format("{0}/{1}", GetProviderbyCmsid(SettingConfig.Instance.DomainName), recordFileCidId),
                    Context = context
                });

                return JsonResult(0, string.Format("启动教室【ClassRoomId：{0}】录制成功", obj.ClassRoomId), false);
            }
            catch (Exception ex)
            {
                if (record.Id>0)
                    FacadeFactory.Instance.Get<IRecordFileManager>().Delete(record.Id);

                Logger.WriteErrorFmt(Log.StartRecord, ex, "启动频道录制失败,【ClassRoomId:{0}】", obj.ClassRoomId);
                return JsonResult(-1, string.Format("启动频道录制失败,【ClassRoomId:{0}】,{1}", obj.ClassRoomId,ex.Message), false);
            }
        }

        [HttpPost]
        [InputStreamFilter]
        public ActionResult StopRecord()
        {
            var obj = CommonHelp.Instance.GetObjectByStream<RequestRecordParam>(Request.InputStream);
            try {
              
                var schoolRoomDto = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetSchoolRoomById(obj.ClassRoomId);
                if (schoolRoomDto == null) {
                    Logger.WriteErrorFmt(Log.StartRecord, "教室不存在或已经删除");
                    JsonResult(-1, "教室不存在或已经删除", false);
                }

                string channelOuterId = schoolRoomDto.PhysicalChannelOuterId;

                FacadeFactory.Instance.Get<IRecordFileManager>().StopRecord(new TerminalRecordParamDto()
                {
                    ChannelOuterId = channelOuterId
                });

                 return JsonResult(0, string.Format("停止教室【ClassRoomId：{0}】录制成功", obj.ClassRoomId), false);

            } catch (Exception ex) {
                Logger.WriteErrorFmt(Log.StopRecord, ex, "停止频道录制失败,【ClassRoomId:{0}】", obj.ClassRoomId);
                return JsonResult(-1, string.Format("停止频道录制失败,【ClassRoomId:{0}】,{1}", obj.ClassRoomId,ex.Message), false);
            }
        }

        [HttpPost]
        [InputStreamFilter]
        public ActionResult UpdateCurriculumMeetingN0()
        {
            var obj = CommonHelp.Instance.GetObjectByStream<RequestUpdateCurriculumMeetingNoParam>(Request.InputStream);

            try
            {
                var curriculum= FacadeFactory.Instance.Get<ICurriculumManager>().Get(obj.CurriculumId);
                if (curriculum==null)
                    return JsonResult(0, string.Format("课表id:{0},串号:{1},信息不存在", obj.CurriculumId, obj.ClassRoomimie), false);

                var dtnow = string.Format("{0:yyyyMMdd}", DB.DBManager.CurrentDatabaseTime);
                if ((curriculum.MettingBeginDateTime + 3600) > Convert.ToInt64(obj.BeginDateTime) && curriculum.MettingId != 0)
                {
                    Logger.WriteInfoFmt(Log.ReserveMetting, "当天课表:{0},会议号:{1}已经预约过,无需重复预约", obj.CurriculumId, curriculum.MettingId);
                    return JsonResult(0, string.Format("当天课表:{0},会议号:{1}已经预约过,无需重复预约", obj.CurriculumId, curriculum.MettingId), false);
                }

                var result = MettingServer.Instance.Create(SettingConfig.Instance.MeetingApiUrl, obj.BeginDateTime, 24);
                if (int.Parse(result.ResultStatus.RC)< 0)
                {
                    Logger.WriteInfoFmt(Log.ReserveMetting, "课表:{0},串号:{1},预约会议失败", obj.CurriculumId, obj.ClassRoomimie);
                   return JsonResult(int.Parse(result.ResultStatus.RC), string.Format("课表:{0},预约会议失败", obj.CurriculumId), false);
                }

                FacadeFactory.Instance.Get<ICurriculumManager>().UpdateCurriculumMeetingNo(obj.CurriculumId, obj.ClassRoomimie, result.Response.MeetingId, Convert.ToInt64(obj.BeginDateTime));
                return JsonResult(0, string.Format("更新课堂号【ClassRoomimie：{0}】成功", obj.ClassRoomimie), false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.ReserveMetting, ex, "更新课堂号失败,【课表Id:{0}】", obj.CurriculumId);
                return JsonResult(-1, "系统错误", false);
            }
        }

        [HttpPost]
        [InputStreamFilter]
        public ActionResult RegisterLive()
        {
            var model = CommonHelp.Instance.GetObjectByStream<LiveClassroomModel>(Request.InputStream);

            try {

                Mapper.CreateMap<LiveClassroomModel, LiveClassRoomDto>().ForMember(lm => lm.Duration, opt => opt.Ignore()); 
                var dto = Mapper.Map<LiveClassroomModel, LiveClassRoomDto>(model);
                FacadeFactory.Instance.Get<ILiveClassRoomManager>().RegisterLive(dto);
                return JsonResult(0, string.Format("注册直播成功【ClassRoomimie：{0}】成功", model.ClassRoomImie), false);
            } catch (Exception ex) {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "注册直播,【ClassRoomimie:{0}】", model.ClassRoomImie);
                return JsonResult(-1, "系统错误", false);
            }
        }

        [HttpPost]
        [InputStreamFilter]
        public ActionResult SendLiveStreamStatus()
        {
            var model = CommonHelp.Instance.GetObjectByStream<LiveClassroomModel>(Request.InputStream);
            try
            {
                FacadeFactory.Instance.Get<ILiveClassRoomManager>().SendLiveStreamStatus(model.ClassRoomImie);
                return JsonResult(0, string.Format("更新直播教室心跳信息【ClassRoomimie：{0}】成功", model.ClassRoomImie), false);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新直播教室心跳失败,【ClassRoomimie:{0}】", model.ClassRoomImie);
                return JsonResult(-1, "系统错误", false);
            }
        }

        public ActionResult GetCourseList(int classroomid)
        {
            try {
                var coruseArry = FacadeFactory.Instance.Get<ICurriculumManager>().GetList(classroomid);

                CourseCollection col = new CourseCollection();
                col.Count = coruseArry.Count;
                foreach (CurriculumDto dto in coruseArry) {
                    CurriculumNumberDto curriculumNumber = FacadeFactory.Instance.Get<ICurriculumNumberManager>().Get(dto.CurriculumNumberId);

                    col.Courses.Add(new Course() {
                        Id = dto.Id,
                        Name = dto.Name,
                        CourseId = dto.CurriculumNameId,
                        CourseName = dto.CurriculumNameName,
                        CourseStartTime = curriculumNumber.StarTime,
                        CourseEndTime = curriculumNumber.StarTime.AddMinutes(curriculumNumber.Duration),
                        CurriculumNumber = dto.CurriculumNumberId,
                        CurriculumName = dto.CurriculumNumberName,
                        GradeId = dto.GradeId,
                        GradeName = dto.GradeName,
                        WeekId = dto.WeekDayId,
                        WeekName = dto.WeekDayName,
                        MainclassRoomId = dto.MainclassRoomId,
                        Mainclassroomname = dto.MainClassRoomName,
                        ListenClassRoomIds = dto.ListenClassRoomIds.Replace(",","|"),
                        ListenClassRoomnames = dto.ListenClassRoomNames.Replace(",", "|"),
                        MettingId = dto.MettingId,
                        IsPush = dto.IsPush,
                        IsRecord = dto.IsRecord,
                        IsPushRemote = dto.IsPushRemote,
                        IsRecordRemote = dto.IsRecordRemote,
                        IsLocalRecord = dto.IsLocalRecord,
                        MettingBeginDateTime = dto.MettingBeginDateTime
                    });
                }

                return Json(col, JsonRequestBehavior.AllowGet);

            } catch (Exception ex) {
                Logger.WriteErrorFmt(Log.StopRecord, ex, "获取【ClassRoomId:{0}】教室课表失败", classroomid);
                return JsonResult(-1, "系统错误", true);
            }
        }

        public ActionResult GetClassList()
        {
            try {
                var list = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetAll();
                return Json(list, JsonRequestBehavior.AllowGet);
            } catch (Exception ex) {
                Logger.WriteErrorFmt(Log.GetClassList, ex, "获取所有教室失败");
                return JsonResult(-1, string.Format("获取所有教室失败, {0}", ex.Message), true);
            }
        }

        public ActionResult CheckConnect()
        {
            return JsonResult(0,"连接正常",true);
        }

        public ActionResult AuthenticationClassRoom(string classRoomImei)
        {
            var result = new MettingExternalApiResultBase() { Status = "0", Message = "成功" };
            try {

                if (string.IsNullOrEmpty(classRoomImei))
                    throw new Exception("串号不能为空");

                var schoolRoom = FacadeFactory.Instance.Get<ISchoolRoomManager>().GetByImeiForPcClient(classRoomImei);
                if (schoolRoom == null)
                {
                    result.Status = "-1";
                    result.Message = "串号不存在";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var channel = RemoteChannelService.Instance.GetById(schoolRoom.Id);
                var model = new SchoolRoomJsonModel();
                AutoMapperWrapper.Instance.Map(schoolRoom, model);
                if (channel != null)
                {
                    model.RemotePlayStreamUrl = channel.PlayStreamUrl;
                    model.RemotePushStreamUrl = channel.PushStreamUrl;
                }

                return Json(new {status = "0", message = "成功", classroom = model}, JsonRequestBehavior.AllowGet);

            } catch (Exception ex) {
                Logger.WriteErrorFmt(Log.GetClassList, ex, "鉴权失败");
                return Json(new { Status = "-1", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       [HttpPost]
       [InputStreamFilter]
        public void ReporttVodComplete()
        {
            try
            {
                string strXml=CommonHelp.Instance.GetRequestBody(Request.InputStream);

                Logger.WriteInfoFmt(Log.ReportFile, "接受到汇报xml字符串   {0}", strXml);

                var result = SerializeHelper.Deserialize(strXml, typeof(RecordReportResult)) as RecordReportResult;
                if (result.Contenttype != 21)
                {
                   Logger.WriteWarningFmt(Log.ReportFile, "接收到汇报文件类型不是录制文件,不进行解析入库");
                    return;
                }

                var context= CommonHelp.Instance.ConvertToObj<RequestRecordParam>(result.Context);
                int duration = CommonHelp.Instance.DataDiff(Convert.ToDateTime(result.StartRecordRealTime),Convert.ToDateTime(result.StopRecordRealTime));

                FacadeFactory.Instance.Get<IRecordFileManager>().UpdateRecordFileDtoManagerDto(new RecordFileDto()
                {
                    Id = context.RecordId,
                    ClassroomId = context.ClassRoomId,
                    CurriculumId = context.CurriculumId,
                    StartTime = Convert.ToDateTime(result.StartRecordRealTime),
                    FilePlayUrl=result.FilePath,
                    Duration = duration,
                    RecordStatus = 1,//录制完成
                    ClassNo = context.ClassNo
                });

                Logger.WriteInfoFmt(Log.ReportFile, "处理汇报结果完成,【录制文件id:{0}】", context.RecordId);

            } catch (Exception ex) {

                Logger.WriteErrorFmt(Log.StopRecord, ex, "处理汇报结果失败");
            }
        }

        public ActionResult ReportContentfile()
        {
            Logger.WriteInfoFmt(Log.ReportContentfile, "接收到 [reportContentfile] 请求，不做处理");

           StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
           sb.Append("<wrs><rc>{0}</rc><wrs>\r\n");

           return Content(sb.ToString());
        }

        public ActionResult GetTopographics(string providerid, string contentid)
        {
            try
            {
                string outerId = providerid + "/" + contentid;
                Logger.WriteInfoFmt(Log.LiveTopographics, "接收到获取top请求outerId={0}", outerId);
                string topInfos = GetLiveChannelTopInfo(outerId);

                return Content(topInfos);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.OperationHits, ex, "获取top信息异常");
                return Content(null);
            }
          
        }

        public ActionResult GetPhysicalChannelInfo(string providerid, string contentid)
        {
            Logger.WriteInfoFmt(Log.LiveChannelInfo, "接收到获取物理频道信息请求providerid={0},contentid={1}", providerid, contentid);
            try
            {
                string physicalInfo = GetLiveChannelInfo(providerid, contentid);
                return Content(physicalInfo);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(Log.LiveChannelInfo, ex, "获取top信息异常");
                return Content(null);
            }
        }

        private string GetLiveChannelTopInfo(string outerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<topographics>");

            var physicalChannelList = new Entities<PhysicalChannel>("outer_id = '{0}'", outerId).Cache;
            Logger.WriteInfoFmt(Log.LiveTopographics, "outerId={0}获取到物理直播频道{1}个", outerId, physicalChannelList.Count);
            sb.Append("<liveChannelTopographics>");
            foreach (PhysicalChannel physicalChannel in physicalChannelList)
            {
                string root = physicalChannel.SrcCastType == TransType.unicast
                                 ? physicalChannel.UnicastUrl
                                 : physicalChannel.MulticastUrl;
                sb.AppendLine("<liveChannelTopographic root=\"" + root.Replace("&", "&amp;") + "\" cid=\"" + physicalChannel.Id +
                             "\" cpid=\"" + physicalChannel.Opid + "\">");
                sb.AppendLine("</liveChannelTopographic>");
            }
            sb.AppendLine("</liveChannelTopographics>");
            sb.AppendLine("</topographics>");

            return sb.ToString();
        }

        private string GetLiveChannelInfo(string pid, string cid) {
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<wrs>\r\n");
            string info = "<rc>{0}</rc>\r\n<rb>{1}</rb>\r\n";
            try {
                string outerId = pid + "/" + cid;
                var physicalChannelList = new Entities<PhysicalChannel>("outer_id = '{0}'", outerId).Cache;

                if (physicalChannelList.Count == 1) {
                    PhysicalChannel channel = physicalChannelList[0];
                    TelecomDomainDictionary domain = TelecomDomainDictionaryService.Instance.Build(channel.Domain);

                    string body = string.Format("<physicalchannelinfo providerid=\"{0}\" contentid=\"{1}\">\r\n<outputprotocoltype>{2}</outputprotocoltype>\r\n<recordcontentformat>{3}</recordcontentformat>\r\n</physicalchannelinfo>\r\n", pid, cid, domain.ProtocolType, channel.RecordContentFormat);
                    sb.Append(string.Format(info, "0", body));
                } else
                    sb.Append(string.Format(info, "-1001", "频道不存在或已删除"));

            } catch (Exception ex) {
                sb.Append(string.Format(info, "-1000", "数据库查询失败"));
                Logger.WriteErrorFmt(Log.LiveChannelInfo, ex, "查询pid={0}，cid={1}直播频道信息异常", pid, cid);
            }

            sb.Append("</wrs>");
            return sb.ToString();
        }

        private JsonResult JsonResult(int code, string msg, bool isallowGet) {

            if (isallowGet)
                return Json(new { status = code, message = msg }, JsonRequestBehavior.AllowGet);

            return Json(new { status = code, message = msg });
        }

        private string GetProviderbyCmsid(string domainName) {
            var ens = new Entities<MediaService>("playback_url_prefix='{0}'", domainName).Cache;

            return ens.Count != 0 ? ens[0].ProviderID : null;
        }
    }
}

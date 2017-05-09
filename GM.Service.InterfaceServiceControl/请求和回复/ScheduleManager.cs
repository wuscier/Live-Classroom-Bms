using System;
using System.Collections.Generic;
using System.Text;
using GM.Business.Service;
using GM.Business.Module;
using GM.Utilities;
using GM.Services;
using GM.Orm;
using GM.Orm.Db;

namespace GM.Service.InterfaceServerControl
{
    class ScheduleManager
    {
        private readonly static ScheduleManager _instance = new ScheduleManager();

        public static ScheduleManager Instance { get { return _instance; } }

        public CreateOrDeleteSchedulesReply CreateSchedules(CreateOrDeleteSchedulesParam reqParam)
        {
            var reply = new CreateOrDeleteSchedulesReply()
            {
                ResultCode = 0,
                Desc = "创建节目单成功"
            };
            try
            {
                //TODO:Domain转Providerid
                //TODO:ChannelID查找物理频道cid
                //TODO:物理频道的outerid=providerid/节目单cid
                //TODO:节目单outerId（PhysicalContentIDAttribute属性）= prividerid + guid后12位
                var mediaServiceEntities = new Entities<MediaService>("playback_url_prefix='{0}'", reqParam.DomainName).Cache;
                String providerId = mediaServiceEntities[0].ProviderID;
                var outerId = providerId + "/" + reqParam.ChannelID;
                var physicalchannelId = new Entities<PhysicalChannel>("outer_id='{0}'", outerId).Cache[0].Id;
                //遍历节目单列表
                for (int i = 0; i < reqParam.schedules.Length; i++)
                {
                    var scheduleParm = reqParam.schedules[i];
                    var scheduleOutId = providerId + "/" + Guid.NewGuid().ToString().Split('-')[4];
                    var timeSpan = Convert.ToDateTime(scheduleParm.endTime) - Convert.ToDateTime(scheduleParm.startTime);
                    var schedule = new ScheduleRecord2()
                    {
                        Name = "schedulerecord",
                        ContentId = physicalchannelId,
                        ContentOpid = "ss01",
                        PhysicalContentIDAttribute = scheduleOutId,
                        IsCompleted = -1,
                        StartTime = Convert.ToDateTime(scheduleParm.startTime),
                        EndTime = Convert.ToDateTime(scheduleParm.endTime),
                        Duration = new DateTime(2000, 1, 1, timeSpan.Days, timeSpan.Minutes, timeSpan.Seconds),
                        DstPop = "0"
                        //Convert.ToDateTime(scheduleParm.endTime) - (Convert.ToDateTime(scheduleParm.startTime) - DateTime.MinValue)
                    };
                    new Entities<ScheduleRecord2>().Add(schedule);
                }
            }
            catch (Exception e)
            {
                reply.ResultCode = -1;
                reply.Desc = "创建节目单失败";
                Logger.WriteError(Log.ScheduleManager, "创建节目单失败", e);
            }
            return reply;
        }
        /// <summary>
        /// 删除节目单
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public CreateOrDeleteSchedulesReply DeleteSchedules(CreateOrDeleteSchedulesParam reqParam)
        {
            var reply = new CreateOrDeleteSchedulesReply()
            {
                ResultCode = 0,
                Desc = "删除节目单成功"
            };
            try
            {
               
                for (int i = 0; i < reqParam.schedules.Length; i++)
                {
                    var record = new Entities<ScheduleRecord2>("outer_id='{0}'", reqParam.schedules[i].ScheduleId).Cache;
                    new Entities<ScheduleRecord2>().Remove(record[0]);
//                    DB.DBManager.ExecuteSQL(null, string.Format(@"UPDATE SCHEDULE_RECORD_2
//                                        SET
//	                                        DELETE_FLAG = 1,
//	                                        UPDATE_TIME = now()
//                                        WHERE
//                                            DELETE_FLAG = 0
//                                            AND OUTER_ID = '{0}'", reqParam.schedules[i].ScheduleId));
                }
            }
            catch (Exception e)
            {
                reply.ResultCode = -1;
                reply.Desc = "删除节目单失败";
                Logger.WriteError(Log.ScheduleManager, "删除节目单失败", e);
            }
            return reply;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class CurriculumService : EntityService<EntityKey, Curriculum, CurriculumService>
    {
        public PagingQueryEntityResult<Curriculum> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize) {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<Curriculum>(context).PagingCache;
        }

        public PagingQueryEntityResult<Curriculum> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize,Curriculum queryParam)
        {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            string whereSql = "1=1";

            if (queryParam.WeekDayId > 0)
                whereSql += string.Format(" and weekday_id={0}",queryParam.WeekDayId);
            if(queryParam.MainClassRoomId>0)
                whereSql += string.Format(" and main_classroom_id={0}", queryParam.MainClassRoomId);
            if(queryParam.CurriculumNumberId>0)
                whereSql += string.Format(" and curriculum_numberid={0}", queryParam.CurriculumNumberId);
            if(queryParam.GradeId>0)
                whereSql += string.Format(" and grade_id={0}", queryParam.GradeId);
            if(queryParam.CurriculumNameId>0)
                whereSql += string.Format(" and curriculum_nameid={0}", queryParam.CurriculumNameId);

            return new Entities<Curriculum>(context, whereSql).PagingCache;
        }

        public List<Curriculum> GetListNoPaging( Curriculum queryParam) {

            string whereSql = "1=1";

            if (queryParam.WeekDayId > 0)
                whereSql += string.Format(" and weekday_id={0}", queryParam.WeekDayId);
            if (queryParam.MainClassRoomId > 0)
                whereSql += string.Format(" and main_classroom_id={0}", queryParam.MainClassRoomId);
            if (queryParam.CurriculumNumberId > 0)
                whereSql += string.Format(" and curriculum_numberid={0}", queryParam.CurriculumNumberId);
            if (queryParam.GradeId > 0)
                whereSql += string.Format(" and grade_id={0}", queryParam.GradeId);
            if (queryParam.CurriculumNameId > 0)
                whereSql += string.Format(" and curriculum_nameid={0}", queryParam.CurriculumNameId);

            return new Entities<Curriculum>(whereSql).Cache;
        }

        /// <summary>
        /// 获取当前正在上课课表
        /// </summary>
        /// <returns></returns>
        public PagingQueryEntityResult<Curriculum> GetListByNowAttendClass(List<string> orderFields, bool orderFlag, int startIndex, int pageSize, Curriculum queryParam)
        {
            var classOrder = GetCurriculumnumberByAttendClass();
            if (classOrder == null) return new PagingQueryEntityResult<Curriculum>();

            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            string whereSql = "1=1";

            // 无查询条件，根据当前时间获取课序查询正在上课的课表
            if (queryParam.MainClassRoomId == 0 && queryParam.GradeId == 0 && queryParam.CurriculumNumberId == 0)
                whereSql += string.Format(" and curriculum_numberid={0}", classOrder.Id);
            else
            {
                if (queryParam.MainClassRoomId > 0)
                    whereSql += string.Format(" and main_classroom_id={0}", queryParam.MainClassRoomId);
                if (queryParam.GradeId > 0)
                    whereSql += string.Format(" and grade_id={0}", queryParam.GradeId);
                if (queryParam.CurriculumNameId > 0)
                    whereSql += string.Format(" and curriculum_nameid={0}", queryParam.CurriculumNameId);
            }

            return new Entities<Curriculum>(context, whereSql).PagingCache;
        }

        /// <summary>
        /// 根据课时、主教师查询课表信息
        /// </summary>
        /// <param name="courseId">课序</param>
        /// <param name="weekDayId">星期</param>
        /// <param name="mainClassId">主教室</param>
        /// <returns>课表信息</returns>
        public Curriculum Get(int courseId, int weekDayId,  int gradeId, int curriculumnameid)
        {
            var ens = new Entities<Curriculum>("curriculum_numberid={0} and weekday_id={1} and  grade_id={2} and curriculum_nameid={3}", courseId, weekDayId, gradeId, curriculumnameid).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        /// <summary>
        /// 根据年级，课序获取课表信息
        /// </summary>
        /// <param name="weekDayId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Curriculum> Get(int weekDayId, int courseId, int gradeId) {
            return new Entities<Curriculum>("weekday_id={0} and curriculum_numberid={1} and grade_id={2}", weekDayId, courseId, gradeId).Cache;
        }


        public List<Curriculum> Get(int weekDayId, int courseId) {
            return new Entities<Curriculum>("weekday_id={0} and curriculum_numberid={1}", weekDayId, courseId).Cache;
        }

        public Curriculum Get(int mainClassId)
        {
            var ens = new Entities<Curriculum>("main_classroom_id={0}", mainClassId).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public IList<Curriculum> GetList(int mainClassRoomId)
        {
            var list = new List<Curriculum>();
            var ens1 = new Entities<Curriculum>("main_classroom_id={0}", mainClassRoomId).Cache;
            var ens2 = new Entities<Curriculum>("listen_classroom_ids LIKE '%{0}%';", mainClassRoomId).Cache;

            list.AddRange(ens1);
            list.AddRange(ens2);
           return list;
        }

        public void UpdateCurriculumMeetingNo(int curriculumid, string classroomimie, int meetingid, long mettingstarttime)
        {
            var curriculum = Get(new EntityKey() {Id = curriculumid});
            if (curriculum == null)
                throw new BusinessException(-1, string.Format("课表id:{0}不存在", curriculumid));

            var schoolroom = SchoolRoomService.Instance.Get(new EntityKey() { Id = curriculum.MainClassRoomId });
            if(schoolroom==null)
                throw new BusinessException(-1, string.Format("主讲教室id:{0}不存在", classroomimie));

            //var curriculum = new Entities<Curriculum>("id={0} and main_classroom_id={1}", curriculumid, schoolroom.Id).Cache;
            //if(curriculum.Count==0)
            //    throw new BusinessException(-1, string.Format("课表id {0},主讲教室 {1} 课表信息不存在或已经删除", curriculumid, schoolroom.Id));

            curriculum.MettingId = meetingid;
            curriculum.MettingBeginDateTime = mettingstarttime;

            (curriculum as IEntity).Update();
        }

        public Curriculum GetCurriculum(int curriculumid, string classroomimie)
        {
            var schoolroom = SchoolRoomService.Instance.GetByImeiForPcClient(classroomimie);
            if (schoolroom == null)
                throw new BusinessException(-1, string.Format("串号:{0}绑定的教室不存在", classroomimie));

            var curriculum = new Entities<Curriculum>("id={0} and main_classroom_id={1}", curriculumid, schoolroom).Cache;
            return curriculum.Count > 0 ? curriculum[0] : null;
        }

        CurriculumNumber GetCurriculumnumberByAttendClass()
        {
            return CacheService.Instance.CurriculumNumbers.Find(n => {
               //当前系统时间
               var now = DateTime.Now.GetDateTimeFormats('t')[0].ToString(CultureInfo.InvariantCulture);
               //课序上课开始时间
               var csst = n.StarTime.GetDateTimeFormats('t')[0].ToString(CultureInfo.InvariantCulture);
               //课序结束时间
               var cset = n.StarTime.AddMinutes(n.Duration).GetDateTimeFormats('t')[0].ToString(CultureInfo.InvariantCulture);

               return Convert.ToDateTime(now) < Convert.ToDateTime(cset) && Convert.ToDateTime(now) >= Convert.ToDateTime(csst);
           });
        }

        /// <summary>
        /// 删除教师，同时删除教室录制文件记录
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Curriculum entity)
        {
            try
            {
                using (DbTranscationScope scope = new DbTranscationScope())
                {
                    var recordList = RecordFileService.Instance.GetByCurriculumId(entity.Id);
                    recordList.ForEach(r =>
                    {
                        RecordFileService.Instance.Delete(r);
                    });

                    base.Delete(entity);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Curriculum GetByGrade(int gradeid)
        {
            var ens = new Entities<Curriculum>("grade_id={0}", gradeid).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public Curriculum GetByCurriculumnameid(int curriculumnameid) {
            var ens = new Entities<Curriculum>("curriculum_nameid={0}", curriculumnameid).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public Curriculum GetByCurriculumnumberid(int curriculumnumberid) {
            var ens = new Entities<Curriculum>("curriculum_numberid ={0}", curriculumnumberid).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }
    }
}

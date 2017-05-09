using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;
using System.Threading.Tasks;
namespace Redcdn.ClassRoom.Facade {
    public  class CurriculumManager:ICurriculumManager {
        public CurriculumDto Create(CurriculumDto entityDto)
        {
            try {
                if (ListroomContaninMainroom(entityDto))
                    throw new BusinessException(-1, "主讲教室和听讲教室不能为同一个教室");

                if (CheckIsContainClassroom(entityDto.ListenClassRoomIds.Split(','), entityDto.MainclassRoomId))
                    throw new BusinessException(-1, "同一时间点听讲教室中不能包含上课主讲教室: " + entityDto.MainclassRoomId);

                var curriculumList = CurriculumService.Instance.Get(entityDto.WeekDayId, entityDto.CurriculumNumberId);
                var check1 =
                    curriculumList.Find(
                        c => entityDto.WeekDayId == c.WeekDayId && entityDto.GradeId == c.GradeId && entityDto.CurriculumNumberId == c.CurriculumNumberId);

                if (check1 != null) {
                    throw new BusinessException(-1, string.Format("[教室同一节:[{0}]课表信息已经存在", entityDto.MainclassRoomId));
                }

                //根据 年级、课序、星期找到匹配课表
                var check2 =curriculumList.FindAll(c => entityDto.CurriculumNumberId == c.CurriculumNumberId);

                //定义缓存符合条件的源听课教室信息
                var sourceList = new List<int>();
                var sourceMainClassroom = new List<int>();

                foreach (var c2 in check2) {
                    if (c2.Id != entityDto.Id && c2.CurriculumNumberId == entityDto.CurriculumNumberId&&c2.WeekDayId==entityDto.WeekDayId && c2.MainClassRoomId == entityDto.MainclassRoomId)
                        throw new BusinessException(-1, string.Format("同一时间点[课表id:[{0},主讲教室id:{1}]同一时间主讲教室已经在上课", c2.Id, c2.MainClassRoomId));

                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && CheckIsContainClassroom(c2.ListenClassRoomIds.Split(','), entityDto.MainclassRoomId))
                        throw new BusinessException(-1, string.Format("同一时间点主讲教室:{0},已在课表id:{1}设置了听讲教室", entityDto.MainclassRoomId, c2.Id));

                    if (c2.Id != entityDto.Id)
                        sourceMainClassroom.Add(c2.MainClassRoomId);//将主讲教室添加缓存
                    var newsourceMainClassroom = sourceMainClassroom.Distinct().ToList();

                    if (c2.Id != entityDto.Id)
                        sourceList.AddRange(c2.ListenClassRoomIds.Split(',').ToList().ConvertAll<int>(n => int.Parse(n)));
                    var newSourceList = sourceList.Distinct().ToList();

                    var descList = entityDto.ListenClassRoomIds.Split(',').ToList().ConvertAll<int>(n => int.Parse(n));
                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && c2.CurriculumNumberId == entityDto.CurriculumNumberId && newSourceList.Intersect(descList).Count() > 0) {
                        throw new BusinessException(-1, string.Format("同一时间点听讲教室ids[{0}]已经在课表id:{1}上课", string.Join(",", newSourceList.Except(descList)), c2.Id));
                    }

                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && c2.CurriculumNumberId == entityDto.CurriculumNumberId && descList.Intersect(newsourceMainClassroom).Count() > 0)
                        throw new BusinessException(-1, string.Format("同一时间点听讲教室中包含正在上课主讲教室:{0}", entityDto.MainclassRoomId));

                    if (entityDto.Id != c2.Id && entityDto.CurriculumNumberId == c2.CurriculumNumberId && entityDto.CurriculumNameId == c2.CurriculumNameId && entityDto.WeekDayId == c2.WeekDayId && c2.GradeId == entityDto.GradeId)
                        throw new BusinessException(-1, "该教室课序课表信息已经存在,不能重复添加成已经存在相同教室、相同课序的课表信息");

                }

                var module = DtoToModule(entityDto);
                var entityModule = CurriculumService.Instance.Create(module);

                return ModuleToDto(entityModule);
            } catch (BusinessException ex) {
                throw ex;
            } catch (Exception ex) {
                throw ex;
            }
        }

        bool ListroomContaninMainroom(CurriculumDto dto) 
        {
            var listenids = dto.ListenClassRoomIds.Split(',');
            if (listenids.Length > 0)
                return listenids.Contains(dto.MainclassRoomId.ToString());
            
            return false;
        }

        /// <summary>
        /// 更新课表信息
        /// ---可以更新---：
        /// 1.主讲教室、日期、课序已经存在课表信息，课时与数据库课表信息相同或不同都可更新
        /// 2.主讲教室、课序、日期课表中不存在可以更新
        /// ---不可以更新
        /// 1.主讲教室和听讲教室不能相同
        /// 2.主讲教室、日期、课序已经存在课表，教室，课序不可更新
        /// </summary>
        /// <param name="entityDto"></param>
        public void Update(CurriculumDto entityDto)
        {
            try {

                if (CheckIsContainClassroom(entityDto.ListenClassRoomIds.Split(','), entityDto.MainclassRoomId))
                    throw new BusinessException(-1, "同一时间点听讲教室中不能包含正在上课主讲教室");

                var cur1 = CurriculumService.Instance.Get(new EntityKey() { Id = entityDto.Id });
                if (cur1 == null)
                    throw new BusinessException(-1, "更新课表信息不存在");

                var curriculumList = CurriculumService.Instance.Get(entityDto.WeekDayId, entityDto.CurriculumNumberId);
                //根据 年级、课序、星期找到匹配课表
                var check2 = curriculumList.FindAll(c =>entityDto.CurriculumNumberId == c.CurriculumNumberId);

                var sourceList=new List<int>();
                var sourceMain = new List<int>();

                foreach (var c2 in check2) {
                    if (c2.Id != entityDto.Id && c2.CurriculumNumberId == entityDto.CurriculumNumberId && c2.WeekDayId == entityDto.WeekDayId && c2.MainClassRoomId == entityDto.MainclassRoomId)
                        throw new BusinessException(-1, string.Format("同一时间点[课表id:[{0},主讲教室id:{1}]同一时间主讲教室已经在上课", c2.Id, c2.MainClassRoomId));

                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && CheckIsContainClassroom(c2.ListenClassRoomIds.Split(','), entityDto.MainclassRoomId))
                        throw new BusinessException(-1, string.Format("同一时间点主讲教室:{0},已在课表id:{1}设置了听讲教室", entityDto.MainclassRoomId, c2.Id));

                    if (c2.Id != entityDto.Id)
                        sourceMain.Add(c2.MainClassRoomId);//将主讲教室添加缓存
                    var newsourceMainClassroom = sourceMain.Distinct().ToList();

                    if (c2.Id != entityDto.Id)
                        sourceList.AddRange(c2.ListenClassRoomIds.Split(',').ToList().ConvertAll<int>(n => int.Parse(n)));
                    var newSourceList = sourceList.Distinct().ToList();

                    var descList = entityDto.ListenClassRoomIds.Split(',').ToList().ConvertAll<int>(n => int.Parse(n));
                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && c2.CurriculumNumberId == entityDto.CurriculumNumberId && newSourceList.Intersect(descList).Count() > 0) {
                        throw new BusinessException(-1, string.Format("同一时间点听讲教室ids[{0}]已经在课表id:{1}上课", string.Join(",", newSourceList.Except(descList)), c2.Id));
                    }

                    if (c2.Id != entityDto.Id && c2.GradeId != entityDto.GradeId && c2.CurriculumNumberId == entityDto.CurriculumNumberId && descList.Intersect(newsourceMainClassroom).Count()>0)
                        throw new BusinessException(-1, string.Format("同一时间点听讲教室中包含正在上课主讲教室:{0}", entityDto.MainclassRoomId));

                    if (entityDto.Id != c2.Id && entityDto.CurriculumNumberId == c2.CurriculumNumberId && entityDto.CurriculumNameId == c2.CurriculumNameId && entityDto.WeekDayId == c2.WeekDayId && c2.GradeId == entityDto.GradeId)
                        throw new BusinessException(-1, "该教室课序课表信息已经存在,不能修改成已经存在相同教室、相同课序的课表信息");

                }

                cur1.Id = entityDto.Id;
                cur1.Name = entityDto.Name ?? "";
                cur1.WeekDayId = entityDto.WeekDayId;
                cur1.CurriculumNameId = entityDto.CurriculumNameId;
                cur1.GradeId = entityDto.GradeId;
                cur1.CurriculumNumberId = entityDto.CurriculumNumberId;
                cur1.MainClassRoomId = entityDto.MainclassRoomId;
                cur1.ListenClassRoomIds = entityDto.ListenClassRoomIds;
                cur1.IsRecord = entityDto.IsRecord;
                cur1.IsLocalRecord = entityDto.IsLocalRecord;
                cur1.IsPush = entityDto.IsPush;
                cur1.IsPushRemote = entityDto.IsPushRemote;
                cur1.IsRecordRemote = entityDto.IsRecordRemote;

                CurriculumService.Instance.Update(cur1);

            } catch (BusinessException ex) {
                throw ex;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void UpdateCurriculumMeetingNo(int curriculumid, string classroomimie, int meetingid, long mettingstarttime)
        {
            try
            {
                CurriculumService.Instance.UpdateCurriculumMeetingNo(curriculumid, classroomimie, meetingid, mettingstarttime);
            }
            catch (BusinessException bex)
            {
                throw new BusinessException(bex.ErrCode,bex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CurriculumManagerDto(CurriculumDto entityDto)
        {
            var modult = new Curriculum();
            try
            {
                AutoMapperWrapper.Instance.Map(entityDto, modult);
                CurriculumService.Instance.Update(modult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var module = CurriculumService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new Exception("课时不存在");

                CurriculumService.Instance.Delete(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CurriculumDto Get(int id)
        {
            try
            {
                var module= CurriculumService.Instance.Get(new EntityKey() {Id = id});
                return module == null ? null : ModuleToDto(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CurriculumDto Get(int curriculumid, string classroomimie)
        {
            try {
                var module = CurriculumService.Instance.GetCurriculum(curriculumid, classroomimie);
                return module == null ? null : ModuleToDto(module);
            } catch (Exception ex) {
                throw ex;
            }
        }


        public IList<CurriculumDto> GetList(int mainClassRoomId)
        {
            var list = new List<CurriculumDto>();
            try {
               var modules= CurriculumService.Instance.GetList(mainClassRoomId);
               foreach (Curriculum curriculum in modules)
                {
                    list.Add(ModuleToDto(curriculum));
                }
            } catch (Exception ex) {

                throw ex;
            }
            return list;
        }

        public PagingQueryResultDto<CurriculumDto> GetList(ComplexQueryParameter queryParam, ContentQueryPageParameter queryPagingParam)
        {
            try
            {
                if (queryParam == null)
                {
                    PagingQueryEntityResult<Curriculum> pqResult = CurriculumService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);
                    return GetListExtracted(pqResult);
                }
                else
                {
                    PagingQueryEntityResult<Curriculum> pqResult = CurriculumService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize, new Curriculum()
                    {
                        WeekDayId = queryParam.WeekDayId,
                        MainClassRoomId = queryParam.MainClassRoomId,
                        CurriculumNumberId = queryParam.CurriculumNumberId,
                        GradeId = queryParam.GradeId,
                        CurriculumNameId = queryParam.CurriculumNameId
                    });

                    return GetListExtracted(pqResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PagingQueryResultDto<CurriculumDto> GetListExtracted(PagingQueryEntityResult<Curriculum> pqResult) {
            var list = new PagingQueryResultDto<CurriculumDto>();
            if (pqResult != null && pqResult.Result.Count > 0) {
                pqResult.Result.ForEach(module => {
                   // AutoMapperWrapper.Instance.Map(module, dto);
                    list.Result.Add(ModuleToDto(module));
                });
            }
            list.TotalCount = pqResult.TotalCount;
            return list;
        }

        /// <summary>
        /// 判断听课教室中是否包含设置主讲教室
        /// </summary>
        /// <param name="listenClassrooms"></param>
        /// <param name="mainClassroomId"></param>
        /// <returns></returns>
        bool CheckIsContainClassroom(string[] listenClassrooms,int mainClassroomId)
        {
            foreach (string classroom in listenClassrooms)
            {
                if (mainClassroomId == int.Parse(classroom))
                    return true;
            }
            return false;
        }

    
    

        Curriculum DtoToModule(CurriculumDto dto)
        {
            var cur = new Curriculum();
            return AutoMapperWrapper.Instance.Map<CurriculumDto, Curriculum>(dto, cur);
        }

        CurriculumDto ModuleToDto(Curriculum module)
        {
            WeekDay weekday = CacheService.Instance.WeekDays.Find(w => w.Id == module.WeekDayId);
            Grade grade = CacheService.Instance.Grades.Find(g => g.Id == module.GradeId);
            CurriculumName curriculumName = CacheService.Instance.CurriculumNames.Find(c => c.Id==module.CurriculumNameId);
            CurriculumNumber curriculumNumber =CacheService.Instance.CurriculumNumbers.Find(c => c.Id == module.CurriculumNumberId);

            var mainRoomName = SchoolRoomService.Instance.Get(new EntityKey() {Id = module.MainClassRoomId}).SchoolRoomName;
            var roomNames = SchoolRoomService.Instance.GetRoomNames(module.ListenClassRoomIds);

            return new CurriculumDto()
            {
                Id = module.Id,
                Name = module.Name,
                MettingId = module.MettingId,
                WeekDayId = module.WeekDayId,
                WeekDayName = weekday!=null?weekday.Name:"",
                GradeId = module.GradeId,
                GradeName = grade != null ? grade.Name : "", 
                CurriculumNameId = module.CurriculumNameId,
                CurriculumNameName = curriculumName != null ? curriculumName.Name : "",
                CurriculumNumberId = module.CurriculumNumberId,
                CurriculumNumberName = curriculumNumber != null ? curriculumNumber.Name : "",
                MainclassRoomId = module.MainClassRoomId,
                MainClassRoomName = mainRoomName,
                ListenClassRoomIds = module.ListenClassRoomIds,
                ListenClassRoomNames = roomNames,
                IsPush = module.IsPush,
                IsRecord = module.IsRecord,
                IsPushRemote = module.IsPushRemote,
                IsRecordRemote = module.IsRecordRemote,
                IsLocalRecord = module.IsLocalRecord,
                MettingBeginDateTime = module.MettingBeginDateTime
            };
        }
    }
}

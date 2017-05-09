using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade
{
    public class RecordFileManager : IRecordFileManager
    {
        public RecordFileDto Create(RecordFileDto entityDto)
        {
            RecordFile entityModule=null;
            try
            {
                var module = DtoToModule(entityDto);
                 entityModule = RecordFileService.Instance.Create(module);
                return ModuleToDto(entityModule);
            }
            catch (Exception ex)
            {
                if (entityModule!=null)
                    RecordFileService.Instance.Delete(entityModule);

                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var module = RecordFileService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new Exception("录制文件不存在");

                RecordFileService.Instance.Delete(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecordFileDto Get(int id)
        {
            try
            {
                var module = RecordFileService.Instance.Get(new EntityKey() { Id = id });
                return ModuleToDto(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRecordFileDtoManagerDto(RecordFileDto entityDto)
        {
          //  var modult = DtoToModule(entityDto);
            try
            {
                var module = RecordFileService.Instance.Get(new EntityKey() { Id = entityDto.Id });
                if (module == null)
                    throw new Exception("录制文件不存在");

                module.Id = entityDto.Id;
                module.CurriculumId = entityDto.CurriculumId;
                module.StartTime = entityDto.StartTime;
                module.Duration = entityDto.Duration;
                module.RecordFilePlayUrl = entityDto.FilePlayUrl;
                module.ClassNo = entityDto.ClassNo;
                module.ClassroomId = entityDto.ClassroomId;
                module.RecordStatus = entityDto.RecordStatus;
                RecordFileService.Instance.Update(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        RecordFile DtoToModule(RecordFileDto dto)
        {
            return new RecordFile()
            {
                Id = dto.Id,
                CurriculumId = dto.CurriculumId,
                StartTime = dto.StartTime,
                Duration = dto.Duration,
                Name = dto.MainClassRoomName,
                RecordFilePlayUrl = dto.FilePlayUrl,
                PhysicalFileImOuterId = dto.PhysicalFileImOuterId,
                ClassNo = dto.ClassNo,
                ClassroomId = dto.ClassroomId,
                RecordStatus = dto.RecordStatus
            };
        }

        public PagingQueryResultDto<RecordFileDto> GetList(ComplexQueryParameter queryParam, ContentQueryPageParameter queryPagingParam)
        {
            try
            {
                var records = new PagingQueryResultDto<RecordFileDto>();
                if (queryParam.WeekDayId == 0 && queryParam.CurriculumNumberId == 0 && queryParam.GradeId == 0 && queryParam.MainClassRoomId == 0&&queryParam.CurriculumNameId==0)
                {
                    PagingQueryEntityResult<RecordFile> pqResult = RecordFileService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);
                    records.Result.AddRange(GetListExtracted(pqResult));
                    records.TotalCount = pqResult.TotalCount;
                }
                else if (queryParam.WeekDayId == 0 && queryParam.CurriculumNumberId == 0 && queryParam.GradeId == 0 &&queryParam.MainClassRoomId != 0 && queryParam.CurriculumNameId == 0)
                {
                    PagingQueryEntityResult<RecordFile> pqResult = RecordFileService.Instance.GetListByClassroomId(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize, queryParam.MainClassRoomId);

                    if (pqResult == null) return new PagingQueryResultDto<RecordFileDto>();


                    var rfdtos = GetListExtracted(pqResult);

                    records.TotalCount = rfdtos.Count;
                    records.Result.AddRange(rfdtos);
                    return records;
                }
                else
                {
                    List<Curriculum> temp = CurriculumService.Instance.GetListNoPaging(new Curriculum()
                    {
                        WeekDayId = queryParam.WeekDayId,
                        MainClassRoomId = queryParam.MainClassRoomId,
                        CurriculumNumberId = queryParam.CurriculumNumberId,
                        GradeId = queryParam.GradeId,
                        CurriculumNameId = queryParam.CurriculumNameId
                    });

                    if (temp == null) return new PagingQueryResultDto<RecordFileDto>();

                    var record=new PagingQueryEntityResult<RecordFile>();
                    int totalcount = 0;
                    foreach (var item in temp)
                    {
                        record = RecordFileService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize, new RecordFile()
                        {
                            CurriculumId = item.Id
                        });

                        if(record.Result.Count==0)
                            continue;

                        var rfdto = GetListExtracted(record);
                        records.Result.AddRange(rfdto);
                        totalcount += rfdto.Count;
                    }
                    records.TotalCount = totalcount;
                }
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartRecord(TerminalRecordParamDto dto)
        {
            try
            {
                RecordFileService.Instance.StartRecord(new TerminalRecordParam()
                {
                    ChannelOuterId = dto.ChannelOuterId,
                    RecordFileOuterId = dto.RecordFileOuterId,
                    Context = dto.Context,
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StopRecord(TerminalRecordParamDto dto)
        {
            try
            {
                 RecordFileService.Instance.StopRecord(new TerminalRecordParam()
                 {
                     ChannelOuterId = dto.ChannelOuterId,
                 });
            } catch (Exception ex) {
                throw ex;
            }
        }

        private List<RecordFileDto> GetListExtracted(PagingQueryEntityResult<RecordFile> pqResult)
        {
            var list = new List<RecordFileDto>();
            if (pqResult != null && pqResult.Result.Count > 0)
            {
                pqResult.Result.ForEach(module =>
                {
                    var dto = ModuleToDto(module);
                    if (dto != null)
                        list.Add(ModuleToDto(module));
                });
            }
            return list;
        }

        RecordFileDto ModuleToDto(RecordFile module)
        {
            if (module.CurriculumId != 0)
            {
                CurriculumDto crrdto = FacadeFactory.Instance.Get<ICurriculumManager>().Get(module.CurriculumId);
                if (crrdto == null)
                    return null;

                WeekDay weekday = CacheService.Instance.WeekDays.Find(w => w.Id == crrdto.WeekDayId);
                Grade grade = CacheService.Instance.Grades.Find(g => g.Id == crrdto.GradeId);
                CurriculumName curriculumName = CacheService.Instance.CurriculumNames.Find(c => c.Id == crrdto.CurriculumNameId);
                CurriculumNumber curriculumNumber = CacheService.Instance.CurriculumNumbers.Find(c => c.Id == crrdto.CurriculumNumberId);

                return new RecordFileDto()
                {
                    Id = module.Id,
                    WeekDayName = weekday != null ? weekday.Name : "",
                    CurriculumId = module.CurriculumId,
                    GradeName = grade != null ? grade.Name : "",
                    CurriculumNumber = curriculumNumber != null ? curriculumNumber.Name : "",
                    CurriCulumName = curriculumName != null ? curriculumName.Name : "",
                    Duration = module.Duration,
                    MainClassRoomName =SchoolRoomService.Instance.Get(new EntityKey() {Id = crrdto.MainclassRoomId}).SchoolRoomName,
                    ListenClassRooms = SchoolRoomService.Instance.GetRoomNames(crrdto.ListenClassRoomIds),
                    StartTime = module.StartTime,
                    FilePlayUrl = module.RecordFilePlayUrl,
                    ClassNo = module.ClassNo,
                    PhysicalFileImOuterId = module.PhysicalFileImOuterId,
                    FileSequence = module.FileSequence
                    //IsRecord = module.IsRecord
                };
            }
            else
            {

                var school = SchoolRoomService.Instance.Get(new EntityKey() {Id = module.ClassroomId});
                if (school==null)
                    return null;

                return new RecordFileDto() {
                    Id = module.Id,
                    WeekDayName =  "",
                    CurriculumId = module.CurriculumId,
                    GradeName =  "",
                    CurriculumNumber =  "",
                    CurriCulumName =  "",
                    Duration = module.Duration,
                    MainClassRoomName = school.SchoolRoomName,
                    ListenClassRooms = "",
                    StartTime = module.StartTime,
                    FilePlayUrl = module.RecordFilePlayUrl,
                    ClassNo = module.ClassNo,
                    PhysicalFileImOuterId = module.PhysicalFileImOuterId,
                    FileSequence = module.FileSequence
                };
            }
        }
    }
}

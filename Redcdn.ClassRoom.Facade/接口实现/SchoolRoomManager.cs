using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class SchoolRoomManager:ISchoolRoomManager {
        public SchoolRoomDto Create(SchoolRoomDto entityDto)
        {
            var dto = new SchoolRoomDto();
            var module = new SchoolRoom();
            try{
                AutoMapperWrapper.Instance.Map(entityDto, module);
                var entity= SchoolRoomService.Instance.Create(module);
                return AutoMapperWrapper.Instance.Map(entity, dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(SchoolRoomDto entityDto)
        {
            try{
                var module = new SchoolRoom();
                AutoMapperWrapper.Instance.Map(entityDto, module);

                var entity=SchoolRoomService.Instance.Get(new EntityKey() {Id = module.Id});
                if (entity == null)
                    throw new Exception("更新教室不存在或已经删除");

                if (entityDto.SchoolRoomNum != entity.SchoolRoomNum)
                    NumberPoolService.Instance.CannelNumberPool2SchoolRoomMapping(entity.SchoolRoomNum);

                entity.SchoolRoomName = module.SchoolRoomName;
                entity.SchoolRoomNum = module.SchoolRoomNum;
                entity.SchoolRoomIMEI = module.SchoolRoomIMEI;
                entity.SchoolRoomAddress = module.SchoolRoomAddress;
                entity.Token = entity.Token;
                entity.PlayStreamUrl = entity.PlayStreamUrl;
                entity.PushStreamUrl = entity.PushStreamUrl;
                entity.Remark = module.Remark;
                entity.PhysicalChannelOuterId = entity.PhysicalChannelOuterId;

                SchoolRoomService.Instance.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try {
                var module = SchoolRoomService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new BusinessException(-1,"删除教室不存在");

                bool isBingCurriculum = CurriculumService.Instance.GetAll().Exists(c =>
                {
                    if (c.MainClassRoomId == id)
                        return true;

                    foreach (string listenroom in c.ListenClassRoomIds.Split(',').ToList())
                    {
                        if (listenroom == id.ToString())
                            return true;
                    }

                    return false;
                });
                if (isBingCurriculum)
                    throw new BusinessException(-1, "教室已经绑定课表不能删除");

                SchoolRoomService.Instance.Delete(module);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public SchoolRoomDto GetByImei(string imei)
        {
            var dto = new SchoolRoomDto();
            try {
                var module = SchoolRoomService.Instance.GetByImei(imei);
                if (module != null)
                    return AutoMapperWrapper.Instance.Map(module, dto);
            } catch (Exception ex) {
                throw ex;
            }

            return null;
        }

        public SchoolRoomDto GetByImeiForPcClient(string imei) {
            var dto = new SchoolRoomDto();
            try {
                var module = SchoolRoomService.Instance.GetByImeiForPcClient(imei);
                if (module != null)
                    return AutoMapperWrapper.Instance.Map(module, dto);
            } catch (Exception ex) {
                throw ex;
            }

            return null;
        }

        public SchoolRoomDto GetByToken(string token)
        {
            var dto = new SchoolRoomDto();
            try {
                var module = SchoolRoomService.Instance.GetByToken(token);
                if (module != null)
                    return AutoMapperWrapper.Instance.Map(module, dto);

            } catch (Exception ex) {
                throw ex;
            }
            return null;
        }

        public SchoolRoomDto GetByNubeNumber(string nubeNumber)
        {
            var dto = new SchoolRoomDto();
            try {
                var module = SchoolRoomService.Instance.GetByNubeNumber(nubeNumber);
                if (module != null)
                    return AutoMapperWrapper.Instance.Map(module, dto);

            } catch (Exception ex) {
                throw ex;
            }
            return null;
        }

        public SchoolRoomDto GetSchoolRoomById(int id)
        {
            var dto = new SchoolRoomDto();
            try {
                var module = SchoolRoomService.Instance.Get(new EntityKey() { Id = id });
                return AutoMapperWrapper.Instance.Map(module, dto);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public PagingQueryResultDto<SchoolRoomDto> GetAll(ContentQueryPageParameter queryPagingParam)
        {
            var list =new PagingQueryResultDto<SchoolRoomDto>();
            try {
                PagingQueryEntityResult<SchoolRoom> pagingQueryEntityResult = SchoolRoomService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);
                if (pagingQueryEntityResult != null && pagingQueryEntityResult.Result.Count != 0) {
                   
                    foreach (SchoolRoom schoolroom in pagingQueryEntityResult.Result) {
                        var dto = new SchoolRoomDto();
                        AutoMapperWrapper.Instance.Map(schoolroom, dto);

                        list.Result.Add(dto);
                    }
                }
                list.TotalCount = pagingQueryEntityResult.TotalCount;
            } catch (Exception ex) {
                throw ex;
            }

            return list;
        }

        public IList<SchoolRoomDto> GetAll()
        {
            var list = new List<SchoolRoomDto>();
            try {
                var lisModule = SchoolRoomService.Instance.GetAll();
                foreach (var schoolRoom in lisModule) {
                    var dto = new SchoolRoomDto();
                    AutoMapperWrapper.Instance.Map(schoolRoom, dto);

                    list.Add(dto);
                }
                return list;

            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}

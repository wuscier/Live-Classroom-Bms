using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade
{
    public class LiveClassRoomManager : ILiveClassRoomManager
    {
        public void RegisterLive(LiveClassRoomDto entity) {
            var module = new LiveClassroom();
            try {
                AutoMapperWrapper.Instance.Map(entity, module);
                LiveClassroomService.Instance.RegisterLive(module);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void SendLiveStreamStatus(string classroomimie) {
            try {
                LiveClassroomService.Instance.SendLiveStreamStatus(classroomimie);
            } catch (BusinessException bex) {
                throw new BusinessException(-1, bex.Message);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public LiveClassRoomDto Get(int id)
        {
            var dto = new LiveClassRoomDto();
            try
            {
                var module = LiveClassroomService.Instance.GetByTimeout(id);
                AutoMapperWrapper.Instance.Map(module, dto);

                var now = DateTime.Now.GetDateTimeFormats('T')[0];
                var csst = dto.LiveStreamBeginTime;
                var dts = Convert.ToDateTime(now);
                var dte = Convert.ToDateTime(csst);
                int sencods = (int) dts.Subtract(dte).TotalSeconds;
                dto.Duration = sencods;

                return dto;

            }
            catch (BusinessException bex)
            {
                throw new BusinessException(bex.ErrCode,bex.Message);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public PagingQueryResultDto<LiveClassRoomDto> GetList(ContentQueryPageParameter queryPagingParam)
        {
            try
            {
                PagingQueryEntityResult<LiveClassroom> pqResult = LiveClassroomService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);

                if (pqResult == null) return new PagingQueryResultDto<LiveClassRoomDto>();

                return GetListExtracted(pqResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PagingQueryResultDto<LiveClassRoomDto> GetListExtracted(PagingQueryEntityResult<LiveClassroom> pqResult)
        {
            var list = new PagingQueryResultDto<LiveClassRoomDto>();
            if (pqResult != null && pqResult.Result.Count > 0) {
                pqResult.Result.ForEach(module => {
                    var dto = new LiveClassRoomDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    list.Result.Add(dto);
                });
                list.TotalCount = pqResult.TotalCount;
            }
            return list;
        }

    }
}

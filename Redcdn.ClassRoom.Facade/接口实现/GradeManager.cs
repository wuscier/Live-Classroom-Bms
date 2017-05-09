using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class GradeManager:IGradeManager {
        public IList<GradeDto> GetAll()
        {
            var dtoList = new List<GradeDto>();

            try {
                var listModule= GradeService.Instance.GetAll();
                foreach (Grade curriculumNumber in listModule) {
                    var dto = new GradeDto();

                    AutoMapperWrapper.Instance.Map(curriculumNumber, dto);

                    dtoList.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtoList;
        }

        public GradeDto Get(int id)
        {
            var dto = new GradeDto();
            try
            {
                var entity = GradeService.Instance.Get(new EntityKey() { Id = id });
                return AutoMapperWrapper.Instance.Map(entity, dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GradeDto Create(GradeDto entityDto)
        {
            var dto = new GradeDto();
            var module = new Grade();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                var entity = GradeService.Instance.Create(module);
                return AutoMapperWrapper.Instance.Map(entity, dto);
            } catch (BusinessException ex) {
                throw ex;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void Update(GradeDto entityDto)
        {
            var module = new Grade();
            try
            {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                GradeService.Instance.Update(module);
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
                var module = GradeService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new BusinessException(-1, "年级信息不存在");

                GradeService.Instance.Delete(module);
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagingQueryResultDto<GradeDto> GetList(ContentQueryPageParameter queryPagingParam) {
            try {
                PagingQueryEntityResult<Grade> pqResult = GradeService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);

                if (pqResult == null) return new PagingQueryResultDto<GradeDto>();

                return GetListExtracted(pqResult);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private PagingQueryResultDto<GradeDto> GetListExtracted(PagingQueryEntityResult<Grade> pqResult) {
            var list = new PagingQueryResultDto<GradeDto>();
            if (pqResult != null && pqResult.Result.Count > 0) {
                pqResult.Result.ForEach(module => {
                    var dto = new GradeDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    list.Result.Add(dto);
                });
                list.TotalCount = pqResult.TotalCount;
            }
            return list;
        }
    } 
}

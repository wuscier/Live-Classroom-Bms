using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class CurriculumNameManager:ICurriculumNameManager {
        public IList<CurriculumNameDto> GetAll()
        {
            var dtoList = new List<CurriculumNameDto>();

            try {
                var listModule= CurriculumNameService.Instance.GetAll();
                foreach (CurriculumName curriculumName in listModule) {
                    var dto = new CurriculumNameDto();
                    AutoMapperWrapper.Instance.Map(curriculumName, dto);
                    dtoList.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtoList;
        }

        public CurriculumNameDto Create(CurriculumNameDto entityDto)
        {
            var dto = new CurriculumNameDto();
            var module = new CurriculumName();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                var entity = CurriculumNameService.Instance.Create(module);
                return AutoMapperWrapper.Instance.Map(entity, dto);
            } catch (BusinessException ex) {
                throw ex;
            } catch (Exception ex) {
                throw;
            }
        }

        public void Update(CurriculumNameDto entityDto)
        {
            var module = new CurriculumName();
            try
            {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                CurriculumNameService.Instance.Update(module);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try {
                var module = CurriculumNameService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new BusinessException(-1, "课时信息不存在");

                CurriculumNameService.Instance.Delete(module);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public PagingQueryResultDto<CurriculumNameDto> GetList(ContentQueryPageParameter queryPagingParam)
        {
            try {
                PagingQueryEntityResult<CurriculumName> pqResult = CurriculumNameService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);

                if (pqResult == null) return new PagingQueryResultDto<CurriculumNameDto>();

                return GetListExtracted(pqResult);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public CurriculumNameDto Get(int id) {
            var dto = new CurriculumNameDto();
            try {
                var entity = CurriculumNameService.Instance.Get(new EntityKey() { Id = id });
                return AutoMapperWrapper.Instance.Map(entity, dto);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private PagingQueryResultDto<CurriculumNameDto> GetListExtracted(PagingQueryEntityResult<CurriculumName> pqResult) {
            var list = new PagingQueryResultDto<CurriculumNameDto>();
            if (pqResult != null && pqResult.Result.Count > 0) {
                pqResult.Result.ForEach(module => {
                    var dto = new CurriculumNameDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    list.Result.Add(dto);
                });
                list.TotalCount = pqResult.TotalCount;
            }
            return list;
        }
    }
}

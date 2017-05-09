using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    class CurriculumNumberManager:ICurriculumNumberManager {
        public IList<CurriculumNumberDto> GetAll()
        {
            var dtoList = new List<CurriculumNumberDto>();

            try {
                var listModule = CurriculumNumberService.Instance.GetAll();
                foreach (CurriculumNumber curriculumNumber in listModule) {
                    var dto = new CurriculumNumberDto();
                    AutoMapperWrapper.Instance.Map(curriculumNumber, dto);
                    dtoList.Add(dto);
                }
            } catch (Exception ex) {
                throw ex;
            }

            return dtoList;
        }

        public CurriculumNumberDto Get(int id)
        {
            var dto = new CurriculumNumberDto();
            try
            {
                var entity = CurriculumNumberService.Instance.Get(new EntityKey(){Id = id});
               return AutoMapperWrapper.Instance.Map(entity, dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CurriculumNumberDto Create(CurriculumNumberDto entityDto)
        {
            var dto = new CurriculumNumberDto();
            var module = new CurriculumNumber();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                var entity = CurriculumNumberService.Instance.Create(module);
                return AutoMapperWrapper.Instance.Map(entity, dto);
            } catch (BusinessException ex) {
                throw ex;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void Update(CurriculumNumberDto entityDto)
        {
            var module = new CurriculumNumber();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                CurriculumNumberService.Instance.Update(module);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try {
                var module = CurriculumNumberService.Instance.Get(new EntityKey() { Id = id });
                if (module == null)
                    throw new BusinessException(-1, "年级信息不存在");

                CurriculumNumberService.Instance.Delete(module);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public PagingQueryResultDto<CurriculumNumberDto> GetList(ContentQueryPageParameter queryPagingParam)
        {
            try {
                PagingQueryEntityResult<CurriculumNumber> pqResult = CurriculumNumberService.Instance.GetList(queryPagingParam.OrderFields, queryPagingParam.OrderFlag, queryPagingParam.StartIndex, queryPagingParam.PageSize);

                if (pqResult == null) return new PagingQueryResultDto<CurriculumNumberDto>();

                return GetListExtracted(pqResult);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private PagingQueryResultDto<CurriculumNumberDto> GetListExtracted(PagingQueryEntityResult<CurriculumNumber> pqResult) {
            var list = new PagingQueryResultDto<CurriculumNumberDto>();
            if (pqResult != null && pqResult.Result.Count > 0) {
                pqResult.Result.ForEach(module => {
                    var dto = new CurriculumNumberDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    list.Result.Add(dto);
                });
                list.TotalCount = pqResult.TotalCount;
            }
            return list;
        }
    }
}

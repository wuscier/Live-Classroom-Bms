using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class CurriculumNumberService : EntityService<EntityKey, CurriculumNumber, CurriculumNumberService>
    {
        public int GetIdByName(string name) {

            var entity = GetAll().Find(cn => cn.Name == name);
            if (entity != null)
                return entity.Id;
            return 0;
        }

        public override CurriculumNumber Create(CurriculumNumber entity)
        {
            var ccnumber = GetByName(entity.Name);
            if (ccnumber != null)
                throw new BusinessException(-1, "已经存在相同课序名称");

            var ens = base.Create(entity);
            CacheService.Instance.ReLoad<CurriculumNumber>(ens);
            return ens;
        }

        public override void Update(CurriculumNumber entity)
        {
            var ccnumber = GetByName(entity.Name);
            if (ccnumber == null) {
                base.Update(entity);
            } else {
                if (entity.Id != ccnumber.Id)
                    throw new BusinessException(-1, "已经存在相同课序名称");
                base.Update(entity);
            }

            CacheService.Instance.ReLoad<CurriculumNumber>(entity);
        }

        public override void Delete(CurriculumNumber entity) {

            var curriculumnumber = CurriculumService.Instance.GetByCurriculumnumberid(entity.Id);
            if (curriculumnumber != null)
                throw new BusinessException(-1, "课序不能删除，已经绑定课表信息");

            base.Delete(entity);
            CacheService.Instance.ReLoad<CurriculumNumber>(entity);
        }

        CurriculumNumber GetByName(string name) {
            var ens = new Entities<CurriculumNumber>("name='{0}'", name).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }

        public PagingQueryEntityResult<CurriculumNumber> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize) {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<CurriculumNumber>(context).PagingCache;
        }
    }
}

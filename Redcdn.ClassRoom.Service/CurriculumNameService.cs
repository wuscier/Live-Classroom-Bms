using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class CurriculumNameService : EntityService<EntityKey, CurriculumName, CurriculumNameService>
    {
        public int GetIdByName(string name) {

            var entity = GetAll().Find(cn => cn.Name == name);
            if (entity != null)
                return entity.Id;
            return 0;
        }

        public override CurriculumName Create(CurriculumName entity) {
            var curriculumname = GetByName(entity.Name);
            if (curriculumname != null)
                throw new BusinessException(-1, "已经存在相同课时名称");

            var ens = base.Create(entity);
            CacheService.Instance.ReLoad<CurriculumName>(ens);
            return ens;
        }

        public override void Update(CurriculumName entity)
        {
            var curriculumname = GetByName(entity.Name);
            if (curriculumname == null)
            {
                base.Update(entity);
            }
            else
            {
                if (entity.Id != curriculumname.Id)
                    throw new BusinessException(-1, "已经存在相同课时名称");
                base.Update(entity);
            }
         
            CacheService.Instance.ReLoad<CurriculumName>(entity);
        }

        public override void Delete(CurriculumName entity)
        {
            var curriculumname=CurriculumService.Instance.GetByCurriculumnameid(entity.Id);
            if (curriculumname!=null)
                throw new BusinessException(-1, "课时不能删除，已经绑定课表信息");

            base.Delete(entity);
            CacheService.Instance.ReLoad<CurriculumName>(entity);
        }

        public PagingQueryEntityResult<CurriculumName> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize) {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<CurriculumName>(context).PagingCache;
        }

        CurriculumName GetByName(string name) {
            var ens = new Entities<CurriculumName>("name='{0}'", name).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }
    }
}

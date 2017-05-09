using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class GradeService : EntityService<EntityKey,Grade,GradeService>
    {
        public int GetIdByName(string name) {

            var entity = GetAll().Find(gs => gs.Name == name);
            if (entity != null)
                return entity.Id;
            return 0;
        }

        public override Grade Create(Grade entity)
        {
            var grade = GetByName(entity.Name);
            if(grade!=null)
                throw new BusinessException(-1,"已经存在相同年级名称");

            var ens= base.Create(entity);
            CacheService.Instance.ReLoad<Grade>(ens);
            return ens;
        }

        public override void Update(Grade entity)
        {
            var grade = GetByName(entity.Name);
            if (grade == null)
            {
                base.Update(entity);
            }
            else 
            {
                if (entity.Id != grade.Id)
                    throw new BusinessException(-1, "已经存在相同年级名称");
                base.Update(entity);
            }

            CacheService.Instance.ReLoad<Grade>(entity);
        }

        public override void Delete(Grade entity)
        {
            var curriculum= CurriculumService.Instance.GetByGrade(entity.Id);
            if (curriculum!=null)
                throw new BusinessException(-1, "不能删除年级,该年级已经绑定了课表信息");

            base.Delete(entity);
            CacheService.Instance.ReLoad<Grade>(entity);
        }

        public PagingQueryEntityResult<Grade> GetList(List<string> orderFields, bool orderFlag, int startIndex,int pageSize)
        {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<Grade>(context).PagingCache;
        }

        Grade GetByName(string name)
        {
            var ens = new Entities<Grade>("name='{0}'", name).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }
    }
}

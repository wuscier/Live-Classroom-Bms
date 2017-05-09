using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Service {
    public class EntityService<TEntityKey, TEntity, TService>
        where TEntity : Entity
        where TEntityKey : EntityKey
        where TService : EntityService<TEntityKey, TEntity, TService>, new() {
        static TService _service = new TService();

        public static TService Instance {
            get { return _service; }
        }

        public virtual TEntity Get(TEntityKey key) {
            List<TEntity> listEntity = InnerGet(key);
            if (listEntity.Count == 0)
                return null;
            else if (listEntity.Count == 1)
                return listEntity[0];

            throw new ArgumentOutOfRangeException("通过Key" + key.ToString() + "查找到" + listEntity.Count + "个对象。key值不唯一");
        }

        public virtual void Delete(TEntity entity) {
            new Entities<TEntity>().Remove(entity);
        }

        public virtual TEntity Create(TEntity entity) {
            TEntity newEntity = new Entities<TEntity>().Add(entity);
            newEntity.EntityKey = CreateEntityKey(newEntity);
            return newEntity;
        }

        public virtual void Update(TEntity entity) {
            (entity as IEntity).Update();
        }

        public virtual List<TEntity> GetAll() {
            return new Entities<TEntity>().Cache;
        }

        protected virtual List<TEntity> InnerGet(TEntityKey key) {
            return new Entities<TEntity>("Id = " + key.Id).Cache;
        }

        protected virtual EntityKey CreateEntityKey(TEntity entity) {
            return new EntityKey() { Id = entity.Id };
        }
    }
}

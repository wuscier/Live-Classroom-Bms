using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service
{
    public class RemoteChannelService : EntityService<EntityKey, RemoteChannel, RemoteChannelService>
    {
        public override RemoteChannel Create(RemoteChannel entity)
        {
            if (entity.Name == null)
                entity.Name = "";
            return base.Create(entity);
        }

        public override void Update(RemoteChannel entity)
        {
            if (entity.Name == null)
                entity.Name = "";
            base.Update(entity);
        }

        public RemoteChannel GetById(int id)
        {
            var key = new EntityKey() {Id = id};
            return Get(key);
        }
    }
}

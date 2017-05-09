using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using GM.Orm.Db;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class LiveClassroomService : EntityService<EntityKey, LiveClassroom, LiveClassroomService>
    {
        public void RegisterLive(LiveClassroom entity)
        {
            var module = Get(entity.ClassRoomImie);
            if (module != null) {
                module.ClassRoomName = entity.ClassRoomName;
                module.GradeName = entity.GradeName;
                module.CurriculumName = entity.CurriculumName;
                module.LiveStreamUrl = entity.LiveStreamUrl;
                module.LiveStreamBeginTime = entity.LiveStreamBeginTime;

                (module as IEntity).Update();
            } else {
                base.Create(entity);
            }
        }

        public void SendLiveStreamStatus(string classroomimie)
        {
            var module = Get(classroomimie);
            if (module == null)
               throw new BusinessException(-1,"更新直播课堂心跳失败,直播教室不存在");
            
            (module as IEntity).Update();
        }

        public  LiveClassroom Get(string classroomimie)
        {
            var ens = new Entities<LiveClassroom>("classroom_imie='{0}'", classroomimie).Cache;
            return ens.Count > 0 ? ens[0] : null;
        }


        public LiveClassroom GetByTimeout(int id)
        {
            var module = Get(new EntityKey(){Id = id});
            if(module==null)
                throw new BusinessException(-1, "直播教室不存在");

            int timeout = SettingConfig.Instance.LiveTimeout;

            var dtnow = DateTime.Now.AddSeconds(-timeout);
            if (module.UpdateTime >= dtnow)
                return module;

            throw new BusinessException(-1,"直播教室超时");
        }

        public PagingQueryEntityResult<LiveClassroom> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize)
        {
            int timeout = SettingConfig.Instance.LiveTimeout;

            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            var dbCurrentDatabaseTime = DB.DBManager.CurrentDatabaseTime;
            string searchTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dbCurrentDatabaseTime.AddSeconds(-timeout));
            string whereSql = string.Format("update_time>='{0}'", searchTime);

            return new Entities<LiveClassroom>(context, whereSql).PagingCache;
        }
    }
}

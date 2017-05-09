using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade
{
    public interface ILiveClassRoomManager
    {

        /// <summary>
        /// 获取正在上课直播教室
        /// </summary>
        /// <param name="queryPagingParam">分页参数</param>
        /// <returns></returns>
        PagingQueryResultDto<LiveClassRoomDto> GetList(ContentQueryPageParameter queryPagingParam);

        /// <summary>
        /// 注册直播接口
        /// </summary>
        /// <param name="entity"></param>
        void RegisterLive(LiveClassRoomDto entity);

        /// <summary>
        /// 直播状态实时汇报接口
        /// </summary>
        /// <param name="classroomimie">教室串号</param>
        void SendLiveStreamStatus(string classroomimie);


        LiveClassRoomDto Get(int id);
    }
}

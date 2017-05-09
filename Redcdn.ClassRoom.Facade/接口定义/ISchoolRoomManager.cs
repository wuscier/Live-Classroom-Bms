using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {

    //教室管理接口
    public interface ISchoolRoomManager
    {
        /// <summary>
        /// 创建教室
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        SchoolRoomDto Create(SchoolRoomDto entityDto);

        /// <summary>
        /// 更新教室
        /// </summary>
        /// <param name="entityDto"></param>
        void Update(SchoolRoomDto entityDto);

        /// <summary>
        /// 根据串号获取教室信息
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        SchoolRoomDto GetByImei(string imei);

        /// <summary>
        /// pc端授权验证接口
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        SchoolRoomDto GetByImeiForPcClient(string imei);

        /// <summary>
        /// 验证token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        SchoolRoomDto GetByToken(string token);

        /// <summary>
        /// 帐号查询
        /// </summary>
        /// <param name="nubeNumber"></param>
        /// <returns></returns>
        SchoolRoomDto GetByNubeNumber(string nubeNumber);

        /// <summary>
        /// 删除教室
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// 根据教室Id获取教室信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SchoolRoomDto GetSchoolRoomById(int id);

        /// <summary>
        /// 获取所有教室信息
        /// </summary>
        /// <param name="queryPagingParam"></param>
        /// <returns></returns>
        PagingQueryResultDto<SchoolRoomDto> GetAll(ContentQueryPageParameter queryPagingParam);

        /// <summary>
        /// pc 端获取所有教室
        /// </summary>
        /// <returns></returns>
        IList<SchoolRoomDto> GetAll();
    }
}

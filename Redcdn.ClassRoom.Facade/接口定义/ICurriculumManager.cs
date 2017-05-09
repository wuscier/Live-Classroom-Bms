using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface ICurriculumManager {

        /// <summary>
        /// 创建课程
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        CurriculumDto Create(CurriculumDto entityDto);

        /// <summary>
        /// 修改课程信息
        /// </summary>
        /// <param name="entityDto"></param>
         void CurriculumManagerDto(CurriculumDto entityDto);

        /// <summary>
        /// 删除一节课程
        /// </summary>
        /// <param name="id">课程主键id</param>
         void Delete(int id);

        /// <summary>
        /// 查看一节课程详细信息
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns></returns>
         CurriculumDto Get(int id);

        /// <summary>
        /// 根据课表id,串号获取课表信息
        /// </summary>
        /// <param name="curriculumid"></param>
        /// <param name="classroomimie"></param>
        /// <returns></returns>
         CurriculumDto Get(int curriculumid, string classroomimie);

        /// <summary>
        /// pc断更具教室id获取课表
        /// </summary>
        /// <param name="mainClassRoomId"></param>
        /// <returns></returns>
        IList<CurriculumDto> GetList(int mainClassRoomId);

        /// <summary>
        /// 获取所有课程
        /// </summary>
        /// <param name="curriculumQueryParam">课表查询条件参数</param>
        /// <param name="queryPagingParam">分页参数</param>
        /// <returns></returns>
        PagingQueryResultDto<CurriculumDto> GetList(ComplexQueryParameter queryParam, ContentQueryPageParameter queryPagingParam);

        /// <summary>
        /// 更新课程
        /// </summary>
        /// <param name="entityDto"></param>
        void Update(CurriculumDto entityDto);

        /// <summary>
        /// 更新课堂号接口
        /// </summary>
        void UpdateCurriculumMeetingNo(int curriculumid, string classroomimie, int meetingid,long mettingstarttime);
    }
}

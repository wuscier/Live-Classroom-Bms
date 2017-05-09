using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface IRecordFileManager
    {

        /// <summary>
        /// 创建录制记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        RecordFileDto Create(RecordFileDto entityDto);

        /// <summary>
        /// 更新录制记录
        /// </summary>
        /// <param name="entityDto"></param>
        void UpdateRecordFileDtoManagerDto(RecordFileDto entityDto);

        /// <summary>
        /// 删除录制记录
        /// </summary>
        /// <param name="id">课程主键id</param>
         void Delete(int id);

        /// <summary>
        /// 根据ID查看录制记录
        /// </summary>
        /// <param name="id">课程id</param>
        /// <returns></returns>
         RecordFileDto Get(int id);
       

        /// <summary>
        /// 获取所有课程
        /// </summary>
        /// <param name="curriculumQueryParam">课表查询条件参数</param>
        /// <param name="queryPagingParam">分页参数</param>
        /// <returns></returns>
         PagingQueryResultDto<RecordFileDto> GetList(ComplexQueryParameter queryParam, ContentQueryPageParameter queryPagingParam);

        /// <summary>
        /// 为pc终端提供实时启动录制接口
        /// </summary>
        void StartRecord(TerminalRecordParamDto dto);

        /// <summary>
        /// 为pc终端提供实时停止录制接口
        /// </summary>
        void StopRecord(TerminalRecordParamDto dto);

    }
}

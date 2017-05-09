using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Service.InterfaceServerControl
{
    public class ContentDeleteTaskInfo
    {
        /// <summary>
        /// 操作人员编号
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 内容删除类型
        /// </summary>
        public ContentDeleteType DeleteType { get; set; }

        /// <summary>
        /// 删除任务列表
        /// </summary>
        public DeleteContentInfo[] ContentInfos { get; set; }

        public override string ToString()
        {
            return string.Format("操作员[id:{0}({1})]执行内容删除任务，删除任务类型：{2}", OperatorID, OperatorName,
                Enum.GetName(typeof (ContentDeleteType), DeleteType));
        }
    }

    /// <summary>
    /// 删除内容信息
    /// </summary>
    public class DeleteContentInfo
    {
        /// <summary>
        /// 加速域名对应的CMSID
        /// </summary>
        public string Cmsid { get; set; }

        /// <summary>
        /// 加速服务类型
        /// </summary>
        public AccelerationServiceType AccelerationServiceType { get; set; }

        /// <summary>
        /// 内容列表
        /// </summary>
        public List<string> ContentIds { get; set; }

        public override string ToString()
        {
            return string.Format("CMSID:{0}，加速服务类型:{1}，内容列表[{2}]", Cmsid,
                Enum.GetName(typeof (AccelerationServiceType), AccelerationServiceType),
                ContentIds != null ? string.Join("|", ContentIds.ToArray()) : "无内容");
        }
    }

    /// <summary>
    /// 内容删除类型
    /// </summary>
    public enum ContentDeleteType
    {
        /// <summary>
        /// 删除指定内容
        /// </summary>
        DeleteByContentIDs = 1,

        /// <summary>
        /// 删除执行域名下所有内容
        /// </summary>
        DeleteByCmsids = 2
    }

    /// <summary>
    /// 加速类型枚举值
    /// </summary>
    [Flags]
    public enum AccelerationServiceType
    {
        Non = 0,
        点播视频加速 = 1 << 0,
        直播频道加速 = 1 << 1,
        文件下载加速 = 1 << 2
    }
}

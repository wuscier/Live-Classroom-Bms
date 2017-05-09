using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module
{
    /// <summary>
    /// 公网直播频道
    /// </summary>
    [DBTable("remote_channel")]
    public class RemoteChannel : Entity
    {
        [DBField("id", IsKey = true)]
        public override int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DBField("name")]
        public string Name { get; set; }
        /// <summary>
        /// 推流地址
        /// </summary>
        [DBField("pushstreamurl")]
        public string PushStreamUrl { get; set; }
        /// <summary>
        /// 播放地址
        /// </summary>
        [DBField("playstreamurl")]
        public string PlayStreamUrl { get; set; }
        /// <summary>
        /// 直播页面地址
        /// </summary>
        [DBField("playurl")]
        public string PlayUrl { get; set; }
    }
}

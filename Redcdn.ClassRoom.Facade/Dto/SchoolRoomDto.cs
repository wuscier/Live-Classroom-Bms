using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {

    /// <summary>
    /// 教室管理Dto 与Ui交互使用
    /// </summary>
    public class SchoolRoomDto {

        public int Id { get; set; }

        //教室号
        public string SchoolRoomNum { get; set; }

        //教室串号
        public string SchoolRoomIMEI { get; set; }

        //教室名称
        public string SchoolRoomName { get; set; }

        //教室地址
        public string SchoolRoomAddress { get; set; }

        //物理频道OuterId
        public string PhysicalChannelOuterId { get; set; }

        //播放地址
        public string PlayStreamUrl { get; set; }

        //推流地址
        public string PushStreamUrl { get; set; }

        // Token
        public string Token { get; set; }

        //备注
        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

    }
}

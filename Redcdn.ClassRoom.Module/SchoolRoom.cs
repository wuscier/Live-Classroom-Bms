using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {

    /// <summary>
    /// 教室
    /// </summary>
    [DBTable("school_room")]
    public class SchoolRoom:Entity,IDeleteflag {

        //教室号
        [DBField("school_roomnum")]
        public string SchoolRoomNum { get; set; }

        //教室名称
        [DBField("school_roomname")]
        public string SchoolRoomName { get; set; }

        // 教室详细地址
         [DBField("schoolroom_address")]
        public string SchoolRoomAddress { get; set; }

        //教室串号
        [DBField("school_room_imei")]
        public string SchoolRoomIMEI { get; set; }

        //物理频道id
        [DBField("physical_channel_outerId")]
        public string PhysicalChannelOuterId { get; set; }

        //播放地址
        [DBField("playstremurl")]
        public string PlayStreamUrl { get; set; }

        //推流地址
        [DBField("pushstreamurl")]
        public string PushStreamUrl { get; set; }

        //备注
        [DBField("remark")]
        public string Remark { get; set; }

        [DBField("token")]
        public string Token { get; set; }

        [DBField("delete_flag")]
        public bool Deleteflag { get; set; }
    }
}

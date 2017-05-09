using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS.Models
{
    public class SchoolRoomJsonModel : SchoolRoomDto
    {
        public string RemotePlayStreamUrl { get; set; }
        public string RemotePushStreamUrl { get; set; }
    }
}
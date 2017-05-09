using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class BaseParam {

        [JsonProperty("classroomid")]
        public int ClassRoomId { get; set; }

    }
}
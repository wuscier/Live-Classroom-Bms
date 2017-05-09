using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    public class LiveChannelDeleteReplyParam : RequestParam
    {
        [JsonProperty("contentIds")]
        public string[] ChannelNames;
    }
}

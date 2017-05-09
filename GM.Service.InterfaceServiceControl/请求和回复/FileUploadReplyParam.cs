using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    public class FileUploadReplyParam : RequestParam
    {
        [JsonProperty("contentids")]
        public string[] ContentIds { get; set; }
    }
}

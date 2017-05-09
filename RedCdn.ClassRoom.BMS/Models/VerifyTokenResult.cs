using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class VerifyTokenResult:MettingExternalApiResultBase {

        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
    }
}
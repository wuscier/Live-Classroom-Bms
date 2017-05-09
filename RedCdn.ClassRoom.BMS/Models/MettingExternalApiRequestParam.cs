using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class MettingExternalApiRequestParam  {

        [JsonProperty("imei")]
        public string Imei { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("nubeNumbers")]
        public string[] NubeNumbers { get; set; }

        public MettingExternalApiRequestParam()
        {
          
        }
    }

    public class NubeNumber
    {
        public string NubeNum { get; set; }
    }
}
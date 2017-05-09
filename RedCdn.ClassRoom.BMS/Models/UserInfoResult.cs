using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class UserInfoResult {

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

         [JsonProperty("createTime")]
        public string CreateTime { get; set; }

          [JsonProperty("imei")]
        public string Imei { get; set; }

        [JsonProperty("memberId")]
        public string MemberId { get; set; }

        [JsonProperty("mettingType")]
        public string MettingType { get; set; }

        [JsonProperty("nube")]
        public string Nube { get; set; }

        [JsonProperty("nubeNumber")]
        public string NubeNumber { get; set; }

        [JsonProperty("preBindTime")]
        public string PreBindTime { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("headUrl")]
        public string HeadUrl { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }

        [JsonProperty("useStartTime")]
        public string UseStartTime { get; set; }

        [JsonProperty("useEndTime")]
        public string UseEndTime { get; set; }
    }
}
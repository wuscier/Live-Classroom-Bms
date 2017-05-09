using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class EntMemberLoginResult : MettingExternalApiResultBase {

        [JsonProperty("userInfo")]
        public UserInfoResult UserInfo { get; set; }

        public EntMemberLoginResult()
        {
            UserInfo = new UserInfoResult();
        }

    }

    /// <summary>
    /// 企业的详细信息
    /// </summary>
    public class EntInfo
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("contactTel")]
        public string ContactTel { get; set; }

        [JsonProperty("customizedParams")]
        public string CustomizedParams { get; set; }

        [JsonProperty("entContact")]
        public string EntContact { get; set; }

        [JsonProperty("entId")]
        public int EntId { get; set; }

        [JsonProperty("entName")]
        public string EntName { get; set; }

        [JsonProperty("entPerson")]
        public string EntPerson { get; set; }

        [JsonProperty("entTel")]
        public string EntTel { get; set; }

        /// <summary>
        /// //企业类型 0:专业会议企业 1:第三方企业 2:极会议企业 默认是专业会议企业
        /// </summary>
        [JsonProperty("entType")]
        public int EntType { get; set; }

        [JsonProperty("mettingAmount")]
        public string MettingAmount { get; set; }

        [JsonProperty("mettingSquares")]
        public string MettingSquares { get; set; }

        [JsonProperty("nubeAmount")]
        public string NubeAmount { get; set; }

        [JsonProperty("orgNo")]
        public int OrgNo { get; set; }

        [JsonProperty("videophoneAmount")]
        public string VideophoneAmount { get; set; }
    }

    /// <summary>
    /// 企业订购的服务信息
    /// </summary>
    public class ServiceInfo
    {
        public string Comment { get; set; }
    }
}
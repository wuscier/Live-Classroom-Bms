using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    public class LiveChannelReply : ReplyInfoBase
    {
        [JsonProperty("playurls")]
        public string[] PalyUrls;

        [JsonProperty("streampushurl")]
        public string StreamPushUrl;

        [JsonProperty("contentId")]
        public string ChannelName;

        [JsonIgnore]
        public string ProviderID;
    }

    /// <summary>
    /// 回复信息基类
    /// </summary>
    public class ReplyInfoBase
    {
        /// <summary>
        /// 请求结果码
        /// </summary>
        [JsonProperty("resultcode")]
        public virtual int ResultCode { get; set; }

        /// <summary>
        /// 请求结果描述
        /// </summary>
        [JsonProperty("desc")]
        public virtual string Desc { get; set; }
    }
}

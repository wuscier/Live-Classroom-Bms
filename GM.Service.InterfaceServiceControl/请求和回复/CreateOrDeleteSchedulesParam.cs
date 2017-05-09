using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GM.Service.InterfaceServerControl
{
    class CreateOrDeleteSchedulesParam
    {
        [JsonProperty("domainname")]
        public String DomainName { get; set; }
        [JsonProperty("channelid")]
        public String ChannelID { get; set; }
        [JsonProperty("schedules")]
        public Schedule[] schedules { get; set; }
    }
    class Schedule
    {
        [JsonProperty("scheduleid")]
        public String ScheduleId { get; set; }
        [JsonProperty("starttime")]
        public String startTime { get; set; }
        [JsonProperty("endtime")]
        public String endTime { get; set; }
        [JsonProperty("playurls")]
        public String[] playUrls { get; set; }
    }
}

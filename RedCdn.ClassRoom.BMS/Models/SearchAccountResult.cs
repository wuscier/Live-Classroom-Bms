using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS.Models {
    public class SearchAccountResult : MettingExternalApiResultBase {

         [JsonProperty("users")]
        public List<User> Users { get; set; }

        public SearchAccountResult()
        {
            Users = new List<User>();
        }
    }

    public class User
    {
        [JsonProperty("appType")]
        public string AppType { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("nubeNumber")]
        public string NubeNumber { get; set; }

    }
}
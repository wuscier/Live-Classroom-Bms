using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {
    [DBTable("MEDIA_SERVICE_CONFIG")]
    public class MediaService : Entity, IDeleteflag
    {
        [DBField("PLAYBACK_URL_PREFIX")]
        public string PlaybackURLprefix { get; set; }

        [DBField("PROVIDER_ID")]
        public string ProviderID { get; set; }

        [DBField("DELETE_FLAG")]
        public bool Deleteflag {
            get;
            set;
        }
    }
}

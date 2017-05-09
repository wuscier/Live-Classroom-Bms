using System.Web.Mvc;

namespace RedCdn.ClassRoom.BMS.Models
{
    [Bind(Prefix = "channel")]
    public class RemoteChannelModel
    {
        public int Id { get; set; }
        public string PushStreamUrl { get; set; }
        public string PlayStreamUrl { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace RedCdn.ClassRoom.BMS {
    [Serializable]
    [XmlRoot("ReportRecordDto")]
    public class RecordReportResult {
        public string Cpid { get; set; }

        public int EntityId { get; set; }

        public string ProviderId { get; set; }

        public string ContentId { get; set; }

        public string NewContextId { get; set; }

        public int TransferRate { get; set; }

        public int Duration { get; set; }

        public ulong FileSize { get; set; }

        public string FilePath { get; set; }

        public string Subfiles { get; set; }

        public string AttachedFiles { get; set; }

        public int Contenttype { get; set; }

        public string StartRecordRealTime { get; set; }

        public string StopRecordRealTime { get; set; }

        public string Context { get; set; }

    }
}
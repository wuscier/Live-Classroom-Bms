using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Service.InterfaceServerControl
{
    public class RecordManager
    {
        private static readonly RecordManager _instance = new RecordManager();

        public static RecordManager Instance{get { return _instance; }}

        private RecordManager(){}

        public void StartRecord(StartRecordParam param)
        {
            GwPublishPointControl.StartRecord(param);
        }

        public void StopRecord(StopRecordParam param)
        {
            GwPublishPointControl.StopRecord(param);
        }
    }
}

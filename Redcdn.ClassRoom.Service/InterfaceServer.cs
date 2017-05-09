using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Service.InterfaceServerControl;
using GM.Utilities;

namespace Redcdn.ClassRoom {
    public class InterfaceServer {

        private readonly static InterfaceServer _instace = new InterfaceServer();

        public static InterfaceServer Instance {
            get { return _instace; }
        }

        private InterfaceServer(){}

        public void Start()
        {
            try
            {
                GwPublishPointControl.Start();
                Logger.WriteInfo("GM.Service.InterfaceServerControl", "启动InterfaceServerControl成功。");
            }
            catch (Exception e)
            {
                Logger.WriteError("GM.Service.InterfaceServerControl", "启动InterfaceServerControl失败。", e);
            }
        }

        public void Stop()
        {
            GwPublishPointControl.Stop();
        }
    }
}

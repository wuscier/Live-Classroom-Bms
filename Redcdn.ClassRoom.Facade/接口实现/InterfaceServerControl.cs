using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public class InterfaceServerControl:IInterfaceServerControl {
        public void Start()
        {
            InterfaceServer.Instance.Start();
        }

        public void Stop()
        {
            InterfaceServer.Instance.Stop();
        }
    }
}

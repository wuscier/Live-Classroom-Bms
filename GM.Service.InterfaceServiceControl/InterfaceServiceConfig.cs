using System;
using System.Collections.Generic;
using System.Text;
using GM.Utilities;

namespace GM.Service.InterfaceServerControl
{
    public class InterfaceServiceConfig
    {
        private static InterfaceServiceConfig _instance;

        public static InterfaceServiceConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = SettingManager.Instance.GetSetting<InterfaceServiceConfig>();
                }
                return _instance;
            }
        }

        [XmlSetting("RRIPPort", @"http://127.0.0.1:80")]
        public string RRIPPort;
    }
}

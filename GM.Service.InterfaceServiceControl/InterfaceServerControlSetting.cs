using System;
using System.Collections.Generic;
using System.Text;
using GM.Utilities;

namespace GM.Services
{
    class InterfaceServerControlSetting
    {
        public InterfaceServerControlSetting()
        { }

        private static InterfaceServerControlSetting _instance = SettingManager.Instance.GetSetting<InterfaceServerControlSetting>();
        public static InterfaceServerControlSetting Instance { get { return _instance; } }


        /// <summary>
        /// 接口服务器工作模式
        /// true 表示工作在中心平台此时接口服务器提供GetVodInfo、DeleteVodInfo、SetKey三种接口服务，并在DeleteVodInfo接口中调用下级接口服务器的删除操作
        /// false 表示工作在边缘平台测试只提供DeleteVodInfo接口服务
        /// </summary>
        [XmlSetting("CenterModel", DefaultValue = "true")]
        public bool CenterModel = true;

        /// <summary>
        /// 下级平台接口服务器地址
        /// </summary>
        [XmlSetting("EdgeInterfaceServerAddress", DefaultValue = "")]
        public string EdgeInterfaceServerAddress = "";
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GM.Business.Module;
using GM.Service.InterfaceServerControl;
using GM.Utilities;
using System.IO;
namespace GM.Services
{
    public class InterfaceServerControl : EquipmentContorlBase,IInterfaceServerControl
    {
        static InterfaceServerControl instance;
        protected override Type ResourceConfigType
        {
            get { return typeof(ResourceConfig); }
        }

        protected override Type ServiceConfigType
        {
            get { return typeof(InterfaceServerConfig); }
        }
        internal static InterfaceServerControl Instance
        {
            get { return instance; }
        }
 
        public override void Init()
        {
            instance = this;
            base.Init();
        }

        public override void Start()
        {
            Logger.WriteInfoFmt(Log.AppShutdown, "接口服务器已启动，工作在{0}，下级接口服务器地址设置为{1}", InterfaceServerControlSetting.Instance.CenterModel?"中心":"边缘", InterfaceServerControlSetting.Instance.EdgeInterfaceServerAddress);

            if (InterfaceServerControlSetting.Instance.CenterModel)
                InterfaceServerData.Instance.Start();

            GwPublishPointControl.Start();
            base.Start();
        }

        public override void Stop()
        {
            if (InterfaceServerControlSetting.Instance.CenterModel)
                InterfaceServerData.Instance.Stop();

            base.Stop();

            GwPublishPointControl.Start();
        }
    }
}

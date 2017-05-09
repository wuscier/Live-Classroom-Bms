using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using GM.Utilities;
using GM.Utilities.Web;

namespace GM.Services
{
    class MoudleLoader : IHttpMethodAssemblyLoader
    {
        #region IHttpMethodAssemblyLoader 成员

        public void Load()
        {
            if (Environment.UserInteractive)
                Logger.ShowSetting();
            
            
            EquipmentContorlBase.Run();

            //if (ConfigurationSettings.AppSettings["DisableAutoRun"] != "true" && RecordDataProvideServiceControl.Instance.Status == ServiceStatus.Stoped)
            //    RecordDataProvideServiceControl.Instance.Start();

           Logger.WriteInfo(Log.Debug, "应用程序已加载");
        }

        public void Unload()
        {
            EquipmentContorlBase.TerminateRemoteServices();
        }

        #endregion
    }
}

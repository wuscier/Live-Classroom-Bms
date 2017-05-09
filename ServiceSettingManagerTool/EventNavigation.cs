using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Business.Module;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool {

    public delegate void UpdateConfigHandler<T>(T config);
    public class EventNavigation<T> where T:ServiceConfig,new() {

        public event UpdateConfigHandler<T> UpdateConfigEvent;

        public void RaiseEvent(T config) {
            OnRaiseEvent(config);
        }

        void OnRaiseEvent(T updateConfig) {
            if (UpdateConfigEvent != null)
                UpdateConfigEvent(updateConfig);
        }
    
    }

    //public class NavigationWrapper
    //{
    //    private static NavigationWrapper _instance = new NavigationWrapper();

    //    public static NavigationWrapper Instance {
    //        get { return _instance; }
    //    }



    //    public T Get<T>() where T : ServiceConfig,new()
    //    {
    //       

            
    //    }
    //}
}

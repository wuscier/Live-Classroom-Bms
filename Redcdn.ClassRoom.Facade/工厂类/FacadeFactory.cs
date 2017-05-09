using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using GM.Utilities;

namespace Redcdn.ClassRoom.Facade {
    /// <summary>
    /// 外观工厂类定义。注，目前一个接口只允许有一个实现类，否则返回实例类型不确定。
    /// </summary>
    public class FacadeFactory {
        FacadeContextObject _contextObject = new FacadeContextObject();
        static FacadeFactory _instance = new FacadeFactory();

        public static FacadeFactory Instance { get { return _instance; } }

        /// <summary>
        /// 返回一个指定接口的实现类型
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>实现类</returns>
        public T Get<T>() {
            _contextObject.Init();
            return (T)_contextObject.Get(typeof(T));
        }
    }

    /// <summary>
    /// 反射程序集，加载Facade实例与接口对应关系
    /// </summary>
    class FacadeContextObject {
        bool _blInit;
        Dictionary<Type, object> _contextDic = new Dictionary<Type, object>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Init() {
            if (_blInit)
                return;

            BuildFacdeContext();
      
        }

        public object Get(Type type) {
            object facade = null;

            if (!_contextDic.TryGetValue(type, out facade))
                throw new Exception(string.Format("未能获取 “{0}” 接口的实现类实例", type.Name));

            return facade;
        }

        void BuildFacdeContext() {
            try {
                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();

                foreach (var type in types) {
                    var interfaceList = type.GetInterfaces();
                    if (interfaceList.Length == 1)
                        _contextDic[interfaceList[0]] = Activator.CreateInstance(type);
                }

                _blInit = true;
            } catch (Exception ex) {
                var msg = "FacadeFactory工厂上下文构建失败";
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, msg);
                throw new Exception(msg);
            }
        }
    }
}

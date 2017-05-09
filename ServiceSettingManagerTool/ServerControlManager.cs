using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Business.Module;
using GM.Orm;
using GM.Utilities;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool {
    public class ServerControlManager {

        static ServerControlManager _instance = new ServerControlManager();

        public static ServerControlManager Instance { get { return _instance; } }


        private Dictionary<int, string> _serverProtocolDic = new Dictionary<int, string>(); //服务器协议url，格式为：tcp://{0}:{1}/service

        /// <summary>
        /// 获取服务器Remoting管理地址
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        private string GetRemotingUrl(string ip, int port, int serverType) {
            if (!_serverProtocolDic.ContainsKey(serverType)) {
                _serverProtocolDic.Add(serverType, "TCP://{0}:{1}/service");
                var eqs = new Entities<Equipment>("type_id={0}", serverType).Cache;
                if (eqs.Count == 0) {
                    Logger.WriteWarningFmt(LogCatalogs.OperationHits, "未找到TypeID='{0}'的服务器记录，使用默认协议，返回服务器Remoting地址为：TCP://{1}:{2}/service", serverType, ip, port);
                    return string.Format("TCP://{0}:{1}/service", ip, port);
                }

                _serverProtocolDic[serverType] = eqs[0].Protocal + "://{0}:{1}/" + eqs[0].RemotingURI;
            }

            return string.Format(_serverProtocolDic[serverType], ip, port);
        }



        public IEquipmentControl GetIEuipmentControl<T>(string ip, int port) where T : Equipment,new()
        {
            string remotingUrl = GetRemotingUrl(ip, port, new T().TypeID);
            IEquipmentControl iec;
            try {
                iec = (IEquipmentControl)Activator.GetObject(typeof(IEquipmentControl), remotingUrl);
            } catch (Exception ex) {
                throw ex;
            }
            return iec;
        }
    }
}

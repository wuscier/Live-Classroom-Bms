using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GM.Business.Module;
using GM.Orm;
using GM.Utilities;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    public partial class FrServiceConfig : UserControl
    {



        private FrIESServiceConfig _iesConfig;
        private FrLssServiceConfig _lssConfig;

        private IESServer _iesServer;
        private LiveStreamSpliteServer _lssServer;

  
       
        public FrServiceConfig() {

            InitializeComponent();

            GetEquipment();
            LoadServiceConfigInfo();
        }

      
        void GetEquipment()
        {
            _iesServer = EquipmentWrapper.Instance.GetEquipment<IESServer>();
            _lssServer = EquipmentWrapper.Instance.GetEquipment<LiveStreamSpliteServer>();
        }

        void LoadServiceConfigInfo()
        {
            if (_rbIes.Checked) {
                if (_iesServer != null) {
                    LoadIesControl();
                }
            } else {
                if (_iesServer != null) {
                    LoadLssControl();
                }
            }
        }

        private void _rbIes_CheckedChanged(object sender, EventArgs e) {
            LoadIesControl();
        }

        private void _rbLrs_CheckedChanged(object sender, EventArgs e) {
            LoadLssControl();
        }

        void LoadIesControl()
        {
            _iesConfig = new FrIESServiceConfig(_iesServer.ServiceConfig as IESServerConfig, _iesServer);
            _panelMain.Controls.Clear();
            _panelMain.Controls.Add(_iesConfig);
        }

        void LoadLssControl() {
            _lssConfig = new FrLssServiceConfig(_lssServer.ServiceConfig as LiveStreamSpliteServerConfig,_lssServer);
            _panelMain.Controls.Clear();
            _panelMain.Controls.Add(_lssConfig);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            string deviceid = GMDeviceInfoKey.GetOrCreateDeviceInfoKey().DeviceID;

            try
            {
                var iescontrol = ServerControlManager.Instance.GetIEuipmentControl<IESServer>(_iesServer.IPAddress, _iesServer.Port);
                iescontrol.SetSvrConfig(_iesServer.ServiceConfig);
                _iesServer.Serviceid = _iesServer.Serviceid;
                _iesServer.Deviceid = deviceid;
                (_iesServer as IEntity).Update();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新ies服务器工作参数失败：{0}",ex.Message);
                MessageBox.Show("更新流服务器工作参数失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var lsscontrol =ServerControlManager.Instance.GetIEuipmentControl<LiveStreamSpliteServer>(_lssServer.IPAddress, _lssServer.Port);
                lsscontrol.SetSvrConfig(_lssServer.ServiceConfig);
                

                _lssServer.Serviceid = _lssServer.Serviceid;
                _lssServer.Deviceid = deviceid;
                (_lssServer as IEntity).Update();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新中继服务器工作参数失败：{0}", ex.Message);
                MessageBox.Show("更新中继服务器工作参数失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("保存成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    class EquipmentWrapper
    {
        static EquipmentWrapper _instance = new EquipmentWrapper();
        public static EquipmentWrapper Instance { get { return _instance; } }

        public T GetEquipment<T>()where T : Equipment,new() {
            try {
                var ens = new Entities<T>("type_id={0}", new T().TypeID).Cache;
                return ens.Count > 0 ? ens[0] : null;
            } catch (Exception ex) {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "从数据库获取{0}类型服务器失败", typeof(T).Name);
                throw;
            }
        }
    }

}

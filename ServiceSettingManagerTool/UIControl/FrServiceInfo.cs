using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GM.Business.Module;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    public partial class FrServiceInfo : UserControl
    {

        private Equipment _eqInfo;
        public FrServiceInfo() {
            InitializeComponent();
        }

        public FrServiceInfo(Equipment eq) : this()
        {
            _eqInfo = eq;
            BindParamsToEquipment(_eqInfo);
        }

        void BindParamsToEquipment(Equipment eq)
        {
            txtSerName.Text = eq.Name;
            txtURI.Text = eq.RemotingURI;
            txtInstallPath.Text = eq.Serviceid.Replace(eq.Deviceid + "+", "").Replace("iis://", ""); ;
            txtIp.Text = eq.IPAddress;
            txtAddressIp.Text = eq.OuterIpAddress;
            cbProtocol.SelectedItem = eq.Protocal;
            txtRemortPort.Text = eq.Port.ToString();

             ServiceConfig config = eq.ServiceConfig;
            cbAutoStart.Checked = config.IsAutoRun;
            cbAlarm.Checked = config.IsRunInitiativeAlarm;
        }

        private void txtSerName_TextChanged(object sender, EventArgs e)
        {
            _eqInfo.Name = txtSerName.Text;
        }

        private void txtInstallPath_TextChanged(object sender, EventArgs e) {
            _eqInfo.Serviceid = string.Format("{0}+{1}", _eqInfo.Deviceid, txtInstallPath.Text);
        }

        private void txtIp_TextChanged(object sender, EventArgs e) {
            _eqInfo.IPAddress = txtIp.Text;
        }

        private void txtAddressIp_TextChanged(object sender, EventArgs e) {
            _eqInfo.OuterIpAddress = txtAddressIp.Text;
        }

        private void txtRemortPort_TextChanged(object sender, EventArgs e) {
            _eqInfo.Port = Convert.ToInt32(txtRemortPort.Text);
        }
    }
}

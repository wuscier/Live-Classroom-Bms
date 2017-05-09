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
    public partial class FrIsaServiceConfig : UserControl
    {

        private StorageServer _isa;
        public FrIsaServiceConfig() {
            InitializeComponent();
            GetIsa();
        }

        void GetIsa()
        {
            txtPath.TextChanged -= txtPath_TextChanged;

            var isa= EquipmentWrapper.Instance.GetEquipment<StorageServer>();
            if (isa != null)
            {
                this._isa = isa;
                txtPath.Text = (isa.ServiceConfig as StorageServerConfig).StorageBasePath;
            }
            txtPath.TextChanged += txtPath_TextChanged;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            _lnkSave.Enabled = true;
        }

        private void _lnkSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try
            {
                if (_isa != null)
                {
                    (_isa.ServiceConfig as StorageServerConfig).StorageBasePath=txtPath.Text;

                    var deviceId = GMServiceProfile.GetOrCreateGMServiceProfile().DeviceID;
                    _isa.Deviceid = deviceId;
                  //  _isa.Serviceid = string.Format("{0}+{1}", deviceId, txtPath.Text);
                    (_isa as IEntity).Update();

                    _lnkSave.Enabled = false;

                    MessageBox.Show("保存成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "保存录制文件存储目录失败", ex);
                MessageBox.Show("保存失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

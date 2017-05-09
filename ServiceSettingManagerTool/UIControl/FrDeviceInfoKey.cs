using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GM.Business.Module;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    public partial class FrDeviceInfoKey : UserControl {

        const string GMDeviceidFile = "gmDeviceid.key";
        public FrDeviceInfoKey() {
            InitializeComponent();
            if (IsGmDeviceidKeyFile())
                EnableControl(true);
            else
                GetDeviceKey();
        }

        private void _lnkCreateDeviceKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetDeviceKey();
            EnableControl(false);
        }

        void GetDeviceKey()
        {
            var device = GMDeviceInfoKey.GetOrCreateDeviceInfoKey();
            _txtDeviceKey.Text = device.DeviceID;
        }

        public  bool IsGmDeviceidKeyFile() {
            string subDir = Is64Runtime() ? "SysWOW64" : "system32";
            string dir = Path.Combine(Environment.GetEnvironmentVariable("windir"), subDir);
            string path = Path.Combine(dir, GMDeviceidFile);

            if (File.Exists(path)) {
                return false;
            }
            return true;
        }

        private  bool Is64Runtime() {
            if (IntPtr.Size == 4)
                return false;

            return true;
        }

        void EnableControl(bool enable) {
            _lnkCreateDeviceKey.Enabled = enable;
        }
    }
}

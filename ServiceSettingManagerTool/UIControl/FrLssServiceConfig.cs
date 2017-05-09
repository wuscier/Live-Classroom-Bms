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
    public partial class FrLssServiceConfig : UserControl {

        private FrServiceInfo _serviceInfo;
        private LiveStreamSpliteServerConfig _config;

        private LiveStreamSpliteServer lssServer;
        public FrLssServiceConfig() {
            InitializeComponent();
        }

        public FrLssServiceConfig(LiveStreamSpliteServerConfig  config,Equipment eq) : this()
        {
            lssServer = eq as LiveStreamSpliteServer;
            _config = config;
            _config.BindIp = eq.IPAddress;

            _serviceInfo=new FrServiceInfo(eq);

            _panelMain.Controls.Add(_serviceInfo);

            _txtListenPort.Text = config.BindPort.ToString();
            _txtReportUrl.Text = config.DataProviderServerList[0];
            _cbDBSync.Checked = config.EnableSync;
        }

        private void _txtListenPort_TextChanged(object sender, EventArgs e)
        {
            _config.BindPort = Convert.ToUInt16(_txtListenPort.Text);
        }

        private void _txtReportUrl_TextChanged(object sender, EventArgs e) {
            _config.DataProviderServerList = new List<string>() { _txtReportUrl.Text };
        }

        private void _cbDBSync_CheckedChanged(object sender, EventArgs e)
        {
            _config.EnableSync = _cbDBSync.Checked;
        }
    }
}

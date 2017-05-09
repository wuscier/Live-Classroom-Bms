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
    public partial class FrIESServiceConfig : UserControl
    {
        private FrServiceInfo _serviceInfo;
        private IESServerConfig _config;

        private IESServer iesServer;

        public FrIESServiceConfig() {
            InitializeComponent();
         
        }

        public FrIESServiceConfig(IESServerConfig config,Equipment eq):this()
        {
            iesServer = eq as IESServer;

            _config = config;
            _serviceInfo = new FrServiceInfo(eq);
            BindConfigToUI();
        }

        public void BindConfigToUI()
        {
            if (_config == null)
                return;


            _panelMain.Controls.Add(_serviceInfo);
            

            _txtRTSP.Text = _config.MediaListenPort.ToString();
            _txtRFCRTSP.Text = _config.RFCRtspPort.ToString();
            _txtMediaPort.Text = _config.MediaHttpPort.ToString();
            _txtUploadBand.Text = _config.UploadBand.ToString();
            _txtDownloadBand.Text = _config.DownloadBand.ToString();
        }

        private void _txtControl_TextChanged(object sender, EventArgs e)
        {
            _config.MediaListenPort = Convert.ToUInt16(_txtRTSP.Text);
        }

        private void _txtRFCRTSP_TextChanged(object sender, EventArgs e) {
            _config.RFCRtspPort = Convert.ToUInt16(_txtRFCRTSP.Text);
        }

        private void _txtUploadBand_TextChanged(object sender, EventArgs e) {
            _config.UploadBand = Convert.ToUInt64(_txtUploadBand.Text);
        }

        private void _txtMediaPort_TextChanged(object sender, EventArgs e) {
            _config.MediaHttpPort = Convert.ToUInt16(_txtMediaPort.Text);
        }

        private void _txtDownloadBand_TextChanged(object sender, EventArgs e) {
            _config.DownloadBand = Convert.ToUInt64(_txtDownloadBand.Text);
        }

        private void _lnkPublishPoint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dlg = new FrIesRecource(iesServer);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}

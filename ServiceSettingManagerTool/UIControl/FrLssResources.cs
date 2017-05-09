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
    public partial class FrLssResources : UserControl
    {
        private IEquipmentControl _control;
        private RTEquipmentStatus _rtStatus;
        private List<PhysicalChannel> _physicalChannelCache;

        private XTimer _xTimer;


        private LiveStreamSpliteServer _lssServer;
        public FrLssResources() {
            InitializeComponent();

            InitXtime();

            _lssServer = GetServer();
            _control = GetControl(_lssServer.OuterIpAddress, _lssServer.Port);

        }

        void InitXtime()
        {
            _xTimer=new XTimer(){Enable = true,Interval = 5*1000};
            _xTimer.Tick += _xTimer_Tick;

          
        }

        void _xTimer_Tick(object sender, EventArgs e) {
            _rtStatus = RtStatus();
        }

        IEquipmentControl GetControl(string ip,int port)
        {
            return ServerControlManager.Instance.GetIEuipmentControl<LiveStreamSpliteServer>(ip, port);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
                return;

            base.OnLoad(e);

            BeginInvoke(new EventHandler((sender, args) => {
                Application.DoEvents();
                RefreshView();
            }));
        }

        void RefreshView()
        {
            var ens = new Entities<LiveStreamSpliteServerResource>().Cache;
            if (ens.Count > 0)
            {
                _physicalChannelCache = new Entities<PhysicalChannel>().Cache;

                _lvLssList.BeginUpdate();
                _lvLssList.Items.Clear();
                foreach (var rs in ens)
                {
                    ListViewItem item = CreateServiceListViewItem(rs);
                    _lvLssList.Items.Add(item);
                }
                _lvLssList.EndUpdate();
            }
        }


        private ListViewItem CreateServiceListViewItem(LiveStreamSpliteServerResource resource) {
            ListViewItem item = new ListViewItem(new string[6]);

            UpdateListViewItemProperties(item, resource);

            return item;
        }

        void UpdateListViewItemProperties(ListViewItem item, LiveStreamSpliteServerResource resource)
        {
            item.Text = resource.Id == 0 ? "*" : resource.Id.ToString();
            item.SubItems[1].Text = GetChanelName(resource.ChannelID);
            item.SubItems[2].Text = resource.EnableTs.ToString();
            item.SubItems[3].Text = resource.EnableTvod.ToString();
            item.SubItems[4].Text = resource.Status == LssResourceStatus.Published?"是":"否";
            item.SubItems[5].Text = GetResourceStatus(resource.Id);
            item.Tag = resource;
        }

        string GetChanelName(int channelId)
        {
            var channel= _physicalChannelCache.Find(o => o.Id == channelId);
            if (channel != null)
                return channel.ChannelName;
            return channelId.ToString();
        }

        RTEquipmentStatus RtStatus()
        {
            try
            {
                if (_control != null)
                {
                   return (_control as ILiveStreamSpliteServerControl).GetRTStatus();
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "获取服务器资源状态信息异常：{0}", ex.Message);
                return null;
            }
        }

        string GetResourceStatus(int owerId) {
            if (_rtStatus != null)
            {
                var rt = _rtStatus.ResourceStatus.RTResourceStatusList.Find(o => o.OwnerId == owerId);
                if (rt != null)
                {
                    var rtResx = rt as LiveStreamSpliteRTResourceStatus;

                    return GetMainStatusDescription(rtResx.MainStatus);
                }
            }
            return "未知";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            BeginInvoke(new EventHandler((s, args) => {
              Application.DoEvents();
              RefreshView();
          }));
        }

        private void _lvLssList_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (_lvLssList.SelectedItems.Count > 0)
                    contextMenuStrip1.Show(_lvLssList, new Point(e.X, e.Y));
            }
        }

        private void _lvLssList_SelectedIndexChanged(object sender, EventArgs e) {
            LiveStreamSpliteServerResource rs = _lvLssList.FocusedItem == null? null : _lvLssList.FocusedItem.Tag as LiveStreamSpliteServerResource;
        }

        LiveStreamSpliteServer GetServer()
        {
            return EquipmentWrapper.Instance.GetEquipment<LiveStreamSpliteServer>();
        }

        private string GetMainStatusDescription(MainStatus mainStatus) {
            switch (mainStatus) {
                case MainStatus.Running: return "正常";
                case MainStatus.StartPending: return "正在启动。。。";
                case MainStatus.Stopped: return "已经停止";
                case MainStatus.StopPending: return "正在停止。。。";
                case MainStatus.Unknown: return "未知";
            }

            return "未知";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            if (_lvLssList.SelectedItems.Count == 0) {
                e.Cancel = true;
                return;
            }

            var bindingItem = _lvLssList.FocusedItem.Tag as LiveStreamSpliteServerResource;
            var mainStatus = _lvLssList.FocusedItem.SubItems[5].Text;
            if (mainStatus == "正常")
            {
                toolStartOrStop.Text = "停止";
            }
            else
            {
                toolStartOrStop.Text = "启动";
            }
        }

        private void toolStartOrStop_Click(object sender, EventArgs e) {

            try
            {
                var bindingItem = _lvLssList.FocusedItem.Tag as LiveStreamSpliteServerResource;

                string status = "";
                if (toolStartOrStop.Text == "停止") {
                    _control.StopResource(bindingItem.ResourceConfig);
                    status = "已经停止";
                } else {
                    _control.StartResource(bindingItem.ResourceConfig);
                    status = "正常";
                }

                ListViewItem item = FindListViewItem(bindingItem);
                item.SubItems[5].Text = status;
               // UpdateListViewItemProperties(item, bindingItem);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "操作资源状态异常：{0}", ex.Message);
                MessageBox.Show("操作资源状态异常", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ListViewItem FindListViewItem(LiveStreamSpliteServerResource resource) {
            if (resource.Id != 0 && _lvLssList.Items.Count == 0)
                RefreshView();

            foreach (ListViewItem item in _lvLssList.Items) {
                if (((LiveStreamSpliteServerResource)item.Tag).Id == resource.Id)
                    return item;
            }

            return null;
        }
    }
}

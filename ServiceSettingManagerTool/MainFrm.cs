using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool {
    public partial class MainFrm : Form
    {

        private FrDeviceInfoKey _deviceInfo;
        private FrServiceConfig _serviceConfig;
        private FrIsaServiceConfig _isaServiceConfig;
        private FrLssResources _lssResources;
        public MainFrm() {
            InitializeComponent();
            InitTabPage();
            InitMenu();
        }

        void InitMenu()
        {
            var listMenu = CatalogManager.Instance.InitMenu();
            tvMenu.Nodes.Clear();
            TreeNode tn = CreateTreeNode(listMenu);
            tvMenu.Nodes.Add(tn);
            tvMenu.ExpandAll();
        }

        void InitTabPage()
        {
            _tbMain.TabPages.Clear();
        }

        TreeNode CreateTreeNode(List<Catalog> catalogs)
        {
            TreeNode node = new TreeNode("功能菜单");
            node.Tag = new Catalog() { Id = -1, Name = "功能菜单" };

            foreach (var cl in catalogs) {
                TreeNode nodechild = new TreeNode(cl.Name);
                nodechild.Tag = cl;
                node.Nodes.Add(nodechild);
            }

            return node;
        }

        private void tvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node.Tag as Catalog;
            if (node.Id==0)
                return;

            if (node.Id == 1) {
                CreateDeviceKeyControl();
            }
            if (node.Id == 2) {
                CreateIsaConfigControl();
            }
            if (node.Id == 3) {
                CreateServiceConfigControl();
            }
            if (node.Id == 4) {
                CreateLssResourcesConfigControl();
            }
        }

        void CreateDeviceKeyControl() {
            TabContentManager.Instance.AddTabPage(_tpDeviceKey, _tbMain);
            
            if (_deviceInfo==null)
                _deviceInfo = new FrDeviceInfoKey();

            _deviceInfo.Dock = DockStyle.Fill;
            _tpDeviceKey.Controls.Add(_deviceInfo);
            _tpDeviceKey.Text = "创建DeviceKey";
        }

        void CreateIsaConfigControl()
        {
            TabContentManager.Instance.AddTabPage(_tpIsaConfig, _tbMain);
            if(_isaServiceConfig==null)
                _isaServiceConfig=new FrIsaServiceConfig();

            _isaServiceConfig.Dock = DockStyle.Fill;
            _tpIsaConfig.Controls.Add(_isaServiceConfig);
            _isaServiceConfig.Text = "设置录制存储目录";

        }

        void CreateServiceConfigControl()
        {
            TabContentManager.Instance.AddTabPage(_tpServiceConfig, _tbMain);

            if (_serviceConfig==null)
                  _serviceConfig=new FrServiceConfig();
            _serviceConfig.Dock = DockStyle.Fill;
            _tpServiceConfig.Controls.Add(_serviceConfig);
            _tpServiceConfig.Text = "服务器参数配置";
        }

        void CreateLssResourcesConfigControl() {
            TabContentManager.Instance.AddTabPage(_tpLssResources, _tbMain);

            if (_lssResources == null)
                _lssResources = new FrLssResources();

            _lssResources.Dock = DockStyle.Fill;
            _tpLssResources.Controls.Add(_lssResources);
            _tpLssResources.Text = "直播中继资源管理";
        }
    }
}

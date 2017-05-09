using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GM.Business.Module;
using GM.Orm;
using GM.Utilities;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    public partial class FrIesRecource : Form
    {

        private IESServerResource iesServerResource;
        private ArrayList nasName2NasServerMapping;
        private IESServer _iesServer;
        public FrIesRecource(IESServer iesServer) {
            InitializeComponent();
            _iesServer = iesServer;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GetResourse(_iesServer.Id);


            cbSourceURL.DisplayMember = "Key";
            cbSourceURL.DataSource = GetNasName2NasServerMapping();
        }

        void GetResourse(int iesId)
        {
            BeginInvoke(new EventHandler((s, a)=>
            {
                var vodResource = new Entities<IESServerResource>("equipment_id={0}", iesId).Cache;
                if (vodResource.Count > 0)
                {
                    var config = vodResource[0].ResourceConfig;
                    _txtName.Text = config.ResourceId;
                    _txtStoragePath.Text = config.SourceUrl;

                    iesServerResource = vodResource[0];

                    btnOk.Enabled = false;
                    button1.Enabled = true;
                }
                else
                {
                    btnOk.Enabled = true;
                    button1.Enabled = false;
                }
                  
            }));
        }

        ArrayList GetNasName2NasServerMapping()
        {
             var isa= GetISa();
            if (nasName2NasServerMapping == null)
            {
                nasName2NasServerMapping = new ArrayList();

                nasName2NasServerMapping.Add(new DictionaryEntry(isa.Name + "[内网ip:" + isa.IPAddress + "]", isa));
            }

            return nasName2NasServerMapping;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (iesServerResource == null)
                {
                    IESServerResource resource = new IESServerResource();
                    IESServerResourceConfig ppconfig = new IESServerResourceConfig();

                    var isaSer = GetSelectedNas();
                    var nasConfig = (isaSer.ServiceConfig as StorageServerConfig);
                    ppconfig.ResourceId = string.Format("{0}{1}", "vod_", isaSer.Name);
                    ppconfig.NasName = isaSer.Name;
                    ppconfig.AccessSourceUrlUserName = nasConfig.UserName;
                    ppconfig.AccessSourceUrlUserPassword = nasConfig.Password;
                    ppconfig.SourceUrl = nasConfig.StorageBasePath;
                    resource.EquipmentID = _iesServer.Id;
                    resource.ResourceConfig = ppconfig;
                    resource.StorageID = isaSer.Id;

                    _iesServer.AddResource(resource);
                }
             

                MessageBox.Show("添加发布点成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "更新发布点资源失败", ex);
                MessageBox.Show("更新发布点失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        StorageServer GetISa()
        {
            var ens = new Entities<StorageServer>("type_id={0}",new StorageServer().TypeID).Cache;

            return ens.Count > 0 ? ens[0] : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _iesServer.RemoveResource(iesServerResource);
                MessageBox.Show("删除发布点成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorFmt(LogCatalogs.OperationHits, ex, "删除发布点资源失败", ex);
                MessageBox.Show("删除发布点失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private StorageServer GetSelectedNas() {
            if (cbSourceURL.SelectedIndex == -1)
                return null;

            return ((DictionaryEntry)nasName2NasServerMapping[cbSourceURL.SelectedIndex]).Value as StorageServer;
        }
    }
}

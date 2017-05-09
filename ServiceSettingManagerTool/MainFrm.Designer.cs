namespace RedCdn.ClassRoom.ServiceSettingManagerTool {
    partial class MainFrm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.plLeft = new System.Windows.Forms.Panel();
            this.tvMenu = new System.Windows.Forms.TreeView();
            this.plRight = new System.Windows.Forms.Panel();
            this._tbMain = new System.Windows.Forms.TabControl();
            this._tpDeviceKey = new System.Windows.Forms.TabPage();
            this._tpServiceConfig = new System.Windows.Forms.TabPage();
            this._tpIsaConfig = new System.Windows.Forms.TabPage();
            this._tpLssResources = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.plLeft.SuspendLayout();
            this.plRight.SuspendLayout();
            this._tbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.plLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plRight);
            this.splitContainer1.Size = new System.Drawing.Size(676, 508);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 0;
            // 
            // plLeft
            // 
            this.plLeft.Controls.Add(this.tvMenu);
            this.plLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plLeft.Location = new System.Drawing.Point(0, 0);
            this.plLeft.Name = "plLeft";
            this.plLeft.Size = new System.Drawing.Size(179, 508);
            this.plLeft.TabIndex = 0;
            // 
            // tvMenu
            // 
            this.tvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMenu.HideSelection = false;
            this.tvMenu.Location = new System.Drawing.Point(0, 0);
            this.tvMenu.Name = "tvMenu";
            this.tvMenu.Size = new System.Drawing.Size(179, 508);
            this.tvMenu.TabIndex = 0;
            this.tvMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMenu_AfterSelect);
            // 
            // plRight
            // 
            this.plRight.Controls.Add(this._tbMain);
            this.plRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRight.Location = new System.Drawing.Point(0, 0);
            this.plRight.Name = "plRight";
            this.plRight.Size = new System.Drawing.Size(493, 508);
            this.plRight.TabIndex = 0;
            // 
            // _tbMain
            // 
            this._tbMain.Controls.Add(this._tpDeviceKey);
            this._tbMain.Controls.Add(this._tpServiceConfig);
            this._tbMain.Controls.Add(this._tpIsaConfig);
            this._tbMain.Controls.Add(this._tpLssResources);
            this._tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbMain.Location = new System.Drawing.Point(0, 0);
            this._tbMain.Name = "_tbMain";
            this._tbMain.SelectedIndex = 0;
            this._tbMain.Size = new System.Drawing.Size(493, 508);
            this._tbMain.TabIndex = 0;
            // 
            // _tpDeviceKey
            // 
            this._tpDeviceKey.Location = new System.Drawing.Point(4, 22);
            this._tpDeviceKey.Name = "_tpDeviceKey";
            this._tpDeviceKey.Padding = new System.Windows.Forms.Padding(3);
            this._tpDeviceKey.Size = new System.Drawing.Size(485, 482);
            this._tpDeviceKey.TabIndex = 0;
            this._tpDeviceKey.Text = "_tpDeviceKey";
            this._tpDeviceKey.UseVisualStyleBackColor = true;
            // 
            // _tpServiceConfig
            // 
            this._tpServiceConfig.Location = new System.Drawing.Point(4, 22);
            this._tpServiceConfig.Name = "_tpServiceConfig";
            this._tpServiceConfig.Padding = new System.Windows.Forms.Padding(3);
            this._tpServiceConfig.Size = new System.Drawing.Size(485, 482);
            this._tpServiceConfig.TabIndex = 1;
            this._tpServiceConfig.Text = "_tbServiceConfig";
            this._tpServiceConfig.UseVisualStyleBackColor = true;
            // 
            // _tpIsaConfig
            // 
            this._tpIsaConfig.Location = new System.Drawing.Point(4, 22);
            this._tpIsaConfig.Name = "_tpIsaConfig";
            this._tpIsaConfig.Size = new System.Drawing.Size(485, 482);
            this._tpIsaConfig.TabIndex = 2;
            this._tpIsaConfig.Text = "_tpIsaConfig";
            this._tpIsaConfig.UseVisualStyleBackColor = true;
            // 
            // _tpLssResources
            // 
            this._tpLssResources.Location = new System.Drawing.Point(4, 22);
            this._tpLssResources.Name = "_tpLssResources";
            this._tpLssResources.Size = new System.Drawing.Size(485, 482);
            this._tpLssResources.TabIndex = 3;
            this._tpLssResources.Text = "_tpLssResources";
            this._tpLssResources.UseVisualStyleBackColor = true;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 508);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务配置管理工具";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.plLeft.ResumeLayout(false);
            this.plRight.ResumeLayout(false);
            this._tbMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel plLeft;
        private System.Windows.Forms.TreeView tvMenu;
        private System.Windows.Forms.Panel plRight;
        private System.Windows.Forms.TabControl _tbMain;
        private System.Windows.Forms.TabPage _tpDeviceKey;
        private System.Windows.Forms.TabPage _tpServiceConfig;
        private System.Windows.Forms.TabPage _tpIsaConfig;
        private System.Windows.Forms.TabPage _tpLssResources;
    }
}


namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrLssResources {
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrLssResources));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labmsg1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._panelMain = new System.Windows.Forms.Panel();
            this._lvLssList = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChannelName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTVod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStartOrStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.colMainStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this._panelMain.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.labmsg1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 63);
            this.panel1.TabIndex = 6;
            // 
            // labmsg1
            // 
            this.labmsg1.AutoSize = true;
            this.labmsg1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labmsg1.Location = new System.Drawing.Point(75, 12);
            this.labmsg1.Name = "labmsg1";
            this.labmsg1.Size = new System.Drawing.Size(317, 12);
            this.labmsg1.TabIndex = 4;
            this.labmsg1.Text = "提供对直播中继资源管理，支持右键菜单设置，启动、停止";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(20, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // _panelMain
            // 
            this._panelMain.Controls.Add(this._lvLssList);
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(0, 63);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(493, 419);
            this._panelMain.TabIndex = 7;
            // 
            // _lvLssList
            // 
            this._lvLssList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colChannelName,
            this.colTs,
            this.colTVod,
            this.colEnable,
            this.colMainStatus});
            this._lvLssList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lvLssList.FullRowSelect = true;
            this._lvLssList.Location = new System.Drawing.Point(0, 0);
            this._lvLssList.Name = "_lvLssList";
            this._lvLssList.Size = new System.Drawing.Size(493, 419);
            this._lvLssList.TabIndex = 0;
            this._lvLssList.UseCompatibleStateImageBehavior = false;
            this._lvLssList.View = System.Windows.Forms.View.Details;
            this._lvLssList.SelectedIndexChanged += new System.EventHandler(this._lvLssList_SelectedIndexChanged);
            this._lvLssList.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lvLssList_MouseUp);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colChannelName
            // 
            this.colChannelName.Text = "频道名称";
            this.colChannelName.Width = 113;
            // 
            // colTs
            // 
            this.colTs.Text = "时移";
            this.colTs.Width = 79;
            // 
            // colTVod
            // 
            this.colTVod.Text = "回看";
            this.colTVod.Width = 48;
            // 
            // colEnable
            // 
            this.colEnable.Text = "是否启用";
            this.colEnable.Width = 86;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(446, 43);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(35, 12);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "刷 新";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStartOrStop,
            this.toolDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStartOrStop
            // 
            this.toolStartOrStop.Name = "toolStartOrStop";
            this.toolStartOrStop.Size = new System.Drawing.Size(152, 22);
            this.toolStartOrStop.Text = "启动";
            this.toolStartOrStop.Click += new System.EventHandler(this.toolStartOrStop_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(94, 22);
            this.toolDelete.Text = "删除";
            // 
            // colMainStatus
            // 
            this.colMainStatus.Text = "状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(75, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "注：暂时不支持实时刷新资源状态信息，请手动点击刷新";
            // 
            // FrLssResources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelMain);
            this.Controls.Add(this.panel1);
            this.Name = "FrLssResources";
            this.Size = new System.Drawing.Size(493, 482);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this._panelMain.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labmsg1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.ListView _lvLssList;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colChannelName;
        private System.Windows.Forms.ColumnHeader colTs;
        private System.Windows.Forms.ColumnHeader colTVod;
        private System.Windows.Forms.ColumnHeader colEnable;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStartOrStop;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private System.Windows.Forms.ColumnHeader colMainStatus;
        private System.Windows.Forms.Label label1;
    }
}

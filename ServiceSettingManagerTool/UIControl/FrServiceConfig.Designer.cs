namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrServiceConfig {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrServiceConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labmsg1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._rbLrs = new System.Windows.Forms.RadioButton();
            this._rbIes = new System.Windows.Forms.RadioButton();
            this._panelMain = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labmsg1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 63);
            this.panel1.TabIndex = 4;
            // 
            // labmsg1
            // 
            this.labmsg1.AutoSize = true;
            this.labmsg1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labmsg1.Location = new System.Drawing.Point(75, 12);
            this.labmsg1.Name = "labmsg1";
            this.labmsg1.Size = new System.Drawing.Size(257, 12);
            this.labmsg1.TabIndex = 4;
            this.labmsg1.Text = "提供对流服务器、直播中继服务器工作参数配置";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._rbLrs);
            this.groupBox1.Controls.Add(this._rbIes);
            this.groupBox1.Location = new System.Drawing.Point(26, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 43);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // _rbLrs
            // 
            this._rbLrs.AutoSize = true;
            this._rbLrs.Location = new System.Drawing.Point(256, 17);
            this._rbLrs.Name = "_rbLrs";
            this._rbLrs.Size = new System.Drawing.Size(83, 16);
            this._rbLrs.TabIndex = 1;
            this._rbLrs.TabStop = true;
            this._rbLrs.Text = "中继服务器";
            this._rbLrs.UseVisualStyleBackColor = true;
            this._rbLrs.CheckedChanged += new System.EventHandler(this._rbLrs_CheckedChanged);
            // 
            // _rbIes
            // 
            this._rbIes.AutoSize = true;
            this._rbIes.Checked = true;
            this._rbIes.Location = new System.Drawing.Point(76, 17);
            this._rbIes.Name = "_rbIes";
            this._rbIes.Size = new System.Drawing.Size(71, 16);
            this._rbIes.TabIndex = 0;
            this._rbIes.TabStop = true;
            this._rbIes.Text = "流服务器";
            this._rbIes.UseVisualStyleBackColor = true;
            this._rbIes.CheckedChanged += new System.EventHandler(this._rbIes_CheckedChanged);
            // 
            // _panelMain
            // 
            this._panelMain.Location = new System.Drawing.Point(26, 112);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(446, 328);
            this._panelMain.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(437, 453);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(35, 12);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "保 存";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // FrServiceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this._panelMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrServiceConfig";
            this.Size = new System.Drawing.Size(493, 482);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labmsg1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _rbIes;
        private System.Windows.Forms.RadioButton _rbLrs;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

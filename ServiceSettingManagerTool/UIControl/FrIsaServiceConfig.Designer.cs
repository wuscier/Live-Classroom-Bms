namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrIsaServiceConfig {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrIsaServiceConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labmsg1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this._lnkSave = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.panel1.TabIndex = 5;
            // 
            // labmsg1
            // 
            this.labmsg1.AutoSize = true;
            this.labmsg1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labmsg1.Location = new System.Drawing.Point(75, 12);
            this.labmsg1.Name = "labmsg1";
            this.labmsg1.Size = new System.Drawing.Size(131, 12);
            this.labmsg1.TabIndex = 4;
            this.labmsg1.Text = "提供ISA存储基目录设置";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "录制文件存储目录：";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(146, 106);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(326, 21);
            this.txtPath.TabIndex = 7;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // _lnkSave
            // 
            this._lnkSave.AutoSize = true;
            this._lnkSave.Enabled = false;
            this._lnkSave.Location = new System.Drawing.Point(437, 152);
            this._lnkSave.Name = "_lnkSave";
            this._lnkSave.Size = new System.Drawing.Size(35, 12);
            this._lnkSave.TabIndex = 8;
            this._lnkSave.TabStop = true;
            this._lnkSave.Text = "保 存";
            this._lnkSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkSave_LinkClicked);
            // 
            // FrIsaServiceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lnkSave);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FrIsaServiceConfig";
            this.Size = new System.Drawing.Size(493, 482);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labmsg1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.LinkLabel _lnkSave;
    }
}

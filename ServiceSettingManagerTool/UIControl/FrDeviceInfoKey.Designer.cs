namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrDeviceInfoKey {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrDeviceInfoKey));
            this.label1 = new System.Windows.Forms.Label();
            this._txtDeviceKey = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._lnkCreateDeviceKey = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labmsg1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统DeviceKey:";
            // 
            // _txtDeviceKey
            // 
            this._txtDeviceKey.BackColor = System.Drawing.SystemColors.Info;
            this._txtDeviceKey.Location = new System.Drawing.Point(113, 32);
            this._txtDeviceKey.Name = "_txtDeviceKey";
            this._txtDeviceKey.ReadOnly = true;
            this._txtDeviceKey.Size = new System.Drawing.Size(323, 21);
            this._txtDeviceKey.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._lnkCreateDeviceKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._txtDeviceKey);
            this.groupBox1.Location = new System.Drawing.Point(20, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // _lnkCreateDeviceKey
            // 
            this._lnkCreateDeviceKey.AutoSize = true;
            this._lnkCreateDeviceKey.Enabled = false;
            this._lnkCreateDeviceKey.Location = new System.Drawing.Point(401, 69);
            this._lnkCreateDeviceKey.Name = "_lnkCreateDeviceKey";
            this._lnkCreateDeviceKey.Size = new System.Drawing.Size(35, 12);
            this._lnkCreateDeviceKey.TabIndex = 2;
            this._lnkCreateDeviceKey.TabStop = true;
            this._lnkCreateDeviceKey.Text = "创 建";
            this._lnkCreateDeviceKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkCreateDeviceKey_LinkClicked);
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
            this.panel1.TabIndex = 3;
            // 
            // labmsg1
            // 
            this.labmsg1.AutoSize = true;
            this.labmsg1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labmsg1.Location = new System.Drawing.Point(75, 12);
            this.labmsg1.Name = "labmsg1";
            this.labmsg1.Size = new System.Drawing.Size(293, 12);
            this.labmsg1.TabIndex = 4;
            this.labmsg1.Text = "创建服务器设备DeviceId，首次运行需要手动出发创建";
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
            // FrDeviceInfoKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrDeviceInfoKey";
            this.Size = new System.Drawing.Size(493, 482);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtDeviceKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel _lnkCreateDeviceKey;
        private System.Windows.Forms.Label labmsg1;
    }
}

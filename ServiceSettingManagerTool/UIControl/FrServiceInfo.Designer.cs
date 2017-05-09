namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrServiceInfo {
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerName = new System.Windows.Forms.TextBox();
            this.txtInstallPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddressIp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRemortPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名 称：";
            // 
            // txtSerName
            // 
            this.txtSerName.Location = new System.Drawing.Point(72, 14);
            this.txtSerName.Name = "txtSerName";
            this.txtSerName.Size = new System.Drawing.Size(106, 21);
            this.txtSerName.TabIndex = 1;
            this.txtSerName.TextChanged += new System.EventHandler(this.txtSerName_TextChanged);
            // 
            // txtInstallPath
            // 
            this.txtInstallPath.Location = new System.Drawing.Point(72, 41);
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.Size = new System.Drawing.Size(293, 21);
            this.txtInstallPath.TabIndex = 3;
            this.txtInstallPath.TextChanged += new System.EventHandler(this.txtInstallPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "安装目录：";
            // 
            // txtURI
            // 
            this.txtURI.BackColor = System.Drawing.SystemColors.Info;
            this.txtURI.Location = new System.Drawing.Point(259, 14);
            this.txtURI.Name = "txtURI";
            this.txtURI.ReadOnly = true;
            this.txtURI.Size = new System.Drawing.Size(106, 21);
            this.txtURI.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "管理Uri：";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(72, 69);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(106, 21);
            this.txtIp.TabIndex = 7;
            this.txtIp.TextChanged += new System.EventHandler(this.txtIp_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "内网IP：";
            // 
            // txtAddressIp
            // 
            this.txtAddressIp.Location = new System.Drawing.Point(259, 68);
            this.txtAddressIp.Name = "txtAddressIp";
            this.txtAddressIp.Size = new System.Drawing.Size(106, 21);
            this.txtAddressIp.TabIndex = 9;
            this.txtAddressIp.TextChanged += new System.EventHandler(this.txtAddressIp_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "外网IP：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "协议：";
            // 
            // cbProtocol
            // 
            this.cbProtocol.BackColor = System.Drawing.SystemColors.Info;
            this.cbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocol.Enabled = false;
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Items.AddRange(new object[] {
            "TCP",
            "HTTP"});
            this.cbProtocol.Location = new System.Drawing.Point(72, 96);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(106, 20);
            this.cbProtocol.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "管理端口：";
            // 
            // txtRemortPort
            // 
            this.txtRemortPort.Location = new System.Drawing.Point(259, 95);
            this.txtRemortPort.Name = "txtRemortPort";
            this.txtRemortPort.Size = new System.Drawing.Size(106, 21);
            this.txtRemortPort.TabIndex = 13;
            this.txtRemortPort.TextChanged += new System.EventHandler(this.txtRemortPort_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "启动方式：";
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Location = new System.Drawing.Point(72, 123);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(72, 16);
            this.cbAutoStart.TabIndex = 15;
            this.cbAutoStart.Text = "自动启动";
            this.cbAutoStart.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "采集告警：";
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Location = new System.Drawing.Point(259, 122);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(48, 16);
            this.cbAlarm.TabIndex = 17;
            this.cbAlarm.Text = "启用";
            this.cbAlarm.UseVisualStyleBackColor = true;
            // 
            // FrServiceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.cbAlarm);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbAutoStart);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRemortPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbProtocol);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAddressIp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtURI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInstallPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSerName);
            this.Controls.Add(this.label1);
            this.Name = "FrServiceInfo";
            this.Size = new System.Drawing.Size(384, 179);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerName;
        private System.Windows.Forms.TextBox txtInstallPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddressIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRemortPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbAlarm;
    }
}

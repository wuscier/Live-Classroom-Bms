namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrLssServiceConfig {
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
            this._panelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._txtListenPort = new System.Windows.Forms.TextBox();
            this._txtReportUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._cbDBSync = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMain
            // 
            this._panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelMain.Location = new System.Drawing.Point(24, 14);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(403, 178);
            this._panelMain.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._cbDBSync);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._txtListenPort);
            this.panel1.Controls.Add(this._txtReportUrl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 82);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "汇报接口地址：";
            // 
            // _txtListenPort
            // 
            this._txtListenPort.Location = new System.Drawing.Point(83, 6);
            this._txtListenPort.Name = "_txtListenPort";
            this._txtListenPort.Size = new System.Drawing.Size(73, 21);
            this._txtListenPort.TabIndex = 1;
            this._txtListenPort.TextChanged += new System.EventHandler(this._txtListenPort_TextChanged);
            // 
            // _txtReportUrl
            // 
            this._txtReportUrl.Location = new System.Drawing.Point(257, 8);
            this._txtReportUrl.Name = "_txtReportUrl";
            this._txtReportUrl.Size = new System.Drawing.Size(139, 21);
            this._txtReportUrl.TabIndex = 3;
            this._txtReportUrl.TextChanged += new System.EventHandler(this._txtReportUrl_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "监听端口：";
            // 
            // _cbDBSync
            // 
            this._cbDBSync.AutoSize = true;
            this._cbDBSync.Location = new System.Drawing.Point(15, 33);
            this._cbDBSync.Name = "_cbDBSync";
            this._cbDBSync.Size = new System.Drawing.Size(96, 16);
            this._cbDBSync.TabIndex = 11;
            this._cbDBSync.Text = "启用数据同步";
            this._cbDBSync.UseVisualStyleBackColor = true;
            this._cbDBSync.CheckedChanged += new System.EventHandler(this._cbDBSync_CheckedChanged);
            // 
            // FrLssServiceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._panelMain);
            this.Name = "FrLssServiceConfig";
            this.Size = new System.Drawing.Size(446, 328);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtListenPort;
        private System.Windows.Forms.TextBox _txtReportUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _cbDBSync;
    }
}

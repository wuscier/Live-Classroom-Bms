namespace RedCdn.ClassRoom.ServiceSettingManagerTool.UIControl {
    partial class FrIESServiceConfig {
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
            this._txtRTSP = new System.Windows.Forms.TextBox();
            this._txtRFCRTSP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._txtMediaPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._panelMain = new System.Windows.Forms.Panel();
            this._txtUploadBand = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._txtDownloadBand = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lnkPublishPoint = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "RTSP端口：";
            // 
            // _txtRTSP
            // 
            this._txtRTSP.Location = new System.Drawing.Point(83, 6);
            this._txtRTSP.Name = "_txtRTSP";
            this._txtRTSP.Size = new System.Drawing.Size(88, 21);
            this._txtRTSP.TabIndex = 1;
            this._txtRTSP.TextChanged += new System.EventHandler(this._txtControl_TextChanged);
            // 
            // _txtRFCRTSP
            // 
            this._txtRFCRTSP.Location = new System.Drawing.Point(296, 8);
            this._txtRFCRTSP.Name = "_txtRFCRTSP";
            this._txtRFCRTSP.Size = new System.Drawing.Size(102, 21);
            this._txtRFCRTSP.TabIndex = 3;
            this._txtRFCRTSP.TextChanged += new System.EventHandler(this._txtRFCRTSP_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "RFCRTSP端口：";
            // 
            // _txtMediaPort
            // 
            this._txtMediaPort.Location = new System.Drawing.Point(296, 35);
            this._txtMediaPort.Name = "_txtMediaPort";
            this._txtMediaPort.Size = new System.Drawing.Size(102, 21);
            this._txtMediaPort.TabIndex = 5;
            this._txtMediaPort.TextChanged += new System.EventHandler(this._txtMediaPort_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "时移HTTP端口：";
            // 
            // _panelMain
            // 
            this._panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelMain.Location = new System.Drawing.Point(24, 14);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(403, 178);
            this._panelMain.TabIndex = 6;
            // 
            // _txtUploadBand
            // 
            this._txtUploadBand.Location = new System.Drawing.Point(113, 33);
            this._txtUploadBand.Name = "_txtUploadBand";
            this._txtUploadBand.Size = new System.Drawing.Size(58, 21);
            this._txtUploadBand.TabIndex = 8;
            this._txtUploadBand.TextChanged += new System.EventHandler(this._txtUploadBand_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "最大服务器带宽：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "级联下载最大带宽：";
            // 
            // _txtDownloadBand
            // 
            this._txtDownloadBand.Location = new System.Drawing.Point(296, 64);
            this._txtDownloadBand.Name = "_txtDownloadBand";
            this._txtDownloadBand.Size = new System.Drawing.Size(102, 21);
            this._txtDownloadBand.TabIndex = 10;
            this._txtDownloadBand.TextChanged += new System.EventHandler(this._txtDownloadBand_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._lnkPublishPoint);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._txtUploadBand);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._txtDownloadBand);
            this.panel1.Controls.Add(this._txtRTSP);
            this.panel1.Controls.Add(this._txtRFCRTSP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this._txtMediaPort);
            this.panel1.Location = new System.Drawing.Point(24, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 112);
            this.panel1.TabIndex = 7;
            // 
            // _lnkPublishPoint
            // 
            this._lnkPublishPoint.AutoSize = true;
            this._lnkPublishPoint.Location = new System.Drawing.Point(25, 87);
            this._lnkPublishPoint.Name = "_lnkPublishPoint";
            this._lnkPublishPoint.Size = new System.Drawing.Size(41, 12);
            this._lnkPublishPoint.TabIndex = 11;
            this._lnkPublishPoint.TabStop = true;
            this._lnkPublishPoint.Text = "发布点";
            this._lnkPublishPoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkPublishPoint_LinkClicked);
            // 
            // FrIESServiceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._panelMain);
            this.Name = "FrIESServiceConfig";
            this.Size = new System.Drawing.Size(446, 328);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtRTSP;
        private System.Windows.Forms.TextBox _txtRFCRTSP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtMediaPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.TextBox _txtUploadBand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _txtDownloadBand;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel _lnkPublishPoint;
    }
}

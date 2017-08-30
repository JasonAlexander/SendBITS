namespace SendBITS.UI
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPingWaitTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConHops = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConnectionHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbShowConsole = new System.Windows.Forms.CheckBox();
            this.ckbUseWinforms = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ckbRunMonitorMode = new System.Windows.Forms.CheckBox();
            this.txtMonitorPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMonitorHost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFindLocalFolder = new System.Windows.Forms.Button();
            this.btnFindToFolder = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.ckbUseBITSLog = new System.Windows.Forms.CheckBox();
            this.txtLocalFolderToMonitor = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTransferToFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTransferHost = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.epInputError = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ckbAllowRemoteMonitor = new System.Windows.Forms.CheckBox();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epInputError)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPingWaitTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtConTimeout);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtConHops);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtConnectionHost);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Monitor";
            // 
            // txtPingWaitTime
            // 
            this.txtPingWaitTime.Location = new System.Drawing.Point(9, 149);
            this.txtPingWaitTime.Name = "txtPingWaitTime";
            this.txtPingWaitTime.Size = new System.Drawing.Size(129, 20);
            this.txtPingWaitTime.TabIndex = 7;
            this.txtPingWaitTime.Tag = "int";
            this.txtPingWaitTime.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connection Timeout (ms)";
            // 
            // txtConTimeout
            // 
            this.txtConTimeout.Location = new System.Drawing.Point(9, 110);
            this.txtConTimeout.Name = "txtConTimeout";
            this.txtConTimeout.Size = new System.Drawing.Size(129, 20);
            this.txtConTimeout.TabIndex = 3;
            this.txtConTimeout.Tag = "int";
            this.txtConTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wait Time (ms)";
            // 
            // txtConHops
            // 
            this.txtConHops.Location = new System.Drawing.Point(9, 71);
            this.txtConHops.Name = "txtConHops";
            this.txtConHops.Size = new System.Drawing.Size(129, 20);
            this.txtConHops.TabIndex = 5;
            this.txtConHops.Tag = "int";
            this.txtConHops.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Connection Hops";
            // 
            // txtConnectionHost
            // 
            this.txtConnectionHost.Location = new System.Drawing.Point(9, 32);
            this.txtConnectionHost.Name = "txtConnectionHost";
            this.txtConnectionHost.Size = new System.Drawing.Size(129, 20);
            this.txtConnectionHost.TabIndex = 1;
            this.txtConnectionHost.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection Host";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ckbShowConsole);
            this.groupBox2.Controls.Add(this.ckbUseWinforms);
            this.groupBox2.Location = new System.Drawing.Point(364, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 68);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UI Options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Show Console";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Use Winforms\r";
            // 
            // ckbShowConsole
            // 
            this.ckbShowConsole.AutoSize = true;
            this.ckbShowConsole.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbShowConsole.Location = new System.Drawing.Point(95, 38);
            this.ckbShowConsole.Name = "ckbShowConsole";
            this.ckbShowConsole.Size = new System.Drawing.Size(15, 14);
            this.ckbShowConsole.TabIndex = 2;
            this.ckbShowConsole.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ckbShowConsole.UseVisualStyleBackColor = true;
            // 
            // ckbUseWinforms
            // 
            this.ckbUseWinforms.AutoSize = true;
            this.ckbUseWinforms.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbUseWinforms.Location = new System.Drawing.Point(95, 15);
            this.ckbUseWinforms.Name = "ckbUseWinforms";
            this.ckbUseWinforms.Size = new System.Drawing.Size(15, 14);
            this.ckbUseWinforms.TabIndex = 1;
            this.ckbUseWinforms.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ckbUseWinforms.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.ckbRunMonitorMode);
            this.groupBox3.Controls.Add(this.txtMonitorPort);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtMonitorHost);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(188, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 119);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remote Monitor";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Run Monitor Mode";
            // 
            // ckbRunMonitorMode
            // 
            this.ckbRunMonitorMode.AutoSize = true;
            this.ckbRunMonitorMode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRunMonitorMode.Location = new System.Drawing.Point(123, 94);
            this.ckbRunMonitorMode.Name = "ckbRunMonitorMode";
            this.ckbRunMonitorMode.Size = new System.Drawing.Size(15, 14);
            this.ckbRunMonitorMode.TabIndex = 6;
            this.ckbRunMonitorMode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ckbRunMonitorMode.UseVisualStyleBackColor = true;
            // 
            // txtMonitorPort
            // 
            this.txtMonitorPort.Location = new System.Drawing.Point(9, 71);
            this.txtMonitorPort.Name = "txtMonitorPort";
            this.txtMonitorPort.Size = new System.Drawing.Size(129, 20);
            this.txtMonitorPort.TabIndex = 5;
            this.txtMonitorPort.Tag = "int";
            this.txtMonitorPort.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Port to Monitor";
            // 
            // txtMonitorHost
            // 
            this.txtMonitorHost.Location = new System.Drawing.Point(9, 32);
            this.txtMonitorHost.Name = "txtMonitorHost";
            this.txtMonitorHost.Size = new System.Drawing.Size(129, 20);
            this.txtMonitorHost.TabIndex = 3;
            this.txtMonitorHost.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Host to Monitor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFindLocalFolder);
            this.groupBox4.Controls.Add(this.btnFindToFolder);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.ckbUseBITSLog);
            this.groupBox4.Controls.Add(this.txtLocalFolderToMonitor);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtTransferToFolder);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtTransferHost);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(12, 197);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(522, 142);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "BITS Transfer";
            // 
            // btnFindLocalFolder
            // 
            this.btnFindLocalFolder.Location = new System.Drawing.Point(484, 110);
            this.btnFindLocalFolder.Name = "btnFindLocalFolder";
            this.btnFindLocalFolder.Size = new System.Drawing.Size(32, 20);
            this.btnFindLocalFolder.TabIndex = 11;
            this.btnFindLocalFolder.Text = "....";
            this.btnFindLocalFolder.UseVisualStyleBackColor = true;
            this.btnFindLocalFolder.Click += new System.EventHandler(this.btnFindLocalFolder_Click);
            // 
            // btnFindToFolder
            // 
            this.btnFindToFolder.Location = new System.Drawing.Point(484, 71);
            this.btnFindToFolder.Name = "btnFindToFolder";
            this.btnFindToFolder.Size = new System.Drawing.Size(32, 20);
            this.btnFindToFolder.TabIndex = 10;
            this.btnFindToFolder.Text = "....";
            this.btnFindToFolder.UseVisualStyleBackColor = true;
            this.btnFindToFolder.Click += new System.EventHandler(this.btnFindToFolder_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(173, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Use BITS Log";
            // 
            // ckbUseBITSLog
            // 
            this.ckbUseBITSLog.AutoSize = true;
            this.ckbUseBITSLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbUseBITSLog.Location = new System.Drawing.Point(253, 35);
            this.ckbUseBITSLog.Name = "ckbUseBITSLog";
            this.ckbUseBITSLog.Size = new System.Drawing.Size(15, 14);
            this.ckbUseBITSLog.TabIndex = 8;
            this.ckbUseBITSLog.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ckbUseBITSLog.UseVisualStyleBackColor = true;
            // 
            // txtLocalFolderToMonitor
            // 
            this.txtLocalFolderToMonitor.Location = new System.Drawing.Point(9, 110);
            this.txtLocalFolderToMonitor.Name = "txtLocalFolderToMonitor";
            this.txtLocalFolderToMonitor.Size = new System.Drawing.Size(453, 20);
            this.txtLocalFolderToMonitor.TabIndex = 7;
            this.txtLocalFolderToMonitor.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Local Folder to Monitor";
            // 
            // txtTransferToFolder
            // 
            this.txtTransferToFolder.Location = new System.Drawing.Point(9, 71);
            this.txtTransferToFolder.Name = "txtTransferToFolder";
            this.txtTransferToFolder.Size = new System.Drawing.Size(453, 20);
            this.txtTransferToFolder.TabIndex = 5;
            this.txtTransferToFolder.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Transfer to Folder";
            // 
            // txtTransferHost
            // 
            this.txtTransferHost.Location = new System.Drawing.Point(9, 32);
            this.txtTransferHost.Name = "txtTransferHost";
            this.txtTransferHost.Size = new System.Drawing.Size(129, 20);
            this.txtTransferHost.TabIndex = 3;
            this.txtTransferHost.Validating += new System.ComponentModel.CancelEventHandler(this.txtbox_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Transfer to Host";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(378, 345);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(459, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // epInputError
            // 
            this.epInputError.ContainerControl = this;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.ckbAllowRemoteMonitor);
            this.groupBox5.Controls.Add(this.txtLocalPort);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Location = new System.Drawing.Point(364, 86);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 75);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Local Monitor";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Allow Remote Monitor";
            // 
            // ckbAllowRemoteMonitor
            // 
            this.ckbAllowRemoteMonitor.AutoSize = true;
            this.ckbAllowRemoteMonitor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbAllowRemoteMonitor.Location = new System.Drawing.Point(123, 50);
            this.ckbAllowRemoteMonitor.Name = "ckbAllowRemoteMonitor";
            this.ckbAllowRemoteMonitor.Size = new System.Drawing.Size(15, 14);
            this.ckbAllowRemoteMonitor.TabIndex = 6;
            this.ckbAllowRemoteMonitor.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ckbAllowRemoteMonitor.UseVisualStyleBackColor = true;
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(9, 28);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(129, 20);
            this.txtLocalPort.TabIndex = 5;
            this.txtLocalPort.Tag = "int";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Local Port";
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(544, 376);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 415);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 415);
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epInputError)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConnectionHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPingWaitTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConHops;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbShowConsole;
        private System.Windows.Forms.CheckBox ckbUseWinforms;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMonitorHost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbRunMonitorMode;
        private System.Windows.Forms.TextBox txtMonitorPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtLocalFolderToMonitor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTransferToFolder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTransferHost;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFindLocalFolder;
        private System.Windows.Forms.Button btnFindToFolder;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox ckbUseBITSLog;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider epInputError;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox ckbAllowRemoteMonitor;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label label15;
    }
}
namespace SendBITS.UI
{
    partial class frmConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsole));
            this.txtConsoleOut = new System.Windows.Forms.TextBox();
            this.txtAppCommand = new System.Windows.Forms.TextBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConsoleOut
            // 
            this.txtConsoleOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsoleOut.Location = new System.Drawing.Point(12, 12);
            this.txtConsoleOut.Multiline = true;
            this.txtConsoleOut.Name = "txtConsoleOut";
            this.txtConsoleOut.ReadOnly = true;
            this.txtConsoleOut.Size = new System.Drawing.Size(464, 301);
            this.txtConsoleOut.TabIndex = 0;
            // 
            // txtAppCommand
            // 
            this.txtAppCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppCommand.Location = new System.Drawing.Point(12, 321);
            this.txtAppCommand.Name = "txtAppCommand";
            this.txtAppCommand.Size = new System.Drawing.Size(383, 20);
            this.txtAppCommand.TabIndex = 1;
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendCommand.Location = new System.Drawing.Point(401, 319);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(75, 23);
            this.btnSendCommand.TabIndex = 2;
            this.btnSendCommand.Text = "Send";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            // 
            // frmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 354);
            this.Controls.Add(this.btnSendCommand);
            this.Controls.Add(this.txtAppCommand);
            this.Controls.Add(this.txtConsoleOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsole";
            this.Text = "SendBITS Console";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConsole_FormClosed);
            this.Load += new System.EventHandler(this.frmConsole_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsoleOut;
        private System.Windows.Forms.TextBox txtAppCommand;
        private System.Windows.Forms.Button btnSendCommand;
    }
}
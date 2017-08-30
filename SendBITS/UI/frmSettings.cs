using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBITS.UI
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            SetConMonTxt();
            SetRemoteMonitor();
            SetLocalMonitor();
            SetUIOptions();
            SetBITS();
        }

        private void SetConMonTxt()
        {
            txtConnectionHost.DataBindings.Add("Text", AppManager.Settings, "ConMonHost", true, DataSourceUpdateMode.Never);
            txtConHops.DataBindings.Add("Text", AppManager.Settings, "ConMonHops", true, DataSourceUpdateMode.Never);
            txtConTimeout.DataBindings.Add("Text", AppManager.Settings, "ConMonTimeout", true, DataSourceUpdateMode.Never);
            txtPingWaitTime.DataBindings.Add("Text", AppManager.Settings, "ConMonWait", true, DataSourceUpdateMode.Never);
        }

        private void SetRemoteMonitor()
        {
            txtMonitorHost.DataBindings.Add("Text", AppManager.Settings, "MonitorHost", true, DataSourceUpdateMode.Never);
            txtMonitorPort.DataBindings.Add("Text", AppManager.Settings, "MonitorPort", true, DataSourceUpdateMode.Never);
            ckbRunMonitorMode.DataBindings.Add("Checked", AppManager.Settings, "MonitorMode", true, DataSourceUpdateMode.Never);
        }

        private void SetLocalMonitor()
        {
            txtLocalPort.DataBindings.Add("Text", AppManager.Settings, "LogPort", true, DataSourceUpdateMode.Never);
            ckbAllowRemoteMonitor.DataBindings.Add("Checked", AppManager.Settings, "AllowRemoteMonitor", true, DataSourceUpdateMode.Never);
        }

        private void SetUIOptions()
        {
            ckbUseWinforms.DataBindings.Add("Checked", AppManager.Settings, "UseWinForms", true, DataSourceUpdateMode.Never);
            ckbShowConsole.DataBindings.Add("Checked", AppManager.Settings, "ShowConsole", true, DataSourceUpdateMode.Never);
        }

        private void SetBITS()
        {
            txtLocalFolderToMonitor.DataBindings.Add("Text", AppManager.Settings, "WatchFolder", true, DataSourceUpdateMode.Never);
            txtTransferToFolder.DataBindings.Add("Text", AppManager.Settings, "CopyToFolder", true, DataSourceUpdateMode.Never);
            txtTransferHost.DataBindings.Add("Text", AppManager.Settings, "CopyToHost", true, DataSourceUpdateMode.Never);
            ckbUseBITSLog.DataBindings.Add("Checked", AppManager.Settings, "UseMSBitsLog", true, DataSourceUpdateMode.Never);
        }

        private void btnFindToFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Uri uri = new Uri(fbd.SelectedPath, UriKind.RelativeOrAbsolute);
                    txtTransferHost.Text = uri.Host.ToUpper();
                    txtTransferToFolder.Text = uri.AbsolutePath.Replace("/", "\\");
                }
            }
        }

        private void btnFindLocalFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    txtLocalFolderToMonitor.Text = fbd.SelectedPath;
                }
            }
        }
        
        private void txtbox_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                e.Cancel = true;
                epInputError.SetError((Control)sender, "Value can not be empty");
                return;
            }
            else if(((TextBox)sender).Tag != null)
            {
                int i = 0;
                if (!int.TryParse(((TextBox)sender).Text, out i))
                {
                    e.Cancel = true;
                    epInputError.SetError((Control)sender, "Value must be NON-Zero Interger");
                    return;
                }
            }
            epInputError.Clear();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if (c.GetType() == typeof(GroupBox))
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc.DataBindings.Count > 0)
                        {
                            if (cc.GetType() == typeof(TextBox))
                            {
                                AppManager.Settings
                                          .UpdateSetting(cc.DataBindings[0]
                                          .BindingMemberInfo
                                          .BindingMember, ((TextBox)cc).Text);
                            }
                            else if(cc.GetType() == typeof(CheckBox))
                            {
                                AppManager.Settings
                                          .UpdateSetting(cc.DataBindings[0]
                                          .BindingMemberInfo
                                          .BindingMember, ((CheckBox)cc).Checked);
                            }
                        }
                    }
                }
            }
        }
    }
}
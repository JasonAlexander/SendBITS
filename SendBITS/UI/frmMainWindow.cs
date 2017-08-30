using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendBITS.Logging;
using System.Diagnostics;

namespace SendBITS.UI
{
    public partial class frmMainWindow : Form
    {
        //private Dictionary<string, FileMsg> workingSet;
        private object lockingObject;
        public frmMainWindow()
        {
            //workingSet = new Dictionary<string, FileMsg>();
            lockingObject = new object();
            InitializeComponent();
            listView1.GetType()
                     .GetProperty("DoubleBuffered", 
                                   System.Reflection.BindingFlags.Instance | 
                                   System.Reflection.BindingFlags.NonPublic)
                     .SetValue(listView1, true, null);
            tsslStatus.Text = string.Empty;
            tsslErrorWarning.Text = string.Empty;
            Log.Manager.ConnectionStatusEvent += Manager_ConnectionStatusEvent;
            Log.Manager.FileTransferCompletedEvent += Manager_FileTransferCompletedEvent;
            Log.Manager.FileTransferErrorEvent += Manager_FileTransferErrorEvent;
            Log.Manager.FileTransferStartedEvent += Manager_FileTransferStartedEvent;
            Log.Manager.FileTransferStoppedEvent += Manager_FileTransferStoppedEvent;
            Log.Manager.FileTransferUpdateEvent += Manager_FileTransferUpdateEvent;
            Log.Manager.SendMessageEvent += Manager_SendMessageEvent;
            Log.Manager.NewFileMsgEvent += Manager_NewFileMsgEvent;
        }

        private void AddFile(FileMsg File)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FileMsg>(AddFile), File);
                return;
            }
            //tsslStatus.Text = "New File Found: " + File.FileName;
            //workingSet.Add(File.FileID, File);

            var item = new ListViewItem(
                            new string[] 
                                {
                                    File.FileName,
                                    File.Status,
                                    File.Transfered.ToString(),
                                    File.Size.ToString(),
                                    File.Percent }, 
                            listView1.Groups["lvgQueued"]);
            item.Name = File.FileID;
            listView1.Items.Add(item);
        }
        private void UpdatetsslErrorWarning(string Message, MessageType MsgType)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<string, MessageType>(UpdatetsslErrorWarning), Message, MsgType);
                return;
            }

            switch (MsgType)
            {
                case MessageType.Warning:
                    tsslErrorWarning.BackColor = Color.Yellow;
                    tsslErrorWarning.Text = Message;
                    break;
                case MessageType.Error:
                    tsslErrorWarning.BackColor = Color.Red;
                    tsslErrorWarning.Text = Message;
                    break;
                case MessageType.General:
                    tsslErrorWarning.BackColor = SystemColors.Control;
                    tsslErrorWarning.Text = string.Empty;
                    break;
                case MessageType.Apocalypse:
                    break;
            }
        }
        private void UpdateItem(FileMsg File)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<FileMsg>(UpdateItem), File);
                return;
            }
            lock(lockingObject)
            {
                switch (File.Status)
                {
                    case "Connecting":
                    case "Transferring":
                        listView1.Items[File.FileID].Group = listView1.Groups["lvgProcessing"];
                        break;
                    case "Verified":                        
                    case "Acknowledged":
                    case "Transferred":
                        listView1.Items[File.FileID].Group = listView1.Groups["lvgCompleted"];
                        break;
                    case "TransientError":
                    case "Error":
                        listView1.Items[File.FileID].Group = listView1.Groups["lvgError"];
                        break;
                }
                listView1.Items[File.FileID].SubItems[1].Text = File.Status;
                listView1.Items[File.FileID].SubItems[2].Text = File.Transfered.ToString();
                listView1.Items[File.FileID].SubItems[4].Text = File.Percent;
            }
            UpdateGroupCount();
        }
        private void UpdateGroupCount()
        {
            int total = listView1.Groups["lvgProcessing"].Items.Count +
                        listView1.Groups["lvgCompleted"].Items.Count +
                        listView1.Groups["lvgError"].Items.Count +
                        listView1.Groups["lvgQueued"].Items.Count;
            int comp = listView1.Groups["lvgCompleted"].Items.Count;
            listView1.Groups["lvgProcessing"].Header = string.Format("Processing ({0})", listView1.Groups["lvgProcessing"].Items.Count);
            listView1.Groups["lvgCompleted"].Header = string.Format("Completed ({0})", listView1.Groups["lvgCompleted"].Items.Count);
            listView1.Groups["lvgError"].Header = string.Format("lvgError ({0})", listView1.Groups["lvgError"].Items.Count);
            listView1.Groups["lvgQueued"].Header = string.Format("Queued ({0})", listView1.Groups["lvgQueued"].Items.Count);
            tsslStatus.Text = string.Format("P:{0}|C:{1}|E:{2}|Q:{3}|T:{4}",
                                            listView1.Groups["lvgProcessing"].Items.Count,
                                            listView1.Groups["lvgCompleted"].Items.Count,
                                            listView1.Groups["lvgError"].Items.Count,
                                            listView1.Groups["lvgQueued"].Items.Count,
                                            total);
            tsslAllJobsProgress.Maximum = total;
            tsslAllJobsProgress.Value = comp;
        }

        private void Manager_NewFileMsgEvent(FileMsg File)
        {
            AddFile(File);
        }
        private void Manager_SendMessageEvent(string Message, MessageType MsgType)
        {
            UpdatetsslErrorWarning(Message, MsgType);
        }
        private void Manager_FileTransferUpdateEvent(FileMsg File)
        {
            UpdateItem(File);
        }
        private void Manager_FileTransferStoppedEvent(FileMsg File)
        {
            UpdateItem(File);
        }
        private void Manager_FileTransferStartedEvent(FileMsg File)
        {
            UpdateItem(File);
        }
        private void Manager_FileTransferErrorEvent(FileMsg File)
        {
            UpdateItem(File);
        }
        private void Manager_FileTransferCompletedEvent(FileMsg File)
        {
            UpdateItem(File);
        }

        private void UpdateConnectionStatus(string Status, long RoundTripTime)
        {
            if(Status == "Success")
            {
                tsslConnectionStatus.Text = Status + " :: " + RoundTripTime.ToString();
                tsslConnectionStatus.BackColor = Color.LimeGreen;
                tsslConnectionStatus.ForeColor = Color.Black;
            }
            else
            {
                tsslConnectionStatus.Text = "Bad Connection";
                tsslConnectionStatus.BackColor = Color.Red;
                tsslConnectionStatus.ForeColor = Color.White;
            }
        }
        private void Manager_ConnectionStatusEvent(string Status, long RoundTripTime)
        {
            if(statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new Action<string, long>(UpdateConnectionStatus), Status, RoundTripTime);
                return;
            }
            UpdateConnectionStatus(Status, RoundTripTime);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var fv = AppManager.Settings;
            frmSettings f = new frmSettings();
            f.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Logging.Log.Manager.FileTransferVerify(listView1.SelectedItems[0].Name);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        private void btnLocal_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", AppManager.Settings.WatchFolder);
        }
        private void btnRemote_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\\\\" + AppManager.Settings.CopyToHost + AppManager.Settings.CopyToFolder);
        }

        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("CONFIRM EXIT", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }
    }
}

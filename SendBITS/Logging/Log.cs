using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBITS.Logging
{
    public sealed class Log : ManagerBase<Log>
    {
        public event ConnectionStatusHandler ConnectionStatusEvent;
        public event SendMessageHandler SendMessageEvent;

        public event NewFileMsgHandler NewFileMsgEvent;
        public event ChangedFileMsgHandler ChangedFileMsgEvent;
        public event DeletedFileMsgHandler DeletedFileMsgEvent;
        public event FileTransferStartedHandler FileTransferStartedEvent;
        public event FileTransferStoppedHandler FileTransferStoppedEvent;
        public event FileTransferUpdateHandler FileTransferUpdateEvent;
        public event FileTransferCompletedHander FileTransferCompletedEvent;
        public event FileTransferErrorHander FileTransferErrorEvent;
        public event FileTransferVerifyHandler FileTransferVerifyEvent;

        public event AppStartingUpHandler AppStartingUpEvent;
        public event AppStartedHandler AppStartedEvent;
        public event AppShuttingDownHandler AppShuttingDownEvent;
        public event AppShutDownHandler AppShutDownEvent;

        public event AppSettingsLoadedHandler AppSettingsLoadedEvent;
        public event AppSettingsChangeHandler AppSettingsChangeEvent;

        public Log()
        {
            AppShutDownEvent += Log_AppShutDownEvent;
        }

        private void Log_AppShutDownEvent()
        {
            PassOut();
        }

        public void AppSettingsLoaded()
        {
            SendMsg("App Settings Loaded", MessageType.General);
            if (AppSettingsLoadedEvent != null)
                Task.Run(() => AppSettingsLoadedEvent());
        }
        public void AppSettingsChange(string Property, object NewValue)
        {
            SendMsg("Setting '" + Property + "' has chagned to => " + NewValue.ToString(), MessageType.General);
            if (AppSettingsChangeEvent != null)
                Task.Run(() => AppSettingsChangeEvent(Property, NewValue));
        }

        public void SendMsg(string Message, MessageType MsgType)
        {
            if (SendMessageEvent != null)
                Task.Run(() => SendMessageEvent(Message, MsgType));
        }
        public void ConnectionStatus(string Status, long RoundTripTime)
        {
            SendMsg("Status: " + Status + " :: Trip Time: " + RoundTripTime.ToString(), MessageType.General);
            if (ConnectionStatusEvent != null)
                Task.Run(() => ConnectionStatusEvent(Status, RoundTripTime));
        }

        public void NewFileMsg(FileMsg file)
        {
            Logging.Log.Manager.SendMsg("New File Found: " + file.FileID + " :: " + file.FileName, Logging.MessageType.General);
            if (NewFileMsgEvent != null)
                Task.Run(() => NewFileMsgEvent(file));
        }
        public void ChangedFileMsg(FileMsg file)
        {
            Logging.Log.Manager.SendMsg("File change occured: " + file.FileName + " :: " + file.FileName, Logging.MessageType.Warning);
            if (ChangedFileMsgEvent != null)
                Task.Run(() => ChangedFileMsgEvent(file));
        }
        public void DeletedFileMsg(FileMsg file)
        {
            Logging.Log.Manager.SendMsg("File Deleted: " + file.FileName, Logging.MessageType.Warning);
            if (DeletedFileMsgEvent != null)
                Task.Run(() => DeletedFileMsgEvent(file));
        }
        public void FileTransferStarted(FileMsg File)
        {
            Logging.Log.Manager.SendMsg("File Transfer Started: " + File.FileName, Logging.MessageType.General);
            if (FileTransferStartedEvent != null)
                Task.Run(() => FileTransferStartedEvent(File));
        }
        public void FileTransferStopped(FileMsg File)
        {
            Logging.Log.Manager.SendMsg("File Transfer Stopped: " + File.FileName, Logging.MessageType.Warning);
            if (FileTransferStoppedEvent != null)
                Task.Run(() => FileTransferStoppedEvent(File));
        }
        public void FileTransferUpdate(FileMsg File)
        {
            Logging.Log.Manager.SendMsg("File Transfer: " + File.FileName + " " + ((float)File.Transfered/(float)File.Size).ToString("P1"), Logging.MessageType.General);
            if (FileTransferUpdateEvent != null)
                Task.Run(() => FileTransferUpdateEvent(File));
        }
        public void FileTransferCompleted(FileMsg File)
        {
            Logging.Log.Manager.SendMsg("File Transfer Completed: " + File.FileName, Logging.MessageType.General);
            if (FileTransferCompletedEvent != null)
                Task.Run(() => FileTransferCompletedEvent(File));
        }
        public void FileTransferError(FileMsg File)
        {
            Logging.Log.Manager.SendMsg("File Transfer Error: " + File.FileName, Logging.MessageType.Error);
            if (FileTransferErrorEvent != null)
                Task.Run(() => FileTransferErrorEvent(File));
        }
        public void FileTransferVerify(string FileID)
        {
            Logging.Log.Manager.SendMsg("File Transfer Verify File ID: " + FileID, Logging.MessageType.Error);
            if (FileTransferVerifyEvent != null)
                Task.Run(() => FileTransferVerifyEvent(FileID));
        }

        public void AppStartingUp()
        {
            Logging.Log.Manager.SendMsg("App starting up", Logging.MessageType.General);
            if (AppStartingUpEvent != null)
                AppStartingUpEvent();
        }
        public void AppStarted()
        {
            Logging.Log.Manager.SendMsg("App has started", Logging.MessageType.General);
            if (AppStartedEvent != null)
                AppStartedEvent();
        }
        public bool AppShuttingDown(bool ClearedToShutDown=true)
        {
            Logging.Log.Manager.SendMsg("App is shutting down", Logging.MessageType.General);
            if (AppShuttingDownEvent != null)
                return AppShuttingDownEvent(ClearedToShutDown);
            return true;
        }
        public void AppShutdown()
        {
            Logging.Log.Manager.SendMsg("App has shut down", Logging.MessageType.General);
            if (AppShutDownEvent != null)
                AppShutDownEvent();
        }
    }
}

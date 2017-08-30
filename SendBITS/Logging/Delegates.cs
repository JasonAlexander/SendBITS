using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS.Logging
{
    public delegate void ConnectionStatusHandler(string Status, long RoundTripTime);
    public delegate void SendMessageHandler(string Message, MessageType MsgType);

    public delegate void NewFileMsgHandler(FileMsg File);
    public delegate void ChangedFileMsgHandler(FileMsg File);
    public delegate void DeletedFileMsgHandler(FileMsg File);
    public delegate void FileTransferStartedHandler(FileMsg File);
    public delegate void FileTransferStoppedHandler(FileMsg File);
    public delegate void FileTransferUpdateHandler(FileMsg File);
    public delegate void FileTransferCompletedHander(FileMsg File);
    public delegate void FileTransferErrorHander(FileMsg File);
    public delegate void FileTransferVerifyHandler(string FileID);

    public delegate void AppStartingUpHandler();
    public delegate void AppStartedHandler();
    public delegate bool AppShuttingDownHandler(bool ClearedToShutdown);
    public delegate void AppShutDownHandler();

    public delegate void AppSettingsLoadedHandler();
    public delegate void AppSettingsChangeHandler(string Property, object NewValue);

    public enum MessageType
    {
        General,
        Error,
        Warning,
        Apocalypse
    }
}

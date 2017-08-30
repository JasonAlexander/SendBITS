using SendBITS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS.Comm
{
    public class MonitorService : IRegisterMonitor
    {
        public Task RegisterForSendBITSUpdatesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

//FileTransferStartedHandler(FileMsg File);
//FileTransferStoppedHandler(FileMsg File);
//FileTransferUpdateHandler(FileMsg File);
//FileTransferCompletedHander(FileMsg File);
//FileTransferErrorHander(FileMsg File);
//FileTransferVerifyHandler(string FileID);

//Log.Manager.FileTransferCompletedEvent += Manager_FileTransferCompletedEvent;
//Log.Manager.FileTransferErrorEvent += Manager_FileTransferErrorEvent;
//Log.Manager.FileTransferStartedEvent += Manager_FileTransferStartedEvent;
//Log.Manager.FileTransferStoppedEvent += Manager_FileTransferStoppedEvent;
//Log.Manager.FileTransferUpdateEvent += Manager_FileTransferUpdateEvent;
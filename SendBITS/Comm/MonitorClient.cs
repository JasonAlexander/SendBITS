using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendBITS.Logging;

namespace SendBITS.Comm
{
    public class MonitorClient : IMonitorUpdates
    {
        public MonitorClient() { }

        public void FileTransferStarted(FileMsg File)
        {
            throw new NotImplementedException();
        }

        public Task<string> RecieveUpdateAsync(string update)
        {
            throw new NotImplementedException();
        }
    }
}

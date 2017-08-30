using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendBITS.Logging;

namespace SendBITS.Comm
{
    public class TCPClient : ITCPClient
    {
        #region ITCPClient
        public void FileTransferCompleted(FileMsg File)
        {
        }

        public void FileTransferError(FileMsg File)
        {
        }

        public void FileTransferStarted(FileMsg File)
        {
        }

        public void FileTransferStopped(FileMsg File)
        {
        }

        public void FileTransferUpdate(FileMsg File)
        {
        }

        public void FileTransferVerify(string FileID)
        {
        }
        #endregion
    }

    public interface ITCPClient
    {
        void FileTransferStarted(FileMsg File);
        void FileTransferStopped(FileMsg File);
        void FileTransferUpdate(FileMsg File);
        void FileTransferCompleted(FileMsg File);
        void FileTransferError(FileMsg File);
        void FileTransferVerify(string FileID);
    }
}

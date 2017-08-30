using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendBITS.Comm
{
    public class TCPServer
    {
        private TcpListener listner;
        private List<TcpClient> clients;

        public TCPServer()
        {

        }

        public ManualResetEvent ResetServer { get; set; }
    }

    public interface ITCPServer
    {
        void WaitForConnections();
        void ReplyToNewConnection();
    }
}

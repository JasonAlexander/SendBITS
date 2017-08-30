using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendBITS.Comm
{
    public sealed class Comm : ManagerBase<Comm>
    {
        private TcpListener tcpServer;
        private TcpClient tcpClient;
        private Ping conMonitor;
        private byte[] buffer;
        private int timeout;
        private int wait;
        private PingOptions pingOptions;
        private bool halt;
        private string host;
        public Comm()
        {
            Logging.Log.Manager.AppSettingsChangeEvent += Manager_AppSettingsChangeEvent;
            Logging.Log.Manager.AppStartingUpEvent += Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent += Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent += Manager_AppShuttingDownEvent;
            Logging.Log.Manager.AppShutDownEvent += Manager_AppShutDownEvent;
        }

        private void Manager_AppSettingsChangeEvent(string Property, object NewValue)
        {
            if (Property.StartsWith("Con"))
                SetupConnectionMonitor();
        }

        private void Manager_AppStartedEvent()
        {
            SetupConnectionMonitor();
            Task.Factory.StartNew(RunConMonitor, TaskCreationOptions.LongRunning);
        }
        private void Manager_AppStartingUpEvent()
        {
        }
        private bool Manager_AppShuttingDownEvent(bool ClearedToShutdown)
        {
            Logging.Log.Manager.AppSettingsChangeEvent -= Manager_AppSettingsChangeEvent;
            Logging.Log.Manager.AppStartingUpEvent -= Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent -= Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent -= Manager_AppShuttingDownEvent;
            halt = true;
            return true;
        }
        private void Manager_AppShutDownEvent()
        {
            PassOut();
        }

        private void SetupConnectionMonitor()
        {
            string bytes = "qazwsxedcrfvtgbyhnujmikolp123456";
            conMonitor = new Ping();
            conMonitor.PingCompleted += ConMonitor_PingCompleted;
            buffer = Encoding.ASCII.GetBytes(bytes);
            timeout = AppManager.Settings.ConMonTimeout;
            wait = AppManager.Settings.ConMonWait;
            pingOptions = new PingOptions(AppManager.Settings.ConMonHops, true);
            host = AppManager.Settings.ConMonHost;
            string setup = "Connection Monitor Setup\r\n" +
                           "Host: " + host + "\r\n" +
                           "Buffer: " + bytes + "\r\n" +
                           "Timeout: " + timeout.ToString() + "\r\n" +
                           "Wait Time: " + wait.ToString() + "\r\n";
            Logging.Log.Manager.SendMsg(setup, Logging.MessageType.General);
        }
        private void ConMonitor_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Logging.Log.Manager.ConnectionStatus("Cancelled", -1);
            }
            else if (e.Error != null)
            {
                Logging.Log.Manager.ConnectionStatus("Error", -2);
            }
            else
            {
                Logging.Log.Manager.ConnectionStatus(e.Reply.Status.ToString(), e.Reply.RoundtripTime);
            }
            ((AutoResetEvent)e.UserState).Set();
        }
        private void RunConMonitor()
        {
            AutoResetEvent waiter = new AutoResetEvent(false);
            halt = false;
            while (!halt)
            {
                conMonitor.SendAsync(host, timeout, buffer, pingOptions, waiter);
                waiter.WaitOne();
                waiter.Reset();
                Thread.Sleep(wait);
            }
            waiter.Close();
        }
    }
}

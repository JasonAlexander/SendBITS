using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendBITS.UI
{
    public sealed class UI : ManagerBase<UI>
    {
        public UI()
        {
            Logging.Log.Manager.AppStartingUpEvent += Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent += Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent += Manager_AppShuttingDownEvent;
            Logging.Log.Manager.AppShutDownEvent += Manager_AppShutDownEvent;
        }

        public void StartUI()
        {
            if (AppManager.Settings.UseWinForms)
            {
                Thread t = new Thread(new ThreadStart(RunWinforms));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
            }
            else
            {
                AppWPF.Current.Startup += Current_Startup;
                AppWPF.Current.Exit += Current_Exit;
                AppWPF.Main();
            }
        }

        private void RunWinforms()
        {
            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainWindow());
        }

        private void Manager_AppShutDownEvent()
        {
            PassOut();
        }

        private bool Manager_AppShuttingDownEvent(bool ClearedToShutdown)
        {
            Logging.Log.Manager.AppStartingUpEvent -= Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent -= Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent -= Manager_AppShuttingDownEvent;
            return true;
        }

        private async void Manager_AppStartedEvent()
        {
            await Task.Run(() => StartUI());
            Logging.Log.Manager.AppShutdown();
        }

        private void Manager_AppStartingUpEvent()
        {
        }

        private void Current_Startup(object sender, System.Windows.StartupEventArgs e)
        {
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Logging.Log.Manager.AppShuttingDown();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            //not how the application should be signaled to end
            //prefer to have Manager_AppStartedEvent completely
            //return and then call shut down methods
            Logging.Log.Manager.AppShuttingDown();
        }
    }
}
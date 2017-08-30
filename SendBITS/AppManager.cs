using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace SendBITS
{
    public class AppManager
    {
        public static void Manage(string[] args)
        {
            IsRunning();
            if (!UI.AppConsole.ParseArgs(args)) return;
            WakeUp();
            MembersAwakeCount.Wait();
        }
        private static void WakeUp()
        {
            MembersAwakeCount = new CountdownEvent(1);

            //Wake up all objects and set event hooks
            Logging.Log.Manager.WakeUp();
            FileIO.FileIO.Manager.WakeUp();
            Comm.Comm.Manager.WakeUp();
            UI.UI.Manager.WakeUp();
            UI.AppConsole.Manager.WakeUp();
            BITS.BITS.Manager.WakeUp();

            Logging.Log.Manager.AppStartingUp();
            MembersAwakeCount.Signal();
            Logging.Log.Manager.AppStarted();
        }
        public static CountdownEvent MembersAwakeCount { get; set; }
        public static AppSettings Settings { get; set; }
        private static bool IsRunning()
        {//Add code to switch to currently running process and kill this one
            var me = Process.GetCurrentProcess();
            var arrProcesses = Process.GetProcessesByName(me.ProcessName);

            if (arrProcesses.Length > 1)
            {
                return true;
            }

            return false;
        }
    }
}
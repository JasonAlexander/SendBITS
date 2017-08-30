using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SendBITS.Logging;

namespace SendBITS.UI
{
    public class AppConsole : ManagerBase<AppConsole>
    {//TODO: Change this class to handle Console args only
        public AppConsole()
        {
            AllocConsole();
            consoleExiting += new EventHandler(Handler);
            SetConsoleCtrlHandler(consoleExiting, true);
            Log.Manager.SendMessageEvent += Manager_SendMessageEvent;
            Log.Manager.AppSettingsChangeEvent += Manager_AppSettingsChangeEvent;
            Log.Manager.AppSettingsLoadedEvent += Manager_AppSettingsLoadedEvent;
            Log.Manager.AppStartingUpEvent += Manager_AppStartingUpEvent;
            Log.Manager.AppStartedEvent += Manager_AppStartedEvent;
            Log.Manager.AppShuttingDownEvent += Manager_AppShuttingDownEvent;
            Log.Manager.AppShutDownEvent += Manager_AppShutDownEvent;
        }

        private void Manager_AppShutDownEvent()
        {
            PassOut();
            FreeConsole();
        }
        private bool Manager_AppShuttingDownEvent(bool ClearedToShutdown)
        {
            Log.Manager.SendMessageEvent -= Manager_SendMessageEvent;
            Log.Manager.AppSettingsChangeEvent -= Manager_AppSettingsChangeEvent;
            Log.Manager.AppSettingsLoadedEvent -= Manager_AppSettingsLoadedEvent;
            Log.Manager.AppStartingUpEvent -= Manager_AppStartingUpEvent;
            Log.Manager.AppStartedEvent -= Manager_AppStartedEvent;
            Log.Manager.AppShuttingDownEvent -= Manager_AppShuttingDownEvent;
            return true;
        }
        private void Manager_AppStartedEvent()
        {
        }
        private void Manager_AppStartingUpEvent()
        {
            if (AppManager.Settings != null)
                ConsoleShowState(AppManager.Settings.ShowConsole);
        }

        private void Manager_AppSettingsLoadedEvent()
        {
            if (AppManager.Settings != null)
                ConsoleShowState(AppManager.Settings.ShowConsole);
        }
        private void Manager_AppSettingsChangeEvent(string Property, object NewValue)
        {
            if (Property == "ShowConsole")
                ConsoleShowState((bool)NewValue);
        }
        private void Manager_SendMessageEvent(string Message, MessageType MsgType)
        {
            switch(MsgType)
            {
                case MessageType.General:
                    Console.WriteLine(Message);
                    break;
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Message);
                    Console.ResetColor();
                    break;
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Message);
                    Console.ResetColor();
                    break;
                case MessageType.Apocalypse:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Message);
                    Console.ResetColor();
                    break;
                default:
                    break;
            }
        }

        public static bool ParseArgs(string[] args)
        {
            if (args.Length > 0)
            {
                bool interactive = false;
                AppConsole.Manager.ConsoleShowState(true);
                Console.Write("Enter SendBITS Command> ");
                if (args.Contains("-cs"))
                {
                    interactive = AppConsole.Manager.CreateNewSettingsFile();
                }
                else if(args.Contains("-i"))
                {
                    Console.Write("-i");
                    Console.WriteLine();
                    AppConsole.Manager.Interactive();
                }
                else if(args.Contains("-cj"))
                {
                    Console.WriteLine(BITS.BITS.ClearAllMSBITSJobs());
                }
                Console.WriteLine();
                if (interactive)
                    AppConsole.Manager.Interactive();
                AppConsole.Manager.ConsoleShowState(false);
                return false;
            }
            return true;
        }
        private void Interactive()
        {
            Console.Write("Enter SendBITS Command> ");
            var s = Console.ReadLine();
            while (s != "exit")
            {
                Console.Write("Enter SendBITS Command> ");
                s = Console.ReadLine();
            }
        }
        private bool CreateNewSettingsFile()
        {
            Console.Write("cs");
            Console.WriteLine();
            Console.WriteLine("Create New Settings File? y/n");
            if (Console.ReadKey().KeyChar == 'y')
            {
                Console.WriteLine();
                FileIO.FileIO.Manager.GetAppSettings();
                FileIO.FileIO.Manager.SaveAppSettings();
                Console.WriteLine("-------------------------");
                Console.WriteLine("New Settings file created");
                Console.WriteLine("-------------------------");
                Console.WriteLine(FileIO.FileIO.Manager.SettingsJSON);
                Console.Write("Press any key to exit or 'i' for interactive command line.");
                if (Console.ReadKey().KeyChar == 'i')
                    return true;
            }
            return false;
        }
        
        private enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        private static bool Handler(CtrlType sig)
        {
            Logging.Log.Manager.AppShutdown();
            return true;
        }
        private delegate bool EventHandler(CtrlType sig);
        private static EventHandler consoleExiting;
        private void ConsoleShowState(bool Show)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, (Show ? SW_SHOW : SW_HIDE));
        }
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
    }
}

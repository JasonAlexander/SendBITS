using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendBITS.Logging;
using System.Runtime.InteropServices;

namespace SendBITS.FileIO
{
    public class FileIO : ManagerBase<FileIO>
    {
        private Dictionary<string, string> filesFound;
        private FileMsg file;
        private string settingsJSON;
        private Random ran;
        FileSystemWatcher watcher;
        public FileIO()
        {
            filesFound = new Dictionary<string, string>();
            var seed = 1776 - DateTime.Now.Hour + DateTime.Now.Minute - DateTime.Now.Second + DateTime.Now.Day;
            ran = new Random(seed);
            Logging.Log.Manager.AppStartingUpEvent += Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent += Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent += Manager_AppShuttingDownEvent;
            Logging.Log.Manager.AppShutDownEvent += Manager_AppShutDownEvent;

            Logging.Log.Manager.AppSettingsChangeEvent += Manager_AppSettingsChangeEvent;
            Logging.Log.Manager.AppSettingsLoadedEvent += Manager_AppSettingsLoadedEvent;
        }

        private void Manager_AppSettingsLoadedEvent()
        {
        }
        private void Manager_AppSettingsChangeEvent(string Property, object NewValue)
        {
            if(Property == "WatchFolder")
            {
                StopWatchers();
                StartWatchers();
            }
        }

        private void Manager_AppShutDownEvent()
        {
            SaveAppSettings();
            PassOut();
        }
        private void Manager_AppStartedEvent()
        {
            StartWatchers();
        }
        private bool Manager_AppShuttingDownEvent(bool ClearedToShutdown)
        {
            Logging.Log.Manager.AppStartingUpEvent -= Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent -= Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent -= Manager_AppShuttingDownEvent;
            Logging.Log.Manager.AppSettingsChangeEvent -= Manager_AppSettingsChangeEvent;
            Logging.Log.Manager.AppSettingsLoadedEvent -= Manager_AppSettingsLoadedEvent;

            StopWatchers();
            return true;
        }
        private void Manager_AppStartingUpEvent()
        {
            GetAppSettings();
        }

        public string GetNewUID()
        {
            ran.Next();
            var ticks = new DateTime(2017, 1, 1).Ticks;
            return (DateTime.Now.Ticks - ticks + ran.Next()).ToString("x").Substring(4).ToUpper();
        }
        public string SettingsJSON { get { return settingsJSON; } }

        private string SettingsPath
        {
            get
            {
                string file = Directory.GetCurrentDirectory() + "\\SendBITS.settings";
                if (!File.Exists(file))
                {//file isn't there, make one with empty JSON { 0x7B, 0x7D } -- file will then be loaded with default values
                    using (FileStream f = File.Create(file))
                    {
                        f.Write(new byte[] { 123, 125 }, 0, 0);
                        f.Close();
                    }
                }
                return file;
            }
        }
        public void GetAppSettings()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(SettingsPath);
                settingsJSON = reader.ReadToEnd();
                AppManager.Settings = JsonConvert.DeserializeObject<AppSettings>(settingsJSON,
                                                                                new JsonSerializerSettings
                                                                                { DefaultValueHandling = DefaultValueHandling.Populate,
                                                                                  NullValueHandling = NullValueHandling.Ignore});
                if (AppManager.Settings == null)
                    AppManager.Settings = new AppSettings();
            }
            catch (Exception)
            {
                //send ex to log
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                Log.Manager.AppSettingsLoaded();
            }
        }
        public bool SaveAppSettings()
        {
            bool completed;
            TextWriter writer = null;
            try
            {
                settingsJSON = JsonConvert.SerializeObject(AppManager.Settings, 
                                                           Formatting.Indented, 
                                                           new JsonSerializerSettings
                                                           { DefaultValueHandling = DefaultValueHandling.Include,
                                                             NullValueHandling = NullValueHandling.Ignore});
                writer = new StreamWriter(SettingsPath, false);
                writer.Write(settingsJSON);
                completed = true;
            }
            catch(Exception)
            {
                //send ex to log
                completed = false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return completed;
        }

        private void StartWatchers()
        {//TODO: Might set an option to load all files currently found in folder at startup. Need feedback on this.
            watcher = new FileSystemWatcher(AppManager.Settings.WatchFolder);
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.IncludeSubdirectories = false;
            watcher.InternalBufferSize *= 2;
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            watcher.Error += Watcher_Error;
            watcher.Renamed += Watcher_Renamed;
            watcher.Deleted += Watcher_Deleted;
            watcher.EnableRaisingEvents = true;
        }
        private void StopWatchers()
        {
            watcher.EnableRaisingEvents = false;
        }

        private void FileCreated(FileInfo fi)
        {
            file = new FileMsg();
            file.FileName = fi.Name;
            file.Folder = fi.DirectoryName;
            file.Created = fi.CreationTime;
            file.FileID = GetNewUID();
            file.Size = fi.Length;
            file.Status = "New";
            filesFound.Add(file.FileName, file.FileID);
            Log.Manager.NewFileMsg(file);
        }
        private void FileChanged(FileInfo fi)
        {
            //if (file.FileName != fi.Name )
            //{

            //    file = new FileMsg();
            //    file.FileName = fi.Name;
            //    file.Folder = fi.DirectoryName;
            //    file.Created = fi.CreationTime;
            //    file.FileID = GetNewUID();
            //    file.Size = fi.Length;
            //    file.Status = "Changed";
            //    Log.Manager.NewFileMsg(file);
            //}
            //else if(file.Size != fi.Length)
            //{
            //    file.Size = fi.Length;
            //    Log.Manager.NewFileMsg(file);
            //}
            //else
            //{

            //}
        }
        private void FileDeleted(FileInfo fi)
        {
            if (file != null)
            {
                if (filesFound.ContainsKey(fi.Name))
                {
                    file.Status = "Deleted";
                    file.FileID = filesFound[fi.Name];
                    filesFound.Remove(fi.Name);
                    Logging.Log.Manager.DeletedFileMsg(file);
                }
                else
                {
                    FileMsg dfile = new FileMsg();
                    dfile.FileName = fi.Name;
                    dfile.Folder = fi.DirectoryName;
                    dfile.Created = fi.CreationTime;
                    dfile.FileID = filesFound[fi.Name];
                    dfile.Size = fi.Length;
                    dfile.Status = "Deleted";
                    filesFound.Remove(fi.Name);
                    Logging.Log.Manager.DeletedFileMsg(dfile);
                }
            }
            else
            {
                FileMsg dfile = new FileMsg();
                dfile.FileName = fi.Name;
                dfile.Folder = fi.DirectoryName;
                dfile.Created = fi.CreationTime;
                dfile.FileID = string.Empty;
                //dfile.Size = fi.Length;
                dfile.Status = "Deleted";
                Logging.Log.Manager.DeletedFileMsg(dfile);
            }
        }
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            FileDeleted(new FileInfo(e.FullPath));
        }
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
        }
        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
        }
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            FileCreated(new FileInfo(e.FullPath));
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            FileChanged(new FileInfo(e.FullPath));
        }
    }
}

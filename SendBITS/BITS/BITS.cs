using SendBITS.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSBITS = Microsoft.BackgroundIntelligentTransfer.Management.Interop;

namespace SendBITS.BITS
{
    public sealed class BITS : ManagerBase<BITS>
    {
        private object lockingObject;
        private bool halt;
        private string remoteHost;
        private string remoteFolder;
        private Queue<Guid> jobsQueue;
        private MSBITS.Log bLog;
        private MSBITS.Manager bitsManager;
        public BITS()
        {
            lockingObject = new object();
            jobsQueue = new Queue<Guid>();
            FileQueue = new ConcurrentQueue<FileMsg>();
            WorkingSet = new ConcurrentDictionary<Guid, FileMsg>();            
            Logging.Log.Manager.NewFileMsgEvent += Manager_NewFileMsgEvent;
            Logging.Log.Manager.FileTransferVerifyEvent += Manager_FileTransferVerifyEvent;
            Logging.Log.Manager.ChangedFileMsgEvent += Manager_ChangedFileMsgEvent;
            Logging.Log.Manager.DeletedFileMsgEvent += Manager_DeletedFileMsgEvent;
            Logging.Log.Manager.AppStartingUpEvent += Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent += Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent += Manager_AppShuttingDownEvent;
            Logging.Log.Manager.AppShutDownEvent += Manager_AppShutDownEvent;            
        }

        private void Manager_AppShutDownEvent()
        {
            PassOut();
        }
        private bool Manager_AppShuttingDownEvent(bool ClearedToShutdown)
        {
            Logging.Log.Manager.NewFileMsgEvent -= Manager_NewFileMsgEvent;
            Logging.Log.Manager.FileTransferVerifyEvent -= Manager_FileTransferVerifyEvent;
            Logging.Log.Manager.ChangedFileMsgEvent -= Manager_ChangedFileMsgEvent;
            Logging.Log.Manager.DeletedFileMsgEvent -= Manager_DeletedFileMsgEvent;
            Logging.Log.Manager.AppStartingUpEvent -= Manager_AppStartingUpEvent;
            Logging.Log.Manager.AppStartedEvent -= Manager_AppStartedEvent;
            Logging.Log.Manager.AppShuttingDownEvent -= Manager_AppShuttingDownEvent;
            halt = true;
            return true;
        }
        private void Manager_AppStartedEvent()
        {
            StartMSBITS();
        }
        private void Manager_AppStartingUpEvent()
        {
        }

        private void StartMSBITS()
        {
            if (AppManager.Settings.UseMSBitsLog)
                MSBITS.Log.Initialize("MSBITSLOG.txt");
            else
                MSBITS.Log.Initialize();
            bitsManager = new MSBITS.Manager();
            halt = false;
            remoteHost = AppManager.Settings.CopyToHost;
            remoteFolder = AppManager.Settings.CopyToFolder;
            CheckCurrentJobs();
            Task.Run(() => RunRITS());
        }
        private void CheckCurrentJobs()
        {   //TODO: Pipe left over jobs into framework
            //Not the correct way to handle left over jobs
            //This does not follow the framework of this app
            //But it gets the job done for the time being
            //Need to code this to fit into the framework
            //Undead dragons are showing up in GoT.
            bitsManager
                .AllJobs
                .Where(x => x.State == MSBITS.JobState.Suspended && 
                            x.FileList.Count == 0)
                .ToList()
                .ForEach(x => x.Cancel());
            bitsManager
                .AllJobs
                .Where(x => x.Description.Contains("SendBITS") &&
                            x.State == MSBITS.JobState.Suspended && 
                            x.FileList.Count > 0)
                .ToList()
                .ForEach(x => AddRunningJobsAtStartup(x.Id));
            Log.Manager.SendMsg("Current number of SendBITS Jobs in BITS: " + 
                bitsManager
                .AllJobs
                .Count(x => x.Description.Contains("SendBITS"))
                .ToString(), 
                MessageType.Error);
            bitsManager
                .AllJobs
                .ToList()
                .ForEach(x => 
                { Log.Manager.SendMsg("Name: " + x.DisplayName + 
                                      "\r\nDescription: " + x.Description + 
                                      "\r\nID: " + x.Id + 
                                      "\r\nFile Count: " + x.FileList.Count.ToString() +
                                      "\r\nState: " + x.State.ToString(), 
                                      MessageType.General); });
        }
        private void AddRunningJobsAtStartup(Guid JobID)
        {
            FileMsg file = new FileMsg();
            MSBITS.Job job = bitsManager.CreateJob(MSBITS.JobType.Upload);
            job.DisplayName = file.FileName;
            job.Description = "SendBITS " + DateTime.Now.Date.ToShortDateString();
            job.Priority = MSBITS.JobPriority.Foreground;
            job.ErrorEvent += Job_ErrorEvent;
            job.ModificationEvent += Job_ModificationEvent;
            job.TransferredEvent += Job_TransferredEvent;
            MSBITS.FileLocation fl = new MSBITS.FileLocation();
            fl.LocalName = file.FullPath;
            //not the right way to build this string
            fl.RemoteName = "\\\\" + remoteHost + remoteFolder + "\\" + file.FileName;
            fl.RemoteName = fl.RemoteName;
            file.RemotePath = fl.RemoteName;
            file.Status = "Working";
            file.Started = DateTime.Now;
            file.JobID = job.Id;
            file.FileID = job.Id.ToString();
            WorkingSet.TryAdd(job.Id, file);
            try
            {
                job.AddFile(fl);
                //job.Resume();
                jobsQueue.Enqueue(job.Id);
            }
            catch (Exception ex)
            {
                Log.Manager.SendMsg(ex.Message, MessageType.Error);
            }
        }
        private void RunRITS()
        {
            while(!halt)
            {
                CreateNewJob();
                CheckJobQueue();
                Task.Run(() => VerifyFiles());
                Thread.Sleep(500);
            }
        }
        private void CreateNewJob()
        {
            FileMsg file;
            if (FileQueue.TryDequeue(out file))
            {
                if (file.Status != "Deleted")
                {
                    MSBITS.Job job = bitsManager.CreateJob(MSBITS.JobType.Upload);
                    job.DisplayName = file.FileName;
                    job.Description = "SendBITS " + DateTime.Now.Date.ToShortDateString();
                    job.Priority = MSBITS.JobPriority.Foreground;
                    job.ErrorEvent += Job_ErrorEvent;
                    job.ModificationEvent += Job_ModificationEvent;
                    job.TransferredEvent += Job_TransferredEvent;
                    MSBITS.FileLocation fl = new MSBITS.FileLocation();
                    fl.LocalName = file.FullPath;
                    //not the right way to build this string
                    fl.RemoteName = "\\\\" + remoteHost + remoteFolder + "\\" + file.FileName;
                    fl.RemoteName = fl.RemoteName;
                    file.RemotePath = fl.RemoteName;
                    file.Status = "Working";
                    file.Started = DateTime.Now;
                    file.JobID = job.Id;
                    WorkingSet.TryAdd(job.Id, file);
                    try
                    {
                        job.AddFile(fl);
                        //job.Resume();
                        jobsQueue.Enqueue(job.Id);
                    }
                    catch (Exception ex)
                    {
                        Log.Manager.SendMsg(ex.Message, MessageType.Error);
                    }
                }
            }
        }
        private void CheckJobQueue()
        {//TODO: add option for the count
            if (bitsManager
                .AllJobs
                .Where(x => 
                x.State == MSBITS.JobState.Transferring)
                .Count() <= 25)
            {
                if (jobsQueue.Count > 0)
                {
                    Guid guid = jobsQueue.Dequeue();
                    bitsManager.AllJobs.Single(x => x.Id == guid).Resume();
                }
            }
        }
        private void VerifyFiles()
        {
            var verify = WorkingSet.Where(x => ((x.Value.Status != "Verified") && 
                                                (x.Value.Transfered == x.Value.Size)));
            foreach(var file in verify)
            {
                if (File.Exists(file.Value.RemotePath))
                {
                    WorkingSet[file.Key].Status = "Verified";
                    Log.Manager.FileTransferUpdate(WorkingSet[file.Key]);
                }
            }
        }
        private void VerifyFile(string FileID)
        {
            VerifyFile(WorkingSet.Values.Single(x => x.FileID == FileID).JobID);
        }
        private void VerifyFile(Guid jobID)
        {
            if (bitsManager.AllJobs.Where(x => x.Id == jobID).Count() > 0)
            {
                var job = bitsManager.GetJob(jobID);
                WorkingSet[jobID].Status = job.State.ToString();
                WorkingSet[jobID].Transfered = job.Progress.BytesTransferred;
            }
            else
            {
                //Assuming that the job actually did complete
                //Might add a verify step that does a FileInfo on the remote host
                WorkingSet[jobID].Status = "Verified";
                WorkingSet[jobID].Transfered = WorkingSet[jobID].Size;
            }
            Log.Manager.FileTransferUpdate(WorkingSet[jobID]);
        }

        private void Job_TransferredEvent(object sender, MSBITS.TransferredEventArgs e)
        {
            lock(lockingObject)
            {
                WorkingSet[e.JobForEvent.Id].Status = e.JobForEvent.State.ToString();
                WorkingSet[e.JobForEvent.Id].Transfered = e.JobForEvent.Progress.BytesTransferred;
                switch (e.JobForEvent.State)
                {
                    case MSBITS.JobState.Acknowledged:
                        Log.Manager.FileTransferCompleted(WorkingSet[e.JobForEvent.Id]);
                        //e.JobForEvent.Complete();
                        break;
                    case MSBITS.JobState.Canceled:
                        break;
                    case MSBITS.JobState.Connecting:
                        break;
                    case MSBITS.JobState.Error:
                        break;
                    case MSBITS.JobState.Queued:
                        break;
                    case MSBITS.JobState.Suspended:
                        break;
                    case MSBITS.JobState.Transferred:
                        Log.Manager.FileTransferCompleted(WorkingSet[e.JobForEvent.Id]);
                        e.JobForEvent.Complete();
                        break;
                    case MSBITS.JobState.Transferring:
                        Log.Manager.FileTransferUpdate(WorkingSet[e.JobForEvent.Id]);
                        break;
                    case MSBITS.JobState.TransientError:
                        break;
                }
                Log.Manager.SendMsg("Transfer Event: " + e.JobForEvent.State.ToString(), MessageType.Warning);
            }
        }
        private void Job_ModificationEvent(object sender, MSBITS.ModificationEventArgs e)
        {
            lock (lockingObject)
            {
                if (WorkingSet.ContainsKey(e.JobForEvent.Id))
                {
                    WorkingSet[e.JobForEvent.Id].Status = e.JobForEvent.State.ToString();
                    WorkingSet[e.JobForEvent.Id].Transfered = e.JobForEvent.Progress.BytesTransferred;
                    switch (e.JobForEvent.State)
                    {
                        case MSBITS.JobState.Acknowledged:
                            Log.Manager.FileTransferCompleted(WorkingSet[e.JobForEvent.Id]);
                            //e.JobForEvent.Complete();
                            break;
                        case MSBITS.JobState.Canceled:
                            break;
                        case MSBITS.JobState.Connecting:
                            break;
                        case MSBITS.JobState.Error:
                            Log.Manager.FileTransferCompleted(WorkingSet[e.JobForEvent.Id]);
                            break;
                        case MSBITS.JobState.Queued:
                            break;
                        case MSBITS.JobState.Suspended:
                            break;
                        case MSBITS.JobState.Transferred:
                            Log.Manager.FileTransferCompleted(WorkingSet[e.JobForEvent.Id]);
                            e.JobForEvent.Complete();
                            break;
                        case MSBITS.JobState.Transferring:
                            Log.Manager.FileTransferUpdate(WorkingSet[e.JobForEvent.Id]);
                            break;
                        case MSBITS.JobState.TransientError:
                            break;
                    }
                }
                Log.Manager.SendMsg("Modification Event: " + e.JobForEvent.State.ToString(), MessageType.Warning);
            }
        }
        private void Job_ErrorEvent(object sender, MSBITS.ErrorEventArgs e)
        {
            lock (lockingObject)
            {
                Log.Manager.SendMsg("Error Event: " + e.Error.Description, MessageType.Error);
                WorkingSet[e.JobForEvent.Id].Status = e.Error.Description;
                Log.Manager.FileTransferError(WorkingSet[e.JobForEvent.Id]);
            }
        }

        public ConcurrentQueue<FileMsg> FileQueue { get; set; }
        public ConcurrentDictionary<Guid, FileMsg> WorkingSet { get; set; }
        public List<FileMsg> TransferringFiles { get; set; }
        private void Manager_NewFileMsgEvent(FileMsg file)
        {
            FileQueue.Enqueue(file);            
        }
        private void Manager_ChangedFileMsgEvent(FileMsg File)
        {
            FileQueue.Where(x => x.FileName == File.FileName).Select(x => x.Status = "Changed");
        }
        private void Manager_DeletedFileMsgEvent(FileMsg File)
        {
            FileQueue.Where(x => x.FileName == File.FileName).Select(x => x.Status = "Deleted");
        }
        private void Manager_FileTransferVerifyEvent(string FileID)
        {
            VerifyFile(FileID);
        }
        public static string ClearAllMSBITSJobs()
        {
            MSBITS.Log.Initialize();
            MSBITS.Manager bitsManager = new MSBITS.Manager();
            bitsManager.AllJobs.Where(y => y.Description.Contains("SendBITS")).ToList().ForEach(x => x.Cancel());
            //bitsManager.AllJobs.ToList().ForEach(x => x.Cancel());
            return "All jobs cleared";
        }
    }
}

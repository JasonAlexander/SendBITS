using SendBITS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS.Comm
{
    [ServiceContract(ConfigurationName = "IRegisterMonitor",
                 CallbackContract = typeof(IMonitorUpdates),
                 Namespace = "SendBITS.Comm")]
    public interface IRegisterMonitor
    {
        [OperationContract(Action = "http://tempuri.org/IRegisterMonitor/RegisterForSendBITSUpdates",
                           ReplyAction = "http://tempuri.org/IRegisterMonitor/RegisterForSendBITSUpdatesResponse")]
        Task RegisterForSendBITSUpdatesAsync();
    }

    [ServiceContract]
    public interface IMonitorUpdates
    {
        [OperationContract(IsOneWay = true,
                           Action = "http://tempuri.org/IMonitorUpdates/RecieveUpdate")]
        Task<string> RecieveUpdateAsync(string update);
        [OperationContract(IsOneWay = true,
                           Action = "http://tempuri.org/IMonitorUpdates/FileTransferStarted")]
        void FileTransferStarted(FileMsg File);
        //void FileTransferStopped(FileMsg File);
        //void FileTransferUpdate(FileMsg File);
        //void FileTransferCompleted(FileMsg File);
        //void FileTransferError(FileMsg File);
        //void FileTransferVerify(string FileID);
    }

    //[DataContract]
    //public class MonitorMessage
    //{
    //    [DataMember]
    //    public FileMsg FileUpdate { get; set; }
    //}
}

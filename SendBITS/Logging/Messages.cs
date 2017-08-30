using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS.Logging
{
    public class FileMsg
    {//565 bytes as new()
        public DateTime Created { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }
        public Guid JobID { get; set; }
        public string FileID { get; set; }
        public string RemotePath { get; set; }
        public string Status { get; set; }
        public long Transfered { get; set; }
        public long Size { get; set; }
        [JsonIgnore]
        public string Percent
        {
            get
            {
                if (Transfered > 0)
                    return ((Convert.ToDouble(Transfered) / Convert.ToDouble(Size))).ToString("P1");
                else
                    return "0";
            }
        } 
        [JsonIgnore]
        public string FullPath { get { return Folder + "\\" + FileName; } }      
    }
}

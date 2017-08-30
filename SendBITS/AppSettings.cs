using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS
{
    public class AppSettings
    {
        public AppSettings()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var d = prop.GetCustomAttribute<DefaultValueAttribute>();
                if (d != null)
                    prop.SetValue(this, d.Value);
            }
        }

        #region Connection Monitor
        [DefaultValue("127.0.0.1")]
        public string ConMonHost { get; set; }
        [DefaultValue(5000)]
        public int ConMonTimeout { get; set; }
        [DefaultValue(64)]
        public int ConMonHops { get; set; }
        [DefaultValue(2500)]
        public int ConMonWait { get; set; }
        #endregion
        
        #region UI Options
        [DefaultValue(true)]
        public bool UseWinForms { get; set; }
        [DefaultValue(false)]
        public bool ShowConsole { get; set; }
        #endregion

        #region Log
        [DefaultValue(4322)]
        public int LogPort { get; set; }
        [DefaultValue(true)]
        public bool AllowRemoteMonitor { get; set; }
        #endregion

        #region Remote Monitor
        [DefaultValue(false)]
        public bool MonitorMode { get; set; }
        [DefaultValue(4321)]
        public int MonitorPort { get; set; }
        [DefaultValue("127.0.0.1")]
        public string MonitorHost { get; set; }//10.0.2.15 -- Host that is running SendBITS
        #endregion

        #region BITS Options
        [DefaultValue("Temp")]
        public string WatchFolder { get; set; }
        [DefaultValue("Temp")]
        public string CopyToFolder { get; set; }
        [DefaultValue("127.0.0.1")]
        public string CopyToHost { get; set; } //Host that SendBITS is copying to
        [DefaultValue(false)]
        public bool UseMSBitsLog { get; set; }
        #endregion

        public void UpdateSetting(string Setting, object NewValue)
        {
            PropertyInfo pi = this.GetType().GetProperty(Setting);
            if (pi.PropertyType == typeof(int))
            {
                int i = int.Parse(NewValue.ToString());
                if ((int)pi.GetValue(this) != i)
                {
                    pi.SetValue(this, i);
                    Task.Run(() => { Logging.Log.Manager.AppSettingsChange(Setting, NewValue); });
                }
            }
            else if (pi.PropertyType == typeof(string))
            {
                if ((string)pi.GetValue(this) != (string)NewValue)
                {
                    pi.SetValue(this, NewValue);
                    Task.Run(() => { Logging.Log.Manager.AppSettingsChange(Setting, NewValue); });
                }
            }
            else if (pi.PropertyType == typeof(bool))
            {
                if ((bool)pi.GetValue(this) != (bool)NewValue)
                {
                    pi.SetValue(this, NewValue);
                    Task.Run(() => { Logging.Log.Manager.AppSettingsChange(Setting, NewValue); });
                }
            }
        }
    }
}

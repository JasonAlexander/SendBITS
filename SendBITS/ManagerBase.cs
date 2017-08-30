using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendBITS
{
    public abstract class ManagerBase<T>
    {
        private static readonly Lazy<T> lazy = new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T)));

        public static T Manager { get { return lazy.Value; } }

        public virtual void WakeUp()
        {
            AppManager.MembersAwakeCount.AddCount();
        }        

        public virtual void PassOut()
        {
            AppManager.MembersAwakeCount.Signal();
        }
    }
}

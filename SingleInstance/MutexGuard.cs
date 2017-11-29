using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;

namespace SingleInstance
{
    public class MutexGuard
    {
        private const string MutexName = "SINGLEINSTANCE";

        private Mutex _mutex;

        public bool IsSingleInstance()
        {
            if (!Mutex.TryOpenExisting(MutexName, out var _))
            {
                _mutex = new Mutex(true, MutexName);
                return true;
            }

            return false;
        }
    }
}
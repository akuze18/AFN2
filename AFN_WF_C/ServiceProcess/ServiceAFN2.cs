using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace AFN_WF_C.ServiceProcess
{
    class ServiceAFN2 : IDisposable
    {
        private DataContract.AFN2Entities _context;
        private Repositories.Main _main;

        public ServiceAFN2()
        {
            _context = new DataContract.AFN2Entities();
            _main = new Repositories.Main(_context);
        }

        public Repositories.Main Repo
        {
            get { return _main; }
        }

        #region IDispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                _main.Dispose();
                _context.Dispose();//  = null;
            }

            disposed = true;
        }
        #endregion
    }
}

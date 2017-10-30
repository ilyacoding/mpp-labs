using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Library
{
    public abstract class OSHandle<T> : IDisposable
    {
        protected bool Disposed { get; set; }
        public T Resource { get; set; }
        
        public IntPtr Handle { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    var disposable = Resource as IDisposable;
                    disposable?.Dispose();
                }
                
                if (Handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(Handle);
                    Handle = IntPtr.Zero;
                }
                
                Disposed = true;
            }
        }
    }
}

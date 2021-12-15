using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Lab4SPP
{
    public class OSHandle : IDisposable
    {
        private bool disposed = false;
        private IntPtr handle;
        /*
         * Вызывает Marshal.AllocHGlobal метод для выделения того же числа байтов, занимаемого неуправляемой строкой.
         * Метод возвращает IntPtr объект, указывающий на начало неуправляемого блока памяти.
         */
        public OSHandle(int size)
        {
            handle = Marshal.AllocHGlobal(size);
        }
        public IntPtr Handle
        {
            get
            {
                if (!disposed)
                {
                    return handle;
                }
                else
                    throw new ObjectDisposedException(ToString());

            }
        }
        public IntPtr GetHandler()
        {
            return handle;
        }

       /*
        * Освобождает память, выделенную ранее из неуправляемой памяти процесса.
        */
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if(handle != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(handle);
                    }
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        ~OSHandle()
        {
            Dispose(false);
        }
    }
}

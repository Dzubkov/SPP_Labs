using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba_3
{
    class Mutex
    {
        private int sem;

        public Mutex()
        {
            sem = 0;
        }

        public void Lock()
        {
            while(Interlocked.CompareExchange(ref sem, Thread.CurrentThread.ManagedThreadId, 0) != 0)
            {
                Thread.Sleep(100);
            }
        }

        public void Unlock()
        {
            Interlocked.CompareExchange(ref sem, 0, Thread.CurrentThread.ManagedThreadId);
        }
        
    }
}

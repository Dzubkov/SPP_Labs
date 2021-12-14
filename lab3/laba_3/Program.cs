using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba_3
{
    class Program
    {

        private static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
           
            for(int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(print);
                thread.Start();
            }
            Console.ReadLine();
        }

        public static void print()
        {
            mutex.Lock();
            Console.WriteLine("Start thread " + Thread.CurrentThread.ManagedThreadId);
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine("thread " + Thread.CurrentThread.ManagedThreadId + " step " + i);
            }
            Console.WriteLine("Finish thread " + Thread.CurrentThread.ManagedThreadId);
            mutex.Unlock();
        }
    }
}

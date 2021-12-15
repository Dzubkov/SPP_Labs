using System;
using System.Runtime.InteropServices;

namespace Lab4SPP
{
    class Program
    {
        static void Main(string[] args)
        {
            OSHandle handle = new OSHandle(100);
            Console.WriteLine(handle.Handle.ToString());
            handle.Dispose();
            Console.WriteLine(handle.Handle.ToString());
        }
       
    }
}

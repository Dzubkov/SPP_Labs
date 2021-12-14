using System;
using System.Threading;
using Lab1SPP.Elements;

namespace Lab1SPP
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskDelegate del = delegate ()
            {
                Console.WriteLine("Some");
            };
            TaskDelegate del2 = delegate ()
            {
                Console.WriteLine("SomeThing");
            }; TaskDelegate del3 = delegate ()
            {
                Console.WriteLine("SOmething");
            };
            TaskDelegate del4 = delegate ()
            {
                Console.WriteLine("something");
            };
            TaskQueue queue = new TaskQueue(8);
            queue.EnqueueTask(del);
            queue.EnqueueTask(del2);
            queue.EnqueueTask(del3);
            queue.EnqueueTask(del4);
            queue.Size();
            queue.Dispose();
        }

    }
}

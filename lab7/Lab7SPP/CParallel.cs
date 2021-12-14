using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lab1SPP.Elements;
namespace Lab7SPP
{
    public class CParallel
    {
        public void WaitAll(TaskDelegate[] delegates)
        {
            int threadLength = delegates.Length;
            TaskQueue queue = new TaskQueue(threadLength);
            using (var countDown = new CountdownEvent(threadLength))
            {
                    foreach (TaskDelegate task in delegates)
                    {
                        queue.EnqueueTask(() =>
                        {
                            task();
                            countDown.Signal();
                        });
                    }
                countDown.Wait();
            }
            Console.WriteLine("Job well done!");
            queue.Dispose();
        }
        public void StartAll(TaskDelegate[] delegates)
        {
            int threadLength = delegates.Length;
            TaskQueue queue = new TaskQueue(threadLength);
            foreach (TaskDelegate task in delegates)
            {
                queue.EnqueueTask(() =>
                {
                    task();

                });
            }
            queue.Dispose();
        }
    }
}

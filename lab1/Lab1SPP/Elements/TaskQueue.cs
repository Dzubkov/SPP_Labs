using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab1SPP.Elements
{
    public class TaskQueue : IDisposable
    {
        private LinkedList<Thread> pool;
        private bool disposed;
        private int capacity;
        private bool disposingProc;
        private LinkedList<TaskDelegate> taskQueue;
        public TaskQueue(int capacity)
        {
            pool = new LinkedList<Thread>();
            taskQueue = new LinkedList<TaskDelegate>();
            this.capacity = capacity;
            for(int i = 0; i<capacity; i++)
            {
                var thread = new Thread(WorkingProcess) { Name = string.Concat("Thread {1}", i) };
                thread.Start();
                pool.AddLast(thread);
            }
        }
        public int Size()
        {
            return capacity;
        }
        public void EnqueueTask(TaskDelegate task)
        {
            lock (taskQueue)
            {
                if (disposingProc)
                {
                    throw new InvalidOperationException("This Pool is in process of disposing.");
                }
                if (disposed)
                {
                    throw new ObjectDisposedException("This Pool has been already disposed");
                }
                taskQueue.AddLast(task);
                Monitor.PulseAll(taskQueue);
            }
        }
        public void Dispose()
        {
            var waitForThreads = false;
            lock (taskQueue)
            {
                if (!disposed)
                {
                    GC.SuppressFinalize(this);

                    disposingProc = true;
                    while (this.taskQueue.Count > 0)
                    {
                        Monitor.Wait(taskQueue);
                    }

                    disposed = true;
                    Monitor.PulseAll(taskQueue);
                    waitForThreads = true;
                }
            }
            if (waitForThreads)
            {
                foreach (var thread in pool)
                {
                    thread.Join();
                }
            }
        }
        private void WorkingProcess()
        {
            TaskDelegate task = null;
            while (true)
            {
                lock (taskQueue)
                {
                    while (true)
                    {
                        if (disposed)
                        {
                            return;
                        }
                        if (pool.First.Value !=null && ReferenceEquals(Thread.CurrentThread, pool.First.Value) && taskQueue.Count> 0)
                        {
                            task = taskQueue.First.Value;
                            taskQueue.RemoveFirst();
                            pool.RemoveFirst();
                            Monitor.PulseAll(taskQueue);
                            break;
                        }
                        Monitor.Wait(taskQueue);
                    }
                }
                task();
                lock (taskQueue)
                {
                    pool.AddLast(Thread.CurrentThread);
                }
                task = null;
            }
        }
    }
}

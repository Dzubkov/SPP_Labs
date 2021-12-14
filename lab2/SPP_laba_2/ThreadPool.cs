using System;
using System.Collections.Generic;
using System.Threading;

namespace SPP_laba_2
{
    class ThreadPool
    {
        private Queue<Thread> threadPool;
        private Queue<CopyFileInfo> filesInfo;
        private Queue<TaskDelegate> tasks;
        public int count { get; set; }

        public ThreadPool(int count)
        {
            threadPool = new Queue<Thread>();
            filesInfo = new Queue<CopyFileInfo>();
            tasks = new Queue<TaskDelegate>();
            InitPool(count);
        }

        private void InitPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(Process);
                thread.Name = $"Thread {i}";
                thread.Start();
                threadPool.Enqueue(thread);
            }
        }

        public void EnqueueTask(TaskDelegate task, CopyFileInfo info)
        {
            lock (this)
            {
                tasks.Enqueue(task);
                filesInfo.Enqueue(info);
            }
        }

        public int getTasks()
        {
            return tasks.Count;
        }

        private void Process()
        {
            while (true)
            {
                TaskDelegate task = null;
                CopyFileInfo info = null;
                lock (this)
                {
                    if (tasks.Count != 0)
                    {
                        info = filesInfo.Dequeue();
                        task = tasks.Dequeue();
                        count++;
                    }
                }
                if(task != null && info != null)
                {
                    task(info.from, info.to);
                }
            }
        }
    }
}
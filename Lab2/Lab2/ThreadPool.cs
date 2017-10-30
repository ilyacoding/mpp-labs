using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    public delegate void TaskDelegate();

    public class ThreadPool : IDisposable
    {
        private List<Thread> ThreadList { get; }

        private Queue<TaskDelegate> Queue { get; }

        private readonly object _syncRoot = new object();

        public ThreadPool(int threadNumber)
        {
            Queue = new Queue<TaskDelegate>();
            ThreadList = new List<Thread>();

            for (var i = 0; i < threadNumber; i++)
            {
                var thread = new Thread(Worker);
                ThreadList.Add(thread);
                thread.Start();
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            Queue.Enqueue(task);
        }

        public void Worker()
        {
            while (true)
            {
                TaskDelegate task;

                lock (_syncRoot)
                {
                    if (Queue.Count.Equals(0))
                    {
                        continue;
                    }

                    task = Queue.Dequeue();
                }

                task?.Invoke();
            }
        }
        
        public void Dispose()
        {
            foreach (var thread in ThreadList)
            {
                thread.Abort();
            }
        }
    }
}

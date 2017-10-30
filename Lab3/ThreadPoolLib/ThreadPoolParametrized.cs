using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolLib
{
    public delegate void TaskDelegate(object parameters);

    public class ThreadPoolParametrized : IDisposable
    {
        private List<Thread> ThreadList { get; }

        private Queue<TaskDelegateParameter> Queue { get; }

        private readonly object _syncRoot = new object();

        public int TasksCounter;
        public int TasksCompletedCounter;

        public ThreadPoolParametrized(int threadNumber)
        {
            Queue = new Queue<TaskDelegateParameter>();
            ThreadList = new List<Thread>();

            for (var i = 0; i < threadNumber; i++)
            {
                var thread = new Thread(Worker);
                ThreadList.Add(thread);
                thread.Start();
            }
        }

        public void EnqueueTask(TaskDelegate task, object parameters)
        {
            Queue.Enqueue(new TaskDelegateParameter {Parameters = parameters, Task = task});
            Interlocked.Increment(ref TasksCounter);
        }

        public void Worker()
        {
            while (true)
            {
                TaskDelegateParameter task;

                lock (_syncRoot)
                {
                    if (Queue.Count.Equals(0))
                    {
                        continue;
                    }

                    task = Queue.Dequeue();
                }

                task?.Task.Invoke(task?.Parameters);
                Interlocked.Increment(ref TasksCompletedCounter);
            }
        }

        public int WaitAll()
        {
            while (TasksCompletedCounter < TasksCounter) { }
            var tasksCompleted = TasksCompletedCounter;
            TasksCompletedCounter = TasksCounter = 0;
            return tasksCompleted;
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

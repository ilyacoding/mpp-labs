using System;
using System.Threading;
using Mutex = Lab2.Mutex;
using ThreadPool = Lab2.ThreadPool;

namespace ConsoleLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var threadPool = new ThreadPool(2))
            {
                var test = new B();
                threadPool.EnqueueTask(test.For1);
                threadPool.EnqueueTask(test.For2);
                threadPool.EnqueueTask(test.For3);
                threadPool.EnqueueTask(test.For4);
               
                Console.ReadLine();
            }

           
        }
    }
}

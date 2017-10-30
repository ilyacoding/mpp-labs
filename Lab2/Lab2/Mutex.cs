using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    public class Mutex
    {
        private int _semaphore;


        public void Lock()
        {
            while (true)
            {
                if (Interlocked.CompareExchange(ref _semaphore, 1, 0) == 0)
                {
                    return;
                }
            }
        }

        public void Unlock()
        {
            Interlocked.CompareExchange(ref _semaphore, 0, 1);
        }
    }
}

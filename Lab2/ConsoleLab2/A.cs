using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2;

namespace ConsoleLab2
{
    public class A
    {
        private const int Cycle = 10;
        public static Mutex Mutex = new Mutex();

        public void For1()
        {
            Mutex.Lock();
            Console.WriteLine("CAPTURED 1");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("a");
            }
            Mutex.Unlock();
        }

        public void For2()
        {
            Mutex.Lock();
            Console.WriteLine("CAPTURED 2");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aa");
            }
            Mutex.Unlock();
        }

        public void For3()
        {
            Mutex.Lock();
            Console.WriteLine("CAPTURED 3");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aaa");
            }
            Mutex.Unlock();
        }

        public void For4()
        {
            Mutex.Lock();
            Console.WriteLine("CAPTURED 4");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aaaa");
            }
            Mutex.Unlock();
        }
    }
}

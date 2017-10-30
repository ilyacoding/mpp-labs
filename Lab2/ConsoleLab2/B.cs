using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2
{
    public class B
    {
        private const int Cycle = 100;

        public void For1()
        {
            Console.WriteLine("CAPTURED 1");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("a");
            }
        }

        public void For2()
        {
            Console.WriteLine("CAPTURED 2");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aa");
            }
        }

        public void For3()
        {
            Console.WriteLine("CAPTURED 3");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aaa");
            }
        }

        public void For4()
        {
            Console.WriteLine("CAPTURED 4");
            for (int i = 0; i < Cycle; i++)
            {
                Console.WriteLine("aaaa");
            }
        }
    }
}

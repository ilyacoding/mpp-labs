using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lab3;

namespace ConsoleFileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    throw new Exception("Too small amount of arguments");
                }
                var SourcePath = args[0];
                var DestinationPath = args[1];

                var start = DateTime.Now;
                Console.WriteLine($"Files copied: {FileCopy.Copy(SourcePath, DestinationPath)}");
                var finish = DateTime.Now;
                Console.WriteLine($"Elapsed time: {(finish - start).TotalMilliseconds / 1000}");
                Console.WriteLine($"Copied size: {(float)FileCopy.GetDirectorySize(DestinationPath)/1024/1024}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

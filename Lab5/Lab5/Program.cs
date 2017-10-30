using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.AssemblyLoaders;

namespace Lab5
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

                var path = args[0];
                var outputPath = args[1];

                var assemblyHandler = new AssemblyHandler(new FileAssemblyLoader(path));
                assemblyHandler.Load();
                assemblyHandler.SaveToXml(outputPath);
                Console.WriteLine("Writed out to file");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

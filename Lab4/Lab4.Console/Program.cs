using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Lab4.Library;

namespace Lab4.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Outputting");
            var path = "file";
            
            var fsResource = new FileStreamResourceHandler(File.Create(path));
            var str = "Example string";
            byte[] info = new UTF8Encoding(true).GetBytes(str);
            fsResource.Resource.Write(info, 0, info.Length);
        }
    }
}

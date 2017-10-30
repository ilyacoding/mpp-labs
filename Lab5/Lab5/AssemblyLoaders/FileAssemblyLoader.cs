using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.AssemblyLoaders
{
    public class FileAssemblyLoader : AssemblyLoader
    {
        private readonly string _filePath;

        public FileAssemblyLoader(string filePath)
        {
            _filePath = Path.GetFullPath(filePath);
        }

        public override Assembly GetAssembly()
        {
            try
            {
                return Assembly.LoadFrom(_filePath);
            }
            catch (Exception)
            {
                throw new FileLoadException("Can't load assembly");
            }
        }
    }
}

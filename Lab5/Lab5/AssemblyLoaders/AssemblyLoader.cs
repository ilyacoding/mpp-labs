using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.AssemblyLoaders
{
    public abstract class AssemblyLoader
    {
        public abstract Assembly GetAssembly();
    }
}

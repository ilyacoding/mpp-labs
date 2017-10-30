using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttributeLibrary;

namespace TestDll3
{
    [ExportXML]
    class Class3
    {
        private string A;
    }

    [ExportXML]
    class Class2 : Class3
    {
        public int H;
    }

    [ExportXML]
    class Class1
    {
        
    }
}

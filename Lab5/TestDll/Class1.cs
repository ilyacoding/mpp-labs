using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AttributeLibrary;

namespace TestDll
{
    [ExportXML]
    public class Class1 : IDisposable
    {
        public int PublicField;
        private bool PrivateField;
        protected char ProtectedField;
        internal double FieldInternal;
        protected internal float FieldProtectedInternal;
        public cl2 obj_cl2;

        public void PublicMethod()
        {
            
        }

        private string PrivateMethod(string str, int b, char a)
        {
            return "aaa";
        }

        protected int ProtectedMethod()
        {
            return 1;
        }

        internal double InternalMethod(bool c)
        {
            return 1.0;
        }

        protected internal char ProtectedInternalMethod()
        {
            return 'a';
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class cl2
    {
        private bool ShowAll;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.XmlStructure;

namespace Lab6.TreeCreators
{
    public interface ITreeCreator
    {
        TreeNodeEx CreateTree(AssemblyXml rootAssemblyXml);
    }
}

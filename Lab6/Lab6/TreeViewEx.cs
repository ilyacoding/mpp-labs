using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.TreeCreators;
using Lab6.XmlStructure;

namespace Lab6
{
    public sealed class TreeViewEx : TreeView
    {
        public AssemblyXml AssemblyXml { get; }

        public ITreeCreator TreeCreator { get; set; }

        public TreeViewEx(AssemblyXml assemblyXml, ITreeCreator treeCreator) : base()
        {
            AssemblyXml = assemblyXml;
            TreeCreator = treeCreator;

            Dock = DockStyle.Fill;
        }

        public void Render()
        {
            var root = TreeCreator.CreateTree(AssemblyXml);

            Nodes.Clear();
            Nodes.Add(root);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.XmlStructure;

namespace Lab6
{
    public class TreeNodeEx : TreeNode
    {
        public IElement LinkedObject { get; set; }

        public TreeNodeEx(IElement linkedObject) : base(linkedObject.ToString())
        {
            LinkedObject = linkedObject;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    public class MethodSignatureXml : IElement
    {
        [XmlAttribute("accessor")]
        public string Accessor { get; set; }

        [XmlAttribute("return_type")]
        public string ReturnType { get; set; }

        public override string ToString()
        {
            return $"Signature (accessor='{Accessor}', return_type='{ReturnType}')";
        }
    }
}

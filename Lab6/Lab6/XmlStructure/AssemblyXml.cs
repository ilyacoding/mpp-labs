using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [XmlRoot("Namespaces")]
    public class AssemblyXml : IElement
    {
        public readonly bool HasAttributes = true;

        [XmlElement("Namespace")]
        public List<NamespaceXml> Namespaces { get; set; }

        [XmlAttribute("dll")]
        public string AssemblyNameAttribute { get; set; }

        public override string ToString()
        {
            return $"Assembly (dll='{AssemblyNameAttribute}')";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    public class MethodXml : IElement
    {
        [XmlAttribute("name")]
        public string NameAttribute { get; set; }

        [XmlElement("Signature")]
        public MethodSignatureXml MethodSignatureXml { get; set; }

        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public List<ParameterXml> Parameters { get; set; }

        public override string ToString()
        {
            return $"Method (name='{NameAttribute}')";
        }
    }
}

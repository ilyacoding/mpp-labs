using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    public class NamespaceXml : IElement
    {
        [XmlElement("Class")]
        public List<ClassXml> Classes { get; set; }

        [XmlAttribute("name")]
        public string NameAttribute { get; set; }

        public override string ToString()
        {
            return $"Namespace (name='{NameAttribute}')";
        }
    }
}

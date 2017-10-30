using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    [XmlRoot("Class")]
    public class ClassXml : IElement
    {
        [XmlArray("Interfaces")]
        [XmlArrayItem("Interface")]
        public List<InterfaceXml> Interfaces { get; set; }
        
        [XmlArray("InheritedTypes")]
        [XmlArrayItem("InheritedType")]
        public List<InheritedTypeXml> InheritedTypes { get; set; }

        [XmlArray("Fields")]
        [XmlArrayItem("Field")]
        public List<FieldXml> Fields { get; set; }

        [XmlArray("Methods")]
        [XmlArrayItem("Method")]
        public List<MethodXml> Methods { get; set; }

        [XmlAttribute("name")]
        public string NameAttribute { get; set; }

        public override string ToString()
        {
            return $"Class (name='{NameAttribute}')";
        }
    }
}

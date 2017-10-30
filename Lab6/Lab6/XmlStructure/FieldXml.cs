using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    public class FieldXml : IElement
    {
        [XmlAttribute("accessor")]
        public string AccessorAttribute { get; set; }

        [XmlAttribute("type")]
        public string TypeAttribute { get; set; }

        [XmlAttribute("name")]
        public string NameAttribute { get; set; }
        
        [XmlElement("Definition")]
        public ClassDefinitionXml Definition { get; set; }

        public override string ToString()
        {
            return $"Field (name='{NameAttribute}', accessor='{AccessorAttribute}', type='{TypeAttribute}')";
        }
    }
}

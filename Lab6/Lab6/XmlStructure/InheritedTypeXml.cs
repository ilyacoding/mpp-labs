﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab6.XmlStructure
{
    [Serializable]
    public class InheritedTypeXml : IElement
    {
        [XmlAttribute("name")]
        public string NameAttribute { get; set; }

        public override string ToString()
        {
            return $"InheritedType (name='{NameAttribute}')";
        }
    }
}

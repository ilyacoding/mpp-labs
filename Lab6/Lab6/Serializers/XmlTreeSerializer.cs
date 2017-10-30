using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Lab6.XmlStructure;

namespace Lab6.Serializers
{
    public class XmlTreeSerializer<T> : ISerializer<T>
    {
        private XmlSerializer _xmlSerializer;

        public XmlTreeSerializer()
        {
            _xmlSerializer = new XmlSerializer(typeof(T));
        }

        public string Serialize(T obj)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                _xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        public T Deserialize(string str)
        {
            using (var reader = new StreamReader(str))
            {
                return (T)_xmlSerializer.Deserialize(reader);
            }
        }
    }
}

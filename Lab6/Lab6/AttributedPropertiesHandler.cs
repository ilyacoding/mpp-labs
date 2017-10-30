using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab6
{
    public class AttributedPropertiesHandler
    {
        private readonly Type _type;
        private readonly Type _attributeType;
        private readonly BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        public AttributedPropertiesHandler(Type objectType, Type attributeType)
        {
            _type = objectType;
            _attributeType = attributeType;
        }

        public IEnumerable<PropertyInfo> GetProperties()
        {
            return _type.GetProperties(_bindingFlags).Where(property => Attribute.IsDefined(property, _attributeType));
        }

        public void SetPropertyValue(PropertyInfo propertyInfo, object obj, object value)
        {
            propertyInfo.SetValue(obj, value);
        }

        public object GetPropertyValue(PropertyInfo propertyInfo, object obj)
        {
            return propertyInfo.GetValue(obj);
        }

        public string GetAttributeName(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(false);
            foreach (var attribute in attributes)
            {
                var attr = attribute as XmlAttributeAttribute;

                if (attr != null)
                {
                    return attr.AttributeName;
                }
            }

            throw new NotImplementedException();
        }
    }
}

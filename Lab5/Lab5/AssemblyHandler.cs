using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AttributeLibrary;
using Lab5.AssemblyLoaders;

namespace Lab5
{
    public class AssemblyHandler
    {
        private readonly BindingFlags _bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly AssemblyLoader _assemblyLoader;
        private bool _loaded;
        private Assembly _assembly;

        public AssemblyHandler(AssemblyLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }

        public void Load()
        {
            _assembly = _assemblyLoader.GetAssembly();
            _loaded = true;
        }

        public void SaveToXml(string outputPath)
        {
            if (!_loaded) return;

            var sortedTypes = GetSortedTypesList();

            var xDoc = new XmlDocument(new NameTable());
            var root = xDoc.CreateElement("Namespaces");
            var rootAttribute = xDoc.CreateAttribute("dll");
            rootAttribute.Value = _assembly.GetName().Name;
            root.Attributes.Append(rootAttribute);
            xDoc.AppendChild(root);

            foreach (var group in sortedTypes)
            {
                var namespaceNodeElement = xDoc.CreateElement("Namespace");
                namespaceNodeElement.SetAttribute("name", group.First().Namespace);
                var namespaceNode = root.AppendChild(namespaceNodeElement);
                foreach (var type in group)
                {
                    var typeList = new List<TypeInfo>();
                    CreateClassElement(xDoc, namespaceNode, type, typeList);
                }
            }

            xDoc.Save(outputPath);
        }

        private void CreateClassElement(XmlDocument xDoc, XmlNode xmlElement, TypeInfo type, List<TypeInfo> addedTypes)
        {
            if (addedTypes.Contains(type)) { return; }
            addedTypes.Add(type);

            var classNodeElement = xDoc.CreateElement("Class");
            classNodeElement.SetAttribute("name", type.Name);
            var classNode = xmlElement.AppendChild(classNodeElement);

            var interfaces = type.ImplementedInterfaces;
            var inheritedTypes = _assembly.GetTypes().Where(assemblyType => assemblyType.IsSubclassOf(type));
            var fields = type.GetFields(_bindingFlags);
            var methods = type.GetMethods(_bindingFlags);

            CreateInterfaces(xDoc, classNode, interfaces);
            CreateInheritedTypes(xDoc, classNode, inheritedTypes);
            CreateFields(xDoc, classNode, fields, addedTypes);
            CreateMethods(xDoc, classNode, methods);
        }
        
        private IEnumerable<IOrderedEnumerable<TypeInfo>> GetSortedTypesList()
        {
            var types = GetAtributedTypes(typeof(ExportXMLAttribute));
            return types.GroupBy(x => x.Namespace).OrderBy(group => group.First().Namespace).Select(group => group.OrderBy(type => type.Name));
        }

        private string GetFieldModifier(FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
            {
                return "public";
            }

            if (fieldInfo.IsPrivate)
            {
                return "private";
            }

            if (fieldInfo.IsFamilyOrAssembly)
            {
                return "protected internal";
            }

            if (fieldInfo.IsAssembly)
            {
                return "internal";
            }

            if (fieldInfo.IsFamily)
            {
                return "protected";
            }

            throw new NotSupportedException();
        }

        private string GetMethodModifier(MethodInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
            {
                return "public";
            }

            if (fieldInfo.IsPrivate)
            {
                return "private";
            }

            if (fieldInfo.IsFamilyOrAssembly)
            {
                return "protected internal";
            }

            if (fieldInfo.IsAssembly)
            {
                return "internal";
            }

            if (fieldInfo.IsFamily)
            {
                return "protected";
            }

            throw new NotSupportedException();
        }
        
        private IEnumerable<TypeInfo> GetAtributedTypes(Type attributeType)
        {
            return _assembly.DefinedTypes.Where(typeInfo => typeInfo.IsDefined(attributeType, false));
        }

        private void CreateInterfaces(XmlDocument xDoc, XmlNode classNode, IEnumerable<Type> interfaces)
        {
            if (interfaces.Any())
            {
                var interfacesElement = classNode.AppendChild(xDoc.CreateElement("Interfaces"));

                foreach (var interfaceEl in interfaces)
                {
                    var interfaceNodeElement = xDoc.CreateElement("Interface");
                    interfaceNodeElement.SetAttribute("name", interfaceEl.Name);
                    var interfaceElement = interfacesElement.AppendChild(interfaceNodeElement);
                }
            }
        }

        private void CreateInheritedTypes(XmlDocument xDoc, XmlNode classNode, IEnumerable<Type> inheritedTypes)
        {
            if (inheritedTypes.Any())
            {
                var inheritedTypesElement = classNode.AppendChild(xDoc.CreateElement("InheritedTypes"));

                foreach (var inheritedType in inheritedTypes)
                {
                    var inheritedTypeNodeElement = xDoc.CreateElement("InheritedType");
                    inheritedTypeNodeElement.SetAttribute("name", inheritedType.Name);
                    var inheritedTypeElement = inheritedTypesElement.AppendChild(inheritedTypeNodeElement);
                }
            }
        }

        private void CreateFields(XmlDocument xDoc, XmlNode classNode, FieldInfo[] fields, List<TypeInfo> addedTypes)
        {
            if (fields.Any())
            {
                var fieldsElement = classNode.AppendChild(xDoc.CreateElement("Fields"));

                foreach (var field in fields)
                {
                    var fieldType = field.FieldType;
                    var fieldNodeElement = xDoc.CreateElement("Field");
                    fieldNodeElement.SetAttribute("name", field.Name);
                    fieldNodeElement.SetAttribute("accessor", GetFieldModifier(field));
                    fieldNodeElement.SetAttribute("type", fieldType.ToString());
                    var fieldElement = fieldsElement.AppendChild(fieldNodeElement);

                    // Check if recursion required

                    if (fieldType.Namespace.Equals("System")) { continue; }

                    var nestedFieldElement = fieldElement.AppendChild(xDoc.CreateElement("Definition"));

                    CreateClassElement(xDoc, nestedFieldElement, fieldType.GetTypeInfo(), addedTypes);
                }
            }
        }
        
        private void CreateMethods(XmlDocument xDoc, XmlNode classNode, MethodInfo[] methods)
        {
            if (methods.Any())
            {
                var methodsElement = classNode.AppendChild(xDoc.CreateElement("Methods"));

                foreach (var method in methods)
                {
                    var methodNodeElement = xDoc.CreateElement("Method");
                    methodNodeElement.SetAttribute("name", method.Name);
                    var methodElement = methodsElement.AppendChild(methodNodeElement);
                    
                    var signatureNodeElement = xDoc.CreateElement("Signature");
                    signatureNodeElement.SetAttribute("accessor", GetMethodModifier(method));
                    signatureNodeElement.SetAttribute("return_type", method.ReturnType.ToString());
                    methodElement.AppendChild(signatureNodeElement);

                    var parameters = method.GetParameters();

                    if (parameters.Length > 0)
                    {
                        var parametersElement = methodElement.AppendChild(xDoc.CreateElement("Parameters"));

                        foreach (var parameter in parameters)
                        {
                            var parameterNodeElement = xDoc.CreateElement("Parameter");
                            parameterNodeElement.SetAttribute("name", parameter.Name);
                            parameterNodeElement.SetAttribute("type", parameter.ParameterType.ToString());
                            parametersElement.AppendChild(parameterNodeElement);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.XmlStructure;

namespace Lab6.TreeCreators
{
    public class XmlTreeCreator : ITreeCreator
    {
        public TreeNodeEx CreateTree(AssemblyXml rootAssemblyXml)
        {
            var rootTreeNode = new TreeNodeEx(rootAssemblyXml);

            var namespacesTreeNode = new TreeNode("Namespaces");
            rootTreeNode.Nodes.Add(namespacesTreeNode);
            CreateNamespaces(namespacesTreeNode, rootAssemblyXml.Namespaces);
            
            return rootTreeNode;
        }

        public void CreateNamespaces(TreeNode treeNode, List<NamespaceXml> namespaceXmls)
        {
            foreach (var namespaceXml in namespaceXmls)
            {
                var namespaceTreeNode = new TreeNodeEx(namespaceXml);
                treeNode.Nodes.Add(namespaceTreeNode);
                if (namespaceXml.Classes.Count > 0)
                {
                    var classesTreeNode = new TreeNode("Classes");
                    namespaceTreeNode.Nodes.Add(classesTreeNode);
                    CreateClasses(classesTreeNode, namespaceXml.Classes);
                }
            }
        }

        public void CreateClasses(TreeNode treeNode, List<ClassXml> classXmls)
        {
            foreach (var classXml in classXmls)
            {
                var classTreeNode = new TreeNodeEx(classXml);
                treeNode.Nodes.Add(classTreeNode);
                if (classXml.Interfaces.Count > 0)
                {
                    var interfacesTreeNode = new TreeNode("Interfaces");
                    classTreeNode.Nodes.Add(interfacesTreeNode);
                    CreateInterfaces(classTreeNode, classXml.Interfaces);
                }

                if (classXml.InheritedTypes.Count > 0)
                {
                    var inheritedTypesTreeNode = new TreeNode("InheritedTypes");
                    classTreeNode.Nodes.Add(inheritedTypesTreeNode);
                    CreateInheritedTypes(inheritedTypesTreeNode, classXml.InheritedTypes);
                }

                if (classXml.Fields.Count > 0)
                {
                    var fieldsTreeNode = new TreeNode("Fields");
                    classTreeNode.Nodes.Add(fieldsTreeNode);
                    CreateFields(fieldsTreeNode, classXml.Fields);
                }

                if (classXml.Methods.Count > 0)
                {
                    var methodsTreeNode = new TreeNode("Methods");
                    classTreeNode.Nodes.Add(methodsTreeNode);
                    CreateMethods(methodsTreeNode, classXml.Methods);
                }
            }
        }

        public void CreateInterfaces(TreeNode treeNode, List<InterfaceXml> interfaceXmls)
        {
            foreach (var interfaceXml in interfaceXmls)
            {
                var interfaceTreeNode = new TreeNodeEx(interfaceXml);
                treeNode.Nodes.Add(interfaceTreeNode);
            }
        }

        public void CreateInheritedTypes(TreeNode treeNode, List<InheritedTypeXml> inheritedTypeXmls)
        {
            foreach (var inheritedTypeXml in inheritedTypeXmls)
            {
                var inheritedTypeTreeNode = new TreeNodeEx(inheritedTypeXml);
                treeNode.Nodes.Add(inheritedTypeTreeNode);
            }
        }

        public void CreateFields(TreeNode treeNode, List<FieldXml> fieldXmls)
        {
            foreach (var fieldXml in fieldXmls)
            {
                var fieldTreeNode = new TreeNodeEx(fieldXml);
                treeNode.Nodes.Add(fieldTreeNode);

                if (fieldXml.Definition != null)
                {
                    CreateFieldDefinition(fieldTreeNode, fieldXml.Definition);
                }
            }
        }

        public void CreateFieldDefinition(TreeNode treeNode, ClassDefinitionXml classDefinitionXml)
        {
            var classDefinitionTreeNode = new TreeNodeEx(classDefinitionXml);
            treeNode.Nodes.Add(classDefinitionTreeNode);

            CreateClasses(classDefinitionTreeNode, new List<ClassXml>() { classDefinitionXml.Class });
        }

        public void CreateMethods(TreeNode treeNode, List<MethodXml> methodXmls)
        {
            foreach (var methodXml in methodXmls)
            {
                var methodTreeNode = new TreeNodeEx(methodXml);
                treeNode.Nodes.Add(methodTreeNode);
                
                CreateMethodSignature(methodTreeNode, methodXml.MethodSignatureXml);

                if (methodXml.Parameters.Count > 0)
                {
                    var parametersTreeNode = new TreeNode("Parameters");
                    methodTreeNode.Nodes.Add(parametersTreeNode);
                    CreateMethodParameters(parametersTreeNode, methodXml.Parameters);
                }
            }
        }

        public void CreateMethodSignature(TreeNode treeNode, MethodSignatureXml methodSignatureXml)
        {
            var methodSignatureTreeNode = new TreeNodeEx(methodSignatureXml);
            treeNode.Nodes.Add(methodSignatureTreeNode);
        }

        public void CreateMethodParameters(TreeNode treeNode, List<ParameterXml> parameterXmls)
        {
            foreach (var parameterXml in parameterXmls)
            {
                var parameterTreeNode = new TreeNodeEx(parameterXml);
                treeNode.Nodes.Add(parameterTreeNode);
            }
        }
    }
}

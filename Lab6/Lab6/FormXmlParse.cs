using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lab6.Serializers;
using Lab6.TreeCreators;
using Lab6.XmlStructure;

namespace Lab6
{
    public partial class FormXmlParser : Form
    {
        private readonly ISerializer<AssemblyXml> _serializer;

        public FormXmlParser()
        {
            InitializeComponent();

            _serializer = new XmlTreeSerializer<AssemblyXml>();
        }

        private void saveAsToolStripMenuItemMain_Click(object sender, EventArgs e)
        {
            if (saveXmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                var currentTabPage = (TabPageEx)tabControlMain.SelectedTab;

                if (currentTabPage == null) return;

                var treeView = (TreeViewEx)currentTabPage.Controls[0];
                
                var savePath = saveXmlFileDialog.FileName;
                var serializedData = _serializer.Serialize(treeView.AssemblyXml);

                File.WriteAllText(savePath, serializedData);

                currentTabPage.ProcessSave();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var currentTabPage = (TabPageEx)tabControlMain.SelectedTab;

                if (currentTabPage == null) return;

                var treeView = (TreeViewEx)currentTabPage.Controls[0];

                var savePath = currentTabPage.FilePath;
                var serializedData = _serializer.Serialize(treeView.AssemblyXml);

                File.WriteAllText(savePath, serializedData);

                currentTabPage.ProcessSave();
            }
            catch
            {
                MessageBox.Show("Error occured while saving file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void AddTabPageWithPath(string tabName, string path, TreeView treeView)
        {
            var tabPage = new TabPageEx(tabName, path) { Size = tabControlMain.Size };
            tabPage.Controls.Add(treeView);
            tabControlMain.TabPages.Add(tabPage);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openXmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openXmlFileDialog.FileName))
                {
                    try
                    {
                        var assemblyObject = _serializer.Deserialize(openXmlFileDialog.FileName);

                        var treeView = new TreeViewEx(assemblyObject, new XmlTreeCreator());
                        treeView.NodeMouseDoubleClick += treeView_NodeMouseDoubleClick;
                        AddTabPageWithPath(openXmlFileDialog.SafeFileName, openXmlFileDialog.FileName, treeView);

                        treeView.Render();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file content", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewEx treeView;
            TabPageEx tabPage;
            TreeNodeEx treeNodeEx;

            try
            {
                treeView = (TreeViewEx) sender;
                tabPage = (TabPageEx) treeView.Parent;
                treeNodeEx = (TreeNodeEx) e.Node;
            }
            catch 
            {
                return;
            }
            
            var linkedObject = treeNodeEx.LinkedObject;
            var type = treeNodeEx.LinkedObject.GetType();
            var attributeType = typeof(XmlAttributeAttribute);
            
            var propertyHandler = new AttributedPropertiesHandler(type, attributeType);
            var properties = propertyHandler.GetProperties();

            foreach (var property in properties)
            {
                var oldValue = propertyHandler.GetPropertyValue(property, linkedObject);
                var attributeName = propertyHandler.GetAttributeName(property);
                var newValue = Microsoft.VisualBasic.Interaction.InputBox(
                    $"{attributeName}=", "Setting attribute", oldValue.ToString());
                propertyHandler.SetPropertyValue(property, linkedObject, newValue);
            }

            treeView.Render();

            tabPage.ProcessChange();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedType = tabControlMain.SelectedTab;
            
            if (selectedType == null) return;

            tabControlMain.TabPages.Remove(selectedType);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Crafted with care in Minsk, by Ilya Kovalenko.", "About");
        }
    }
}

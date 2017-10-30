using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public class TabPageEx : TabPage
    {
        private bool _contentChanged;
        
        public string FilePath { get; }

        public TabPageEx(string tabName, string filePath) : base(tabName)
        {
            FilePath = filePath;
        }

        public void ProcessChange()
        {
            if (!_contentChanged)
            {
                _contentChanged = true;
                Text += '*';
            }
        }

        public void ProcessSave()
        {
            Text = Text.TrimEnd('*');
            _contentChanged = false;
        }
    }
}

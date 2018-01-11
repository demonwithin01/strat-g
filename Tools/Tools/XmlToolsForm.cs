using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class XmlToolsForm : Form
    {
        public XmlToolsForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Language Settings|*.ls",
                InitialDirectory = @"G:\Demonic Entertainment\Strategy Game\Strategy Game\Assets\Language\"
            };

            openFileDialog.ShowDialog();
        }
    }
}

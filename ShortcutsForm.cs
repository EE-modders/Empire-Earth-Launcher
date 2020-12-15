using System;
using System.Windows.Forms;

namespace EELauncher
{
    public partial class ShortcutsForm : Form
    {
        public ShortcutsForm()
        {
            InitializeComponent();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

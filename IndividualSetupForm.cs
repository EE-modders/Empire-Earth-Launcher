using System;
using System.Windows.Forms;

namespace EELauncher
{
    public partial class IndividualSetupForm : Form
    {
        public IndividualSetupForm()
        {
            InitializeComponent();

            if (Utils.isWindowsOld())
            {
                windowsVersionComboBox.SelectedIndex = 1;
            }
            else
            {
                windowsVersionComboBox.SelectedIndex = 0;
            }

        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

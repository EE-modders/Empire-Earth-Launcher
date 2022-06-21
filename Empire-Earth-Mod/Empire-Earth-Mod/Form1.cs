using System;
using System.Windows.Forms;
using Empire_Earth_Mod_Lib;

namespace Empire_Earth_Mod
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            label1.Text += WindowsVersion.GetWindowsVersionName(WindowsVersion.GetCurrentWindowsVersion());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ModCreatorForm().ShowDialog();
        }
    }
}
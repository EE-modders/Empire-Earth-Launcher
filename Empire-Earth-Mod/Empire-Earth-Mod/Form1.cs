using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Empire_Earth_Mod_Lib;

namespace Empire_Earth_Mod
{
    public partial class Form1 : Form
    {
        
        private ModData newModData;
        private DirectoryInfo creatorDir;
        
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
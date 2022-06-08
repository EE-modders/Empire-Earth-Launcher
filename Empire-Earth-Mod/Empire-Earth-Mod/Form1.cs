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
        
        public Form1()
        {
            InitializeComponent();
            label1.Text += WindowsVersion.GetWindowsVersionName(WindowsVersion.GetCurrentWindowsVersion());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newModData = new ModData("Empire Earth Mod", "An amazing description");
            Debug.WriteLine("Mod UUID: " + newModData.Uuid);

            var di = new DirectoryInfo("./creator");
            /*if (di.Exists)
                di.Delete(true);
            di.Create();*/

            foreach (var dirs in di.GetDirectories())
            {
                foreach (var file in dirs.GetFiles())
                {
                    dataGridView1.Rows.Add(file.Name, dirs.Name, default);
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && sender is TextBox text)
            {
                if (Encoding.UTF8.GetByteCount(text.Text) == text.Text.Length)
                {
                    string variantName = text.Text.Replace(' ', '_');
                    listBox1.Items.Add(variantName);
                    var di = new DirectoryInfo("./creator");
                    di.CreateSubdirectory(variantName);
                    text.Text = "";
                    e.Handled = true;
                }
                else
                {
                    MessageBox.Show("You can only use ASCII characters in your variant name.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                }
            }
        }
    }
}
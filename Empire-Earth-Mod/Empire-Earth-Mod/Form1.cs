﻿using System;
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
            ModData mod = new ModData("Empire Earth Mod", "An amazing description");
            Debug.WriteLine("Mod UUID: " + mod.Uuid);

            var di = new DirectoryInfo("./creator");
            /*if (di.Exists)
                di.Delete(true);
            di.Create();*/

            foreach (var dirs in di.GetDirectories())
            {
                if (dirs.Name.Equals("default"))
                {
                    foreach (var file in dirs.GetFiles())
                    {
                        dataGridView1.Rows.Add("file", true, true, true);
                    }
                }
            }
        }
    }
}
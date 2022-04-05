﻿using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public partial class LauncherDialog : KryptonForm
    {
        public LauncherDialog(string title, string message, MessageBoxButtons buttons)
        {
            InitializeComponent();
            Program.LauncherKryptonTheme.AddPalette(launcherKryptonPalette, this);

            pictureBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            pictureBox3.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox4.Image.RotateFlip(RotateFlipType.RotateNoneFlipX | RotateFlipType.Rotate180FlipNone);

            kryptonLabel1.Text = title;
            kryptonLabel2.Text = message;


        }
    }
}

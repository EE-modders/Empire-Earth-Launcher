using System.ComponentModel;

namespace Empire_Earth_Mod
{
    partial class ModCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.variantsKryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.VariantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariantUuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonLabel8 = new Krypton.Toolkit.KryptonLabel();
            this.removeVariantKryptonButton = new Krypton.Toolkit.KryptonButton();
            this.variantKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.addVariantKryptonButton = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.authorsKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.contactKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.versionKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.descriptionKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonTextBox2 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.nameKryptonTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonButton6 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton5 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.bannersPictureBox = new System.Windows.Forms.PictureBox();
            this.bannersVariantsKryptonComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonButton4 = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.nextKryptonButton = new Krypton.Toolkit.KryptonButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.variantsKryptonDataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannersPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannersVariantsKryptonComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 317);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.variantsKryptonDataGridView1);
            this.tabPage1.Controls.Add(this.kryptonLabel8);
            this.tabPage1.Controls.Add(this.removeVariantKryptonButton);
            this.tabPage1.Controls.Add(this.variantKryptonTextBox);
            this.tabPage1.Controls.Add(this.addVariantKryptonButton);
            this.tabPage1.Controls.Add(this.kryptonLabel5);
            this.tabPage1.Controls.Add(this.authorsKryptonTextBox);
            this.tabPage1.Controls.Add(this.kryptonLabel4);
            this.tabPage1.Controls.Add(this.contactKryptonTextBox);
            this.tabPage1.Controls.Add(this.versionKryptonTextBox);
            this.tabPage1.Controls.Add(this.kryptonLabel3);
            this.tabPage1.Controls.Add(this.descriptionKryptonTextBox);
            this.tabPage1.Controls.Add(this.kryptonLabel2);
            this.tabPage1.Controls.Add(this.kryptonTextBox2);
            this.tabPage1.Controls.Add(this.kryptonLabel1);
            this.tabPage1.Controls.Add(this.nameKryptonTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 291);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // variantsKryptonDataGridView1
            // 
            this.variantsKryptonDataGridView1.AllowUserToAddRows = false;
            this.variantsKryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.VariantName, this.VariantUuid });
            this.variantsKryptonDataGridView1.Location = new System.Drawing.Point(279, 79);
            this.variantsKryptonDataGridView1.MultiSelect = false;
            this.variantsKryptonDataGridView1.Name = "variantsKryptonDataGridView1";
            this.variantsKryptonDataGridView1.RowHeadersVisible = false;
            this.variantsKryptonDataGridView1.Size = new System.Drawing.Size(326, 174);
            this.variantsKryptonDataGridView1.TabIndex = 22;
            // 
            // VariantName
            // 
            this.VariantName.HeaderText = "Name";
            this.VariantName.Name = "VariantName";
            // 
            // VariantUuid
            // 
            this.VariantUuid.HeaderText = "ID";
            this.VariantUuid.Name = "VariantUuid";
            this.VariantUuid.ReadOnly = true;
            this.VariantUuid.Width = 225;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(410, 27);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel8.TabIndex = 21;
            this.kryptonLabel8.Values.Text = "Variants";
            // 
            // removeVariantKryptonButton
            // 
            this.removeVariantKryptonButton.Location = new System.Drawing.Point(508, 53);
            this.removeVariantKryptonButton.Name = "removeVariantKryptonButton";
            this.removeVariantKryptonButton.Size = new System.Drawing.Size(24, 20);
            this.removeVariantKryptonButton.TabIndex = 20;
            this.removeVariantKryptonButton.Values.Text = "-";
            this.removeVariantKryptonButton.Click += new System.EventHandler(this.removeVariantKryptonButton_Click);
            // 
            // variantKryptonTextBox
            // 
            this.variantKryptonTextBox.Location = new System.Drawing.Point(344, 53);
            this.variantKryptonTextBox.Name = "variantKryptonTextBox";
            this.variantKryptonTextBox.Size = new System.Drawing.Size(133, 20);
            this.variantKryptonTextBox.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.variantKryptonTextBox.TabIndex = 19;
            // 
            // addVariantKryptonButton
            // 
            this.addVariantKryptonButton.Location = new System.Drawing.Point(483, 53);
            this.addVariantKryptonButton.Name = "addVariantKryptonButton";
            this.addVariantKryptonButton.Size = new System.Drawing.Size(24, 20);
            this.addVariantKryptonButton.TabIndex = 18;
            this.addVariantKryptonButton.Values.Text = "+";
            this.addVariantKryptonButton.Click += new System.EventHandler(this.addVariantKryptonButton_Click);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(34, 246);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(53, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "Authors";
            // 
            // authorsKryptonTextBox
            // 
            this.authorsKryptonTextBox.Location = new System.Drawing.Point(91, 243);
            this.authorsKryptonTextBox.Name = "authorsKryptonTextBox";
            this.authorsKryptonTextBox.Size = new System.Drawing.Size(125, 23);
            this.authorsKryptonTextBox.TabIndex = 9;
            this.authorsKryptonTextBox.Text = "author name";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(34, 217);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(53, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Contact";
            // 
            // contactKryptonTextBox
            // 
            this.contactKryptonTextBox.Location = new System.Drawing.Point(91, 214);
            this.contactKryptonTextBox.Name = "contactKryptonTextBox";
            this.contactKryptonTextBox.Size = new System.Drawing.Size(125, 23);
            this.contactKryptonTextBox.TabIndex = 7;
            this.contactKryptonTextBox.Text = "an@email.world";
            // 
            // versionKryptonTextBox
            // 
            this.versionKryptonTextBox.Location = new System.Drawing.Point(91, 185);
            this.versionKryptonTextBox.Name = "versionKryptonTextBox";
            this.versionKryptonTextBox.Size = new System.Drawing.Size(125, 23);
            this.versionKryptonTextBox.TabIndex = 6;
            this.versionKryptonTextBox.Text = "1.0.0.0";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(34, 188);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "Version";
            // 
            // descriptionKryptonTextBox
            // 
            this.descriptionKryptonTextBox.Location = new System.Drawing.Point(34, 79);
            this.descriptionKryptonTextBox.Multiline = true;
            this.descriptionKryptonTextBox.Name = "descriptionKryptonTextBox";
            this.descriptionKryptonTextBox.Size = new System.Drawing.Size(182, 100);
            this.descriptionKryptonTextBox.TabIndex = 4;
            this.descriptionKryptonTextBox.Text = "TestMod description";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(34, 53);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Description";
            // 
            // kryptonTextBox2
            // 
            this.kryptonTextBox2.Location = new System.Drawing.Point(83, 50);
            this.kryptonTextBox2.Multiline = true;
            this.kryptonTextBox2.Name = "kryptonTextBox2";
            this.kryptonTextBox2.Size = new System.Drawing.Size(133, 0);
            this.kryptonTextBox2.TabIndex = 2;
            this.kryptonTextBox2.Text = "TestMod";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(34, 24);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Name";
            // 
            // nameKryptonTextBox
            // 
            this.nameKryptonTextBox.Location = new System.Drawing.Point(83, 21);
            this.nameKryptonTextBox.Name = "nameKryptonTextBox";
            this.nameKryptonTextBox.Size = new System.Drawing.Size(133, 23);
            this.nameKryptonTextBox.TabIndex = 0;
            this.nameKryptonTextBox.Text = "TestMod";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.kryptonGroupBox1);
            this.tabPage2.Controls.Add(this.kryptonButton4);
            this.tabPage2.Controls.Add(this.kryptonLabel6);
            this.tabPage2.Controls.Add(this.iconPictureBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 291);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(198, 20);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton6);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton5);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton3);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.bannersPictureBox);
            this.kryptonGroupBox1.Panel.Controls.Add(this.bannersVariantsKryptonComboBox);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(397, 247);
            this.kryptonGroupBox1.TabIndex = 28;
            this.kryptonGroupBox1.Values.Heading = "Banners";
            // 
            // kryptonButton6
            // 
            this.kryptonButton6.Location = new System.Drawing.Point(85, 182);
            this.kryptonButton6.Name = "kryptonButton6";
            this.kryptonButton6.Size = new System.Drawing.Size(75, 23);
            this.kryptonButton6.TabIndex = 41;
            this.kryptonButton6.Values.Text = "Remove";
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.Location = new System.Drawing.Point(232, 182);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.Size = new System.Drawing.Size(75, 23);
            this.kryptonButton5.TabIndex = 40;
            this.kryptonButton5.Values.Text = "Add new";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Location = new System.Drawing.Point(73, 99);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(16, 41);
            this.kryptonButton3.TabIndex = 39;
            this.kryptonButton3.Values.Text = "<";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(304, 99);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(16, 41);
            this.kryptonButton2.TabIndex = 37;
            this.kryptonButton2.Values.Text = ">";
            // 
            // bannersPictureBox
            // 
            this.bannersPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bannersPictureBox.Location = new System.Drawing.Point(85, 56);
            this.bannersPictureBox.Name = "bannersPictureBox";
            this.bannersPictureBox.Size = new System.Drawing.Size(222, 129);
            this.bannersPictureBox.TabIndex = 38;
            this.bannersPictureBox.TabStop = false;
            // 
            // bannersVariantsKryptonComboBox
            // 
            this.bannersVariantsKryptonComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.bannersVariantsKryptonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bannersVariantsKryptonComboBox.DropDownWidth = 121;
            this.bannersVariantsKryptonComboBox.IntegralHeight = false;
            this.bannersVariantsKryptonComboBox.Location = new System.Drawing.Point(138, 18);
            this.bannersVariantsKryptonComboBox.Name = "bannersVariantsKryptonComboBox";
            this.bannersVariantsKryptonComboBox.Size = new System.Drawing.Size(121, 21);
            this.bannersVariantsKryptonComboBox.TabIndex = 36;
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.Location = new System.Drawing.Point(62, 184);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.Size = new System.Drawing.Size(75, 23);
            this.kryptonButton4.TabIndex = 24;
            this.kryptonButton4.Values.Text = "Select";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(83, 52);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(34, 20);
            this.kryptonLabel6.TabIndex = 26;
            this.kryptonLabel6.Values.Text = "Icon";
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconPictureBox.Location = new System.Drawing.Point(45, 78);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(100, 100);
            this.iconPictureBox.TabIndex = 25;
            this.iconPictureBox.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(637, 291);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // nextKryptonButton
            // 
            this.nextKryptonButton.Location = new System.Drawing.Point(559, 335);
            this.nextKryptonButton.Name = "nextKryptonButton";
            this.nextKryptonButton.Size = new System.Drawing.Size(94, 23);
            this.nextKryptonButton.TabIndex = 0;
            this.nextKryptonButton.Values.Text = "Next >";
            this.nextKryptonButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // ModCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(669, 366);
            this.Controls.Add(this.nextKryptonButton);
            this.Controls.Add(this.tabControl1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "ModCreatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.variantsKryptonDataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bannersPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannersVariantsKryptonComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridViewTextBoxColumn VariantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariantUuid;

        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonComboBox bannersVariantsKryptonComboBox;

        private Krypton.Toolkit.KryptonDataGridView variantsKryptonDataGridView1;

        private Krypton.Toolkit.KryptonLabel kryptonLabel8;

        private Krypton.Toolkit.KryptonButton removeVariantKryptonButton;

        private Krypton.Toolkit.KryptonTextBox variantKryptonTextBox;

        private Krypton.Toolkit.KryptonButton addVariantKryptonButton;

        private Krypton.Toolkit.KryptonButton kryptonButton5;
        private Krypton.Toolkit.KryptonButton kryptonButton6;

        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonButton kryptonButton4;

        private Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.PictureBox bannersPictureBox;

        private System.Windows.Forms.PictureBox iconPictureBox;

        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonTextBox authorsKryptonTextBox;

        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonTextBox contactKryptonTextBox;

        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox versionKryptonTextBox;

        private Krypton.Toolkit.KryptonTextBox descriptionKryptonTextBox;

        private Krypton.Toolkit.KryptonButton nextKryptonButton;
        private Krypton.Toolkit.KryptonTextBox nameKryptonTextBox;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBox2;

        private System.Windows.Forms.TabPage tabPage3;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        #endregion
    }
}
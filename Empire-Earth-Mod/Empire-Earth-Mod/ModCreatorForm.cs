﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Empire_Earth_Mod_Lib;
using Empire_Earth_Mod_Lib.Serialization;
using Krypton.Toolkit;

namespace Empire_Earth_Mod
{
    public partial class ModCreatorForm : KryptonForm
    {
        private ModData mod;
        private ModData.Creator creator;

        public ModCreatorForm()
        {
            InitializeComponent();
            mod = new ModData();
            creator = new ModData.Creator(mod, "./creator");

            if (kryptonDataGridView1.Columns[3] is DataGridViewComboBoxColumn)
            {
                if (kryptonDataGridView1.Columns[3] is not DataGridViewComboBoxColumn columnAlternative)
                    return;
                Enum.GetValues(typeof(ModFile.ModFileType)).Cast<ModFile.ModFileType>()
                    .Select(ModFile.GetModFileName).ToList()
                    .ForEach(fileName => columnAlternative.Items.Add(fileName));
            }
        }

        /* Variants Management */
        
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                mod.Name = nameKryptonTextBox.Text;
                mod.Description = descriptionKryptonTextBox.Text;
                mod.Version = new Version(versionKryptonTextBox.Text);
                mod.Authors.Add(authorsKryptonTextBox.Text);

                
                foreach (DataGridViewRow row in variantsKryptonDataGridView1.Rows)
                {
                    mod.AddOrUpdateVariant(Guid.Parse(row.Cells[1].Value.ToString()), row.Cells[0].Value.ToString());
                }

                bannersVariantsKryptonComboBox.Items.Clear();
                bannersVariantsKryptonComboBox.Items.AddRange(
                    mod.Variants.Values.Select(x => x.ToString() as object).ToArray());

                if (bannersVariantsKryptonComboBox.SelectedIndex == -1 &&
                    bannersVariantsKryptonComboBox.Items.Count > 0)
                {
                    bannersVariantsKryptonComboBox.SelectedIndex = 0;
                    Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                        value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
                    _bannerIndex = 0;
                    _UpdateBannerPreview(selectedVariantUuid);
                }
                else
                {
                    _UpdateBannerPreview(Guid.Empty);
                }
            }

            if (tabControl1.SelectedIndex == 1)
            {
                filesKryptonComboBox.Items.Clear();
                filesKryptonComboBox.Items.AddRange(
                    mod.Variants.Values.Select(x => x.ToString() as object).ToArray());
                
                creator.ReloadVariantsFolders();
                creator.ExportBannersAndIcon();
            }
            
            tabControl1.SelectTab(tabControl1.SelectedTab.TabIndex + 1);
            backKryptonButton.Visible = true;

            Debug.WriteLine("STEP " + tabControl1.SelectedTab.TabIndex);
            Debug.WriteLine(JsonSerializer<ModData>.Serialize(BinarySerializer<ModData>.Deserialize(BinarySerializer<ModData>.Serialize(mod))));
        }
        
        private void backKryptonButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabControl1.SelectedTab.TabIndex - 1);
            if (tabControl1.SelectedTab.TabIndex == 0)
                backKryptonButton.Visible = false;
        }

        private void addVariantKryptonButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(variantKryptonTextBox.Text))
            {
                if (variantsKryptonDataGridView1.Rows.Cast<DataGridViewRow>().Any(x =>
                        (x.Cells[0].Value.ToString().Equals(variantKryptonTextBox.Text,
                            StringComparison.InvariantCultureIgnoreCase))))
                {
                    MessageBox.Show("Variant already exists", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                variantsKryptonDataGridView1.Rows.Add(variantKryptonTextBox.Text, Guid.NewGuid());
                variantKryptonTextBox.Clear();
            }
        }

        private void removeVariantKryptonButton_Click(object sender, EventArgs e)
        {
            if (variantsKryptonDataGridView1.SelectedRows.Count != 1)
                return;
            
            if (MessageBox.Show("Are you sure you want to remove this variant?\n" +
                    "If you need to simply rename it double click on the variant name cell.", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Guid variantId = Guid.Parse(variantsKryptonDataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (mod.RemoveVariant(variantId))
                {
                    MessageBox.Show("Variant removed, some related data to that variant (banners, files, etc...) has been deleted.", 
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                variantsKryptonDataGridView1.Rows.RemoveAt(variantsKryptonDataGridView1.SelectedRows[0].Index);
            }
        }

        /* Icon Management */
        
        private void iconKryptonButton4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(ofd.FileName))
                        return;
                    try
                    {
                        mod.SetIcon(Image.FromFile(ofd.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while loading icon: " + ex.Message, "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    iconPictureBox.Image = mod.GetIcon();
                }
            }
        }
        
        /* Banner Management */
        private int _bannerIndex = 0;

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a variant to add a banner for it", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
            
            if (!mod.DoesVariantExist(selectedVariantUuid))
            {
                MessageBox.Show("Variant does not exist", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(ofd.FileName))
                        return;
                    try
                    {
                        mod.AddBanner(Image.FromFile(ofd.FileName), selectedVariantUuid);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while adding banner: " + ex.Message, "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _bannerIndex = mod.GetBanners(selectedVariantUuid).Count - 1;
                    _UpdateBannerPreview(selectedVariantUuid);
                }
            }
        }
        
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
                return;
            Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
            
            if (!mod.HasBanner(selectedVariantUuid))
                return;
            mod.GetBanners(selectedVariantUuid).RemoveAt(_bannerIndex);
            if (_bannerIndex != 0)
                _bannerIndex--;
            _UpdateBannerPreview(selectedVariantUuid);
        }

        private void bannersVariantsKryptonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
            {
                _UpdateBannerPreview(Guid.Empty);
            }
            else
            {
                Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                    value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
                _bannerIndex = 0;
                _UpdateBannerPreview(selectedVariantUuid);
            }
        }

        private void nextBannerKryptonButton_Click(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
                return;
            Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
            
            if (!mod.HasBanner(selectedVariantUuid) || _bannerIndex + 1 == mod.GetBanners(selectedVariantUuid).Count)
                return;
            _bannerIndex++;
            _UpdateBannerPreview(selectedVariantUuid);
        }

        private void prevBannerKryptonButton_Click(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
                return;
            Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
            
            if (!mod.HasBanner(selectedVariantUuid) || _bannerIndex == 0)
                return;
            _bannerIndex--;
            _UpdateBannerPreview(selectedVariantUuid);
        }

        private void _UpdateBannerPreview(Guid variantUuid)
        {
            if (variantUuid == Guid.Empty)
            {
                bannersPictureBox.Image = null;
                kryptonButton5.Enabled = false;
                kryptonButton6.Enabled = false;
                prevBannerKryptonButton.Enabled = false;
                nextBannerKryptonButton.Enabled = false;
                kryptonLabel7.Values.ExtraText = string.Empty;
            } else if (!mod.HasBanner(variantUuid)){
                bannersPictureBox.Image = null;
                kryptonButton5.Enabled = true;
                kryptonButton6.Enabled = false;
                prevBannerKryptonButton.Enabled = false;
                nextBannerKryptonButton.Enabled = false;
                kryptonLabel7.Values.ExtraText = string.Empty;
            } else {
                int bannerStrIndex = mod.GetBanners(variantUuid).Count > 0 ? _bannerIndex + 1 : 0;
                
                bannersPictureBox.Image = mod.GetBanners(variantUuid)[_bannerIndex];
                kryptonButton5.Enabled = true;
                kryptonButton6.Enabled = true;
                prevBannerKryptonButton.Enabled = true;
                nextBannerKryptonButton.Enabled = true;
                kryptonLabel7.Values.ExtraText = "(" + bannerStrIndex+ "/" + mod.GetBanners(variantUuid).Count + ")";
            }
        }

        private void _UpdateVariantFilesPreview(Guid variantUuid)
        {
            List<string> alreadyPresent = new List<string>();
            foreach (DataGridViewRow row in kryptonDataGridView1.Rows)
            {
                var rowFileRelative = row.Cells[1].Value.ToString();
                var preFilePathProduct = row.Cells[2].Value.ToString().Equals("EEC") ? "\\EEC" :
                    row.Cells[2].Value.ToString().Equals("AOC") ? "\\AOC" : "all";
                string finalRowFilePath = preFilePathProduct + rowFileRelative;
                alreadyPresent.Add(finalRowFilePath);
                Debug.WriteLine(finalRowFilePath);
            }
            
            foreach (var modFile in mod.ModFiles)
            {
                if (!alreadyPresent.Contains(modFile.RelativeFilePath))
                {
                    kryptonDataGridView1.Rows.Add(null, modFile.RelativeFilePath.Substring(4),
                        modFile.RelativeFilePath.StartsWith("\\EEC") ? "EEC" : modFile.RelativeFilePath.StartsWith("\\AOC") ? "AOC" : "Both",
                        ModFile.GetModFileName(
                            ModFile.GetDefaultModFileTypeFromFileExtension(
                                Path.GetExtension(modFile.RelativeFilePath))));
                }
            }
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid selectedVariantUuid = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());

            creator.ReloadModFiles(selectedVariantUuid);
            _UpdateVariantFilesPreview(selectedVariantUuid);
        }

        private void kryptonDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1);

            if (sender is not DataGridView dataGridView)
                return;
            if (dataGridView.Columns[e.ColumnIndex] is not DataGridViewComboBoxColumn || !validClick)
                return;
            dataGridView.BeginEdit(true);
            ((ComboBox)dataGridView.EditingControl).DroppedDown = true;
        }
    }
}
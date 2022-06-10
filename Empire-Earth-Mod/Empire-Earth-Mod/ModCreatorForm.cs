using System;
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

        public ModCreatorForm()
        {
            InitializeComponent();
            mod = new ModData();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabIndex == 0)
            {
                mod.Name = nameKryptonTextBox.Text;
                mod.Description = descriptionKryptonTextBox.Text;
                mod.Version = new Version(versionKryptonTextBox.Text);
                mod.Authors.Add(authorsKryptonTextBox.Text);

                bannersVariantsKryptonComboBox.Items.Clear();
                mod.Variants.Clear();

                foreach (DataGridViewRow row in variantsKryptonDataGridView1.Rows)
                {
                    mod.Variants.Add(Guid.Parse(row.Cells[1].Value.ToString()), row.Cells[0].Value.ToString());
                }

                bannersVariantsKryptonComboBox.Items.AddRange(
                    mod.Variants.Values.Select(x => x.ToString() as object).ToArray());

                tabControl1.SelectTab(1);
            }
            else
            {
            }

            Debug.WriteLine("STEP " + tabControl1.TabIndex);
            Debug.WriteLine(JsonSerializer<ModData>.Serialize(BinarySerializer<ModData>.Deserialize(BinarySerializer<ModData>.Serialize(mod))));
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
            variantsKryptonDataGridView1.SelectedRows.OfType<DataGridViewRow>().ToList()
                .ForEach(x => variantsKryptonDataGridView1.Rows.Remove(x));
        }

        private void iconKryptonButton4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(ofd.FileName))
                        return;
                    var ico = Image.FromFile(ofd.FileName);
                    if (ico.Size != new Size(128, 128))
                    {
                        MessageBox.Show("Icon must be 128x128", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    iconPictureBox.Image = ico;
                    mod.Icon = ico;
                }
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (bannersVariantsKryptonComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a variant to add a banner for it", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Guid currentvariantId = Guid.Parse(mod.Variants.First(value =>
                value.Value.ToString() == bannersVariantsKryptonComboBox.Text).Key.ToString());
            if (!mod.Variants.ContainsKey(currentvariantId))
            {
                MessageBox.Show("Variant not found", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(ofd.FileName))
                        return;
                    var banner = Image.FromFile(ofd.FileName);
                    if (banner.Size != new Size(1280, 720))
                    {
                        MessageBox.Show("Banner must be 1280x720", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    mod.Banners.Add(banner, currentvariantId);
                    kryptonLabel7.Values.ExtraText = mod.Banners.Count.ToString();
                }
            }
        }
    }
}
using System;
using System.Diagnostics;
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
                    mod.Variants.Keys.Select(x => x.ToString() as object).ToArray());

                tabControl1.SelectTab(1);
            }
            else
            {
            }

            Debug.WriteLine(JsonSerializer<ModData>.Serialize(mod));
        }

        private void addVariantKryptonButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(variantKryptonTextBox.Text))
            {
                variantsKryptonDataGridView1.Rows.Add(variantKryptonTextBox.Text, Guid.NewGuid());
            }
        }

        private void removeVariantKryptonButton_Click(object sender, EventArgs e)
        {
            variantsKryptonDataGridView1.SelectedRows.OfType<DataGridViewRow>().ToList()
                .ForEach(x => variantsKryptonDataGridView1.Rows.Remove(x));
        }
    }
}
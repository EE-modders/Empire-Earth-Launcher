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
            mod.Name = nameKryptonTextBox.Text;
            mod.Description = descriptionKryptonTextBox.Text;
            mod.Version = new Version(versionKryptonTextBox.Text);
            mod.Authors.Add(authorsKryptonTextBox.Text);

            Debug.WriteLine(JsonSerializer<ModData>.Serialize(mod));
        }

        private void addVariantKryptonButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(variantKryptonTextBox.Text))
            {
                variantsKryptonDataGridView1.Rows.Add(variantKryptonTextBox.Text, mod.Uuid.ToString());
            }
        }

        private void removeVariantKryptonButton_Click(object sender, EventArgs e)
        {
            variantsKryptonDataGridView1.SelectedRows.OfType<DataGridViewRow>().ToList()
                .ForEach(x => variantsKryptonDataGridView1.Rows.Remove(x));
        }
    }
}
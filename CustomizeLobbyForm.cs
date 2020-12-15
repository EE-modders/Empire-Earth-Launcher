using System.Windows.Forms;

namespace EELauncher
{
    public partial class CustomizeLobbyForm : Form
    {
        public CustomizeLobbyForm(GameHelper gameHelper)
        {
            InitializeComponent();
            if (gameHelper.IsInstalled())
            {

            }
            else
            {

            }
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}

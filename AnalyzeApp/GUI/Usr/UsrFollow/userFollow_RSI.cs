using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_RSI : UserControl
    {
        public userFollow_RSI()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbOption.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

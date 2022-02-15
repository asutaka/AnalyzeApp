using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MACD : UserControl
    {
        public userFollow_MACD()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbOption.SelectedIndex = 0;
        }
        public FollowSetting_MacdModel GetData()
        {
            return new FollowSetting_MacdModel
            {
                IsPositive = cmbOption.SelectedIndex == 0,
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

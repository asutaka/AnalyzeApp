using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_Price : UserControl
    {
        public userFollow_Price()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbOption.SelectedIndex = 0;
            cmbMode.SelectedIndex = 0;
        }

        public FollowSetting_PriceModel GetData()
        {
            return new FollowSetting_PriceModel
            {
                Mode = cmbMode.SelectedIndex,
                IsPositive = cmbOption.SelectedIndex == 0,
                Value = (int)nmValue.Value,
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

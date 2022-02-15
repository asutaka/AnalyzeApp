using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MA : UserControl
    {
        public userFollow_MA()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbOption.SelectedIndex = 0;
            cmbMode.SelectedIndex = 0;
        }

        public FollowSetting_MaModel GetData()
        {
            return new FollowSetting_MaModel
            {
                Mode = cmbMode.SelectedIndex,
                IsPositive = cmbOption.SelectedIndex == 0,
                Value1 = (int)nmValue1.Value,
                Value2 = (int)nmValue2.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

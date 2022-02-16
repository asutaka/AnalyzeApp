using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MACD : UserControl
    {
        public userFollow_MACD(FollowSetting_MacdModel model)
        {
            InitializeComponent();
            InitData(model);
        }
        private void InitData(FollowSetting_MacdModel model)
        {
            if (model == null)
                model = new FollowSetting_MacdModel();
            cmbOption.SelectedIndex = model.Option;
        }
        public FollowSetting_MacdModel GetData()
        {
            return new FollowSetting_MacdModel
            {
                Option = cmbOption.SelectedIndex,
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

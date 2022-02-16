using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_ADX : UserControl
    {
        public userFollow_ADX(FollowSetting_AdxModel model)
        {
            InitializeComponent();
            InitData(model);
        }
        private void InitData(FollowSetting_AdxModel model)
        {
            if (model == null)
                model = new FollowSetting_AdxModel { Value = 20 };
            cmbOption.SelectedIndex = model.Option;
            nmValue.Value = model.Value;
        }

        public FollowSetting_AdxModel GetData()
        {
            return new FollowSetting_AdxModel
            {
                Option = cmbOption.SelectedIndex,
                Value = nmValue.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

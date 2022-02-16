using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_Price : UserControl
    {
        public userFollow_Price(FollowSetting_PriceModel model)
        {
            InitializeComponent();
            InitData(model);
        }

        private void InitData(FollowSetting_PriceModel model)
        {
            if (model == null)
                model = new FollowSetting_PriceModel { Value = 5 };
            cmbOption.SelectedIndex = model.Option;
            cmbMode.SelectedIndex = model.Mode;
            nmValue.Value = model.Value;
        }

        public FollowSetting_PriceModel GetData()
        {
            return new FollowSetting_PriceModel
            {
                Mode = cmbMode.SelectedIndex,
                Option = cmbOption.SelectedIndex,
                Value = nmValue.Value,
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

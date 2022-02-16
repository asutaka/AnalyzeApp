using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MA : UserControl
    {
        public userFollow_MA(FollowSetting_MaModel model)
        {
            InitializeComponent();
            InitData(model);
        }
        private void InitData(FollowSetting_MaModel model)
        {
            if (model == null)
                model = new FollowSetting_MaModel { Value1 = 5, Value2 = 10 };
            cmbOption.SelectedIndex = model.Option;
            cmbMode.SelectedIndex = model.Mode;
            nmValue1.Value = model.Value1;
            nmValue2.Value = model.Value2;
        }

        public FollowSetting_MaModel GetData()
        {
            return new FollowSetting_MaModel
            {
                Mode = cmbMode.SelectedIndex,
                Option = cmbOption.SelectedIndex,
                Value1 = nmValue1.Value,
                Value2 = nmValue2.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

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
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(nmValue, "giá trị");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }
        private void InitData(FollowSetting_AdxModel model)
        {
            if (model == null)
                model = new FollowSetting_AdxModel { Value = 20 };
            cmbOption.SelectedIndex = model.Option;
            nmValue.Value = model.Value;
            nmPoint.Value = model.Point;
        }

        public FollowSetting_AdxModel GetData()
        {
            return new FollowSetting_AdxModel
            {
                Option = cmbOption.SelectedIndex,
                Value = nmValue.Value,
                Point = nmPoint.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

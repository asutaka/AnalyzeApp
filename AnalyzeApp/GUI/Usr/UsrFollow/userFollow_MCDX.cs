using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MCDX : UserControl
    {
        public userFollow_MCDX(FollowSetting_McdxModel model)
        {
            InitializeComponent();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(nmValue, "giá trị");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }

        private void InitData(FollowSetting_McdxModel model)
        {
            if (model == null)
                model = new FollowSetting_McdxModel { Value = 12 };
            cmbOption.SelectedIndex = model.Option;
            nmValue.Value = model.Value;
            nmPoint.Value = model.Point;
        }

        public FollowSetting_McdxModel GetData()
        {
            return new FollowSetting_McdxModel
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

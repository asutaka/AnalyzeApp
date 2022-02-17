using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_Volume : UserControl
    {
        public userFollow_Volume(FollowSetting_VolumeModel model)
        {
            InitializeComponent();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(cmbMode, "chỉ báo");
            toolTip1.SetToolTip(nmValue, "giá trị");
            toolTip1.SetToolTip(nmRatioMax, "biên độ tối đa(%)");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }

        private void InitData(FollowSetting_VolumeModel model)
        {
            if (model == null)
                model = new FollowSetting_VolumeModel { Value = 20 };
            cmbOption.SelectedIndex = model.Option;
            cmbMode.SelectedIndex = model.Mode;
            nmValue.Value = model.Value;
            nmRatioMax.Value = model.RatioMax;
            nmPoint.Value = model.Point;
        }

        public FollowSetting_VolumeModel GetData()
        {
            return new FollowSetting_VolumeModel
            {
                Mode = cmbMode.SelectedIndex,
                Option = cmbOption.SelectedIndex,
                Value = nmValue.Value,
                RatioMax = nmRatioMax.Value,
                Point = nmPoint.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MA : UserControl
    {
        private readonly bool _isFollow;
        public userFollow_MA(FollowSetting_MaModel model, bool isFollow)
        {
            InitializeComponent();
            _isFollow = isFollow;
            InitControl();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(cmbMode, "chỉ báo");
            toolTip1.SetToolTip(nmValue1, "giá trị 1");
            toolTip1.SetToolTip(nmValue2, "giá trị 2");
            toolTip1.SetToolTip(nmRatioMax, "biên độ tối đa(%)");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }
        private void InitControl()
        {
            nmPoint.Visible = !_isFollow;
        }
        private void InitData(FollowSetting_MaModel model)
        {
            if (model == null)
                model = new FollowSetting_MaModel { Value1 = 5, Value2 = 10 };
            cmbOption.SelectedIndex = model.Option;
            cmbMode.SelectedIndex = model.Mode;
            nmValue1.Value = model.Value1;
            nmValue2.Value = model.Value2;
            nmRatioMax.Value = model.RatioMax;
            nmPoint.Value = model.Point;
        }

        public FollowSetting_MaModel GetData()
        {
            return new FollowSetting_MaModel
            {
                Mode = cmbMode.SelectedIndex,
                Option = cmbOption.SelectedIndex,
                Value1 = nmValue1.Value,
                Value2 = nmValue2.Value,
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

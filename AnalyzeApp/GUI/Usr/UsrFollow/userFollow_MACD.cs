using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MACD : UserControl
    {
        private readonly bool _isFollow;
        public userFollow_MACD(FollowSetting_MacdModel model, bool isFollow)
        {
            InitializeComponent();
            _isFollow = isFollow;
            InitControl();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(nmRatioMax, "biên độ tối đa(%)");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }
        private void InitControl()
        {
            nmPoint.Visible = !_isFollow;
        }
        private void InitData(FollowSetting_MacdModel model)
        {
            if (model == null)
                model = new FollowSetting_MacdModel();
            cmbOption.SelectedIndex = model.Option;
            nmRatioMax.Value = model.RatioMax;
            nmPoint.Value = model.Point;
        }
        public FollowSetting_MacdModel GetData()
        {
            return new FollowSetting_MacdModel
            {
                Option = cmbOption.SelectedIndex,
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

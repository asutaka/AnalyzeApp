using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
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
            cmbOption.Properties.BeginUpdate();
            cmbOption.Properties.DataSource = typeof(enumCross).EnumToData();
            cmbOption.Properties.EndUpdate();

            if (model != null)
            {
                cmbOption.EditValue = model.Option;
                nmRatioMax.Value = model.RatioMax;
                nmPoint.Value = model.Point;
            }
            else
            {
                cmbOption.EditValue = (int)enumCross.Cross_Above;
            }
        }
        public FollowSetting_MacdModel GetData()
        {
            return new FollowSetting_MacdModel
            {
                Option = (int)cmbOption.EditValue,
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

using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MCDX : UserControl
    {
        private readonly bool _isFollow;
        private readonly BasicSettingModel _model = Config.BasicSetting;
        public userFollow_MCDX(FollowSetting_McdxModel model, bool isFollow)
        {
            InitializeComponent();
            _isFollow = isFollow;
            InitControl();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(nmValue, "giá trị");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }
        private void InitControl()
        {
            nmPoint.Visible = !_isFollow;
        }
        private void InitData(FollowSetting_McdxModel model)
        {
            cmbOption.Properties.BeginUpdate();
            cmbOption.Properties.DataSource = typeof(enumAboveBelow).EnumToData();
            cmbOption.Properties.EndUpdate();

            if (model != null)
            {
                cmbOption.EditValue = model.Option;
                nmValue.Value = model.Value;
                nmPoint.Value = model.Point;
            }
            else
            {
                cmbOption.EditValue = (int)enumAboveBelow.Above;
                nmValue.Value = _model.MCDX_Value;
            }
        }

        public FollowSetting_McdxModel GetData()
        {
            return new FollowSetting_McdxModel
            {
                Option = (int)cmbOption.EditValue,
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

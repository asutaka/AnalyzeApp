using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_Volume : UserControl
    {
        private readonly bool _isFollow;
        private readonly BasicSettingModel _model = Config.BasicSetting;
        public userFollow_Volume(FollowSetting_VolumeModel model, bool isFollow)
        {
            InitializeComponent();
            _isFollow = isFollow;
            InitControl();
            InitData(model);
            toolTip1.SetToolTip(cmbOption, "tùy chọn");
            toolTip1.SetToolTip(cmbMode, "chỉ báo");
            toolTip1.SetToolTip(nmValue, "giá trị");
            toolTip1.SetToolTip(nmRatioMax, "biên độ tối đa(%)");
            toolTip1.SetToolTip(nmPoint, "điểm");
            toolTip1.SetToolTip(btnDelete, "Xóa");
        }
        private void InitControl()
        {
            nmPoint.Visible = !_isFollow;
        }
        private void InitData(FollowSetting_VolumeModel model)
        {
            cmbOption.Properties.BeginUpdate();
            cmbOption.Properties.DataSource = typeof(enumCross).EnumToData();
            cmbOption.Properties.EndUpdate();

            cmbMode.Properties.BeginUpdate();
            cmbMode.Properties.DataSource = typeof(enumMA).EnumToData();
            cmbMode.Properties.EndUpdate();

            if (model != null)
            {
                cmbOption.EditValue = model.Option;
                cmbMode.EditValue = model.Mode;
                nmValue.Value = model.Value;
                nmRatioMax.Value = model.RatioMax;
                nmPoint.Value = model.Point;
            }
            else
            {
                cmbOption.EditValue = (int)enumCross.Cross_Above;
                cmbMode.EditValue = (int)enumMA.MA;
                nmValue.Value = _model.MA_Value;
            }
        }

        public FollowSetting_VolumeModel GetData()
        {
            return new FollowSetting_VolumeModel
            {
                Mode = (int)cmbMode.EditValue,
                Option = (int)cmbOption.EditValue,
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

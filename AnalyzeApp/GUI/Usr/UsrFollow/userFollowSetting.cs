using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System.Data;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollowSetting : UserControl
    {
        private userFollowSettingDetail user1 = null;
        private userFollowSettingDetail user2 = null;
        private userFollowSettingDetail user3 = null;
        private userFollowSettingDetail user4 = null;
        private userFollowSettingDetail user5 = null;
        private readonly bool _isFollow;
        public FollowSettingModel _model = Config.FollowSetting;
        public userFollowSetting(bool isFollow)
        {
            InitializeComponent();
            _isFollow = isFollow;
            InitData();
        }

        public void ResetConfig()
        {
            user1.ResetConfig();
            user2.ResetConfig();
            user3.ResetConfig();
            user4.ResetConfig();
            user5.ResetConfig();
            cmbFrequency.SelectedIndex = _model.Interval;
            chkState.IsOn = _model.IsNotify;
            tabControl.SelectedTabPage = tp1;
        }

        private void LoadInternalNotify()
        {
            cmbFrequency.Properties.BeginUpdate();
            foreach (DataRow row in typeof(enumIntervalNotify).EnumToData().Rows)
            {
                cmbFrequency.Properties.Items.Add(row["Name"]);
            }
            cmbFrequency.SelectedIndex = 0;
            cmbFrequency.Properties.EndUpdate();
        }

        public FollowSettingModel GetConfigData()
        {
            _model.Interval = cmbFrequency.SelectedIndex;
            _model.IsNotify = chkState.IsOn;
            _model.FollowSettingMode1 = user1.GetData();
            _model.FollowSettingMode2 = user2.GetData();
            _model.FollowSettingMode3 = user3.GetData();
            _model.FollowSettingMode4 = user4.GetData();
            _model.FollowSettingMode5 = user5.GetData();
            return _model;
        }

        private void InitData()
        {
            LoadInternalNotify();
            user1 = new userFollowSettingDetail(1, _isFollow);
            user2 = new userFollowSettingDetail(2, _isFollow);
            user3 = new userFollowSettingDetail(3, _isFollow);
            user4 = new userFollowSettingDetail(4, _isFollow);
            user5 = new userFollowSettingDetail(5, _isFollow);
            
            tp1.AddControl(user1);
            tp2.AddControl(user2);
            tp3.AddControl(user3);
            tp4.AddControl(user4);
            tp5.AddControl(user5);
        }
    }
}

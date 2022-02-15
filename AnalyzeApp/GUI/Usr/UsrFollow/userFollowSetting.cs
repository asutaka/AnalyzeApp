using AnalyzeApp.Common;
using AnalyzeApp.Model.ENUM;
using System.Data;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollowSetting : UserControl
    {
        public userFollowSetting()
        {
            InitializeComponent();
            InitData();
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
        private void InitData()
        {
            LoadInternalNotify();
            tp1.AddControl(new userFollowSettingDetail());
            tp2.AddControl(new userFollowSettingDetail());
            tp3.AddControl(new userFollowSettingDetail());
            tp4.AddControl(new userFollowSettingDetail());
            tp5.AddControl(new userFollowSettingDetail());
        }
    }
}

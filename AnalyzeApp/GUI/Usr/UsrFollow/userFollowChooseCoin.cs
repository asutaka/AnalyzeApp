using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using System;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollowChooseCoin : UserControl
    {
        public FollowSettingModel _model = Config.FollowSetting;
        public userFollowChooseCoin()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbCoin.Properties.ValueMember = "S";
            cmbCoin.Properties.DisplayMember = "AN";
            cmbCoin.Properties.DataSource = StaticVal.lstCoinFilter;
        }

        private void cmbCoin_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCoin.EditValue == null
                || string.IsNullOrWhiteSpace(cmbCoin.EditValue.ToString()))
                return;

            if (_model.Coins.Contains(cmbCoin.EditValue.ToString()))
            {
                MessageBox.Show($"Coin {cmbCoin.EditValue} đã tồn tại trên danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCoin.EditValue = null;
                return;
            }
            _model.Coins.Add(cmbCoin.EditValue.ToString());
            pnl.Controls.Add(new userCoinTrace(cmbCoin.EditValue.ToString()));
            cmbCoin.EditValue = null;
        }
    }
}

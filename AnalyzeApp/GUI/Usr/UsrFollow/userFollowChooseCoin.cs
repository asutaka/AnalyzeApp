using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using System;
using System.Collections.Generic;
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

        public void ResetConfig()
        {
            pnl.Controls.Clear();
            InitData();   
        }

        public List<string> GetCoins()
        {
            var lResult = new List<string>();
            foreach (Control item in pnl.Controls)
            {
                var userModel = item as userCoinTrace;
                lResult.Add(userModel.GetCoin());
            }
            return lResult;
        }

        private void InitData()
        {
            cmbCoin.Properties.ValueMember = "S";
            cmbCoin.Properties.DisplayMember = "AN";
            cmbCoin.Properties.DataSource = StaticVal.lstCoinFilter;
            LoadData();
        }

        private void LoadData()
        {
            if (_model.Coins == null)
                return;
            foreach (var item in _model.Coins)
            {
                pnl.Controls.Add(new userCoinTrace(item));
            }
        }

        private void cmbCoin_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCoin.EditValue == null
                || string.IsNullOrWhiteSpace(cmbCoin.EditValue.ToString()))
                return;

            if (GetCoins().Contains(cmbCoin.EditValue.ToString()))
            {
                MessageBox.Show($"Coin {cmbCoin.EditValue} đã tồn tại trên danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCoin.EditValue = null;
                return;
            }
            pnl.Controls.Add(new userCoinTrace(cmbCoin.EditValue.ToString()));
            cmbCoin.EditValue = null;
        }
    }
}

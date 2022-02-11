using System;
using DevExpress.XtraEditors;
using System.Linq;
using System.Windows.Forms;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Common;
using AnalyzeApp.GUI.Usr;
using Newtonsoft.Json;
using AnalyzeApp.Model.ENUM;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmTradeList : XtraForm
    {
        private readonly TradeListModel _model = Config.TradeList;
        private frmTradeList()
        {
            InitializeComponent();
            InitData();
        }

        private static frmTradeList _instance = null;
        public static frmTradeList Instance()
        {
            _instance = _instance ?? new frmTradeList();
            return _instance;
        }

        private void InitData()
        {
            cmbCoin.Properties.ValueMember = "S";
            cmbCoin.Properties.DisplayMember = "AN";
            cmbCoin.Properties.DataSource = StaticVal.lstCoinFilter;
            SetupData();
        }

        private void SetupData()
        {
            pnl.Controls.Clear();
            foreach (var item in _model.lData)
            {
                pnl.Controls.Add(new userCoinTrade(item));
            }
            chkState.IsOn = _model.IsNotify;
        }

        private void cmbCoin_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCoin.EditValue == null 
                || string.IsNullOrWhiteSpace(cmbCoin.EditValue.ToString()))
                return;

            if(_model.lData.Any(x => x.Coin == cmbCoin.EditValue.ToString()))
            {
                MessageBox.Show($"Coin {cmbCoin.EditValue} đã tồn tại trên danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCoin.EditValue = null;
                return;
            }

            var model = new TradeModel { Coin = cmbCoin.EditValue.ToString() };
            _model.lData.Add(model);
            pnl.Controls.Add(new userCoinTrade(model));
            cmbCoin.EditValue = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetupData();
        }

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            StaticVal.IsTradeListChange = true;
            _model.lData.Clear();
            if (pnl.Controls.Count > 0)
            {
                foreach (var item in pnl.Controls)
                {
                    var user = item as userCoinTrade;

                    _model.lData.Add(user.tradeModel);
                }
            }
            _model.IsNotify = chkState.IsOn;
            Config.TradeList = _model;
            APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.TradeList, Setting = JsonConvert.SerializeObject(_model) }).GetAwaiter().GetResult();
            StaticVal.IsTradeListChange = false;
            MessageBox.Show("Đã lưu dữ liệu!");
        }
    }
}
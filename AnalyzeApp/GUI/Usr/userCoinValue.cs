using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userCoinValue : UserControl
    {
        private readonly double _currentValue;
        public userCoinValue(double currentValue)
        {
            InitializeComponent();
            InitData();
            cmbOption.EditValue = (int)enumAboveBelow.Above;
            _currentValue = currentValue;

            nmValue.Value = (decimal)_currentValue;
        }

        public userCoinValue(TradeDetailModel model)
        {
            InitializeComponent();
            InitData();
            cmbOption.EditValue = model.IsAbove ? (int)enumAboveBelow.Above : (int)enumAboveBelow.Below;
            nmValue.Value = model.Value;
            picOption.Image = model.IsAbove ? Properties.Resources.up_24x24 : Properties.Resources.down_24x24;
        }

        private void InitData()
        {
            cmbOption.Properties.BeginUpdate();
            cmbOption.Properties.DataSource = typeof(enumAboveBelow).EnumToData();
            cmbOption.Properties.EndUpdate();
        }

        public TradeDetailModel GetData()
        {
            return new TradeDetailModel
            {
                IsAbove = (int)cmbOption.EditValue == 1,
                Value = nmValue.Value
            };
        }

        private void lblClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }

        private void cmbOption_EditValueChanged(object sender, System.EventArgs e)
        {
            picOption.Image = (int)cmbOption.EditValue == (int)enumAboveBelow.Above? Properties.Resources.up_24x24 : Properties.Resources.down_24x24;
        }
    }
}

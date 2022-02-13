using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userCoinTrade : UserControl
    {
        public TradeModel tradeModel;
        public userCoinTrade(TradeModel model)
        {
            InitializeComponent();
            tradeModel = model;
            SetData();
        }

        private void SetData()
        {
            var entity = StaticVal.lstCoinFilter.FirstOrDefault(x => x.S == tradeModel.Coin);
            if (entity != null)
            {
                txtCoin.Text = entity.S;
                txtDescription.Text = entity.AN;
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var obj = new frmCoinInfo(this);
            obj.Show();
        }

        private void lblClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
    }
}

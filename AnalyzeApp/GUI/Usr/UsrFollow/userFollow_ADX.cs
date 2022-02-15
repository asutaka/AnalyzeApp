﻿using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_ADX : UserControl
    {
        public userFollow_ADX()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            cmbOption.SelectedIndex = 0;
        }

        public FollowSetting_AdxModel GetData()
        {
            return new FollowSetting_AdxModel
            {
                IsPositive = cmbOption.SelectedIndex == 0,
                Value = (int)nmValue.Value
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

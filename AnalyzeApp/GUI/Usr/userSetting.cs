using AnalyzeApp.Common;
using AnalyzeApp.Model.ENUM;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userSetting : UserControl
    {
        public userSetting()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            LoadDataOption(cmbOptionFirst);
            LoadDataOption(cmbOptionLast);
            LoadDataCandleStick(cmbResultFirst);
            LoadDataCandleStick(cmbResultLast);
            LoadDataOperator();
            LoadDataUnit();
            cmbOptionFirst.SelectedIndex = 0;
            cmbOptionLast.SelectedIndex = -1;
            cmbResultFirst.SelectedIndex = StaticVal.basicModel.ListModel.First(x => x.Indicator == (int)enumCandleStick.C).Period;
            cmbResultLast.SelectedIndex = StaticVal.basicModel.ListModel.First(x => x.Indicator == (int)enumCandleStick.C).Period;
            cmbOperator.SelectedIndex = 3;
            cmbUnit.SelectedIndex = 0;
        }

        private void LoadDataOption(ComboBox cmb)
        {
            cmb.ValueMember = "Id";
            cmb.DisplayMember = "Name";
            cmb.DataSource = typeof(enumOptionTime).EnumToData();
        }

        private void LoadDataCandleStick(ComboBox cmb)
        {
            cmb.ValueMember = "Id";
            cmb.DisplayMember = "Name";
            cmb.DataSource = typeof(enumCandleStick).EnumToData();
        }

        private void LoadDataOperator()
        {
            cmbOperator.ValueMember = "Id";
            cmbOperator.DisplayMember = "Name";
            cmbOperator.DataSource = typeof(enumOperator).EnumToData();
        }

        private void LoadDataUnit()
        {
            cmbUnit.ValueMember = "Id";
            cmbUnit.DisplayMember = "Name";
            cmbUnit.DataSource = typeof(enumUnit).EnumToData();
        }

        private void lblClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
        private void ConfigShow(ComboBox cmb, NumericUpDown nm, ComboBox cmbResult)
        {
            cmbResult.Visible = false;
            nm.Visible = false;
            var index = cmb.SelectedIndex;
            if (index == -1)
                return;
            nm.Value = StaticVal.basicModel.ListModel.First(x => x.Indicator == index).Period;
            if (index == (int)enumChooseData.CandleStick_1 || index == (int)enumChooseData.CandleStick_2)
            {
                cmbResult.SelectedIndex = StaticVal.basicModel.ListModel.First(x => x.Indicator == index).Period;
                cmbResult.Visible = true;
            }
            else if (index == (int)enumChooseData.MA || index == (int)enumChooseData.EMA)
            {
                nm.Visible = true;
            }
        }
        private void cmbOptionFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigShow(cmbOptionFirst, nmResultFirst, cmbResultFirst);
        }

        private void cmbOptionLast_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigShow(cmbOptionLast, nmResultLast, cmbResultLast);
        }
    }
}

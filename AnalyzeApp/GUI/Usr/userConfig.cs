using AnalyzeApp.Common;
using AnalyzeApp.Model.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userConfig : UserControl
    {
        public userConfig()
        {
            InitializeComponent();
            InitData();
        }

        public void ResetConfig()
        {

        }

        private void InitData()
        {
            cmbTime.Properties.BeginUpdate();
            cmbTime.Properties.DataSource = typeof(enumTimeZone).EnumToData();
            cmbTime.Properties.EndUpdate();
            cmbTime.EditValue = (int)enumTimeZone.OneHour;

            cmbTimeZone.Properties.BeginUpdate();
            cmbTimeZone.Properties.DataSource = typeof(enumTimeZone).EnumToData();
            cmbTimeZone.Properties.EndUpdate();
            cmbTimeZone.EditValue = (int)enumTimeZone.OneHour;

            cmbCandleStick.Properties.BeginUpdate();
            cmbCandleStick.Properties.DataSource = typeof(enumCandleStick).EnumToData();
            cmbCandleStick.Properties.EndUpdate();
            cmbCandleStick.EditValue = (int)enumCandleStick.C;
        }
    }
}

using AnalyzeApp.GUI.Usr.UsrFollow;
using AnalyzeApp.Model.ENTITY;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmFollow : XtraForm
    {
        private userFollowChooseCoin usrChosen = new userFollowChooseCoin { Dock = DockStyle.Fill };
        private userFollowSetting usrSetting = new userFollowSetting { Dock = DockStyle.Fill };
        public FollowSettingModel _model = Config.FollowSetting;
        public frmFollow()
        {
            InitializeComponent();
            SetupData();
        }

        private static frmFollow _instance = null;
        public static frmFollow Instance()
        {
            _instance = _instance ?? new frmFollow();
            return _instance;
        }
        private void SetupData()
        {
            pnlMain.Controls.Clear();
            if (chkScreen.IsOn)
                pnlMain.Controls.Add(usrSetting);
            else
                pnlMain.Controls.Add(usrChosen);
        }

        private void chkScreen_Toggled(object sender, EventArgs e)
        {
            SetupData();
        }
    }
}
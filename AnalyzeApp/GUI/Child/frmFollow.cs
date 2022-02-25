using AnalyzeApp.Common;
using AnalyzeApp.GUI.Usr.UsrFollow;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmFollow : XtraForm
    {
        private userFollowChooseCoin usrChosen = null;
        private userFollowSetting usrSetting = null;
        public frmFollow()
        {
            InitializeComponent();
            usrChosen = new userFollowChooseCoin { Dock = DockStyle.Fill };
            usrSetting = new userFollowSetting(true){ Dock = DockStyle.Fill };
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

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            var model = usrSetting.GetConfigData();
            model.Coins = usrChosen.GetCoins();
            if (model.Coins == null)
                model.Coins = new List<string>();
            var bkgr = new BackgroundWorker();
            bkgr.DoWork += (object sender1, DoWorkEventArgs e1) =>
            {
                APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.FollowList, Setting = JsonConvert.SerializeObject(model) }).GetAwaiter().GetResult();
                Config.FollowSetting = model;
                MessageBox.Show("Đã lưu dữ liệu!");
                bkgr.Dispose();
            };
            bkgr.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            usrChosen.ResetConfig();
            usrSetting.ResetConfig();
            SetupData();
        }
    }
}
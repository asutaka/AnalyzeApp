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
            usrSetting = new userFollowSetting { Dock = DockStyle.Fill };
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
            var dicInterval = new Dictionary<int, int>();
            dicInterval.Add(0, 1);
            dicInterval.Add(1, 2);
            dicInterval.Add(2, 5);
            dicInterval.Add(3, 10);
            dicInterval.Add(4, 15);
            dicInterval.Add(5, 60);
            dicInterval.Add(6, 120);
            dicInterval.Add(7, 240);
            dicInterval.Add(8, 300);
            dicInterval.Add(9, 720);
            dicInterval.Add(10, 1440);
            var CronValue = dicInterval[model.Interval];
            if (CronValue < 60)
            {
                model.Cron = $"0 0/{CronValue} * * * ?"; ;
            }
            else if (CronValue < 1440)
            {
                model.Cron = $"0 0 0/{CronValue} * * ?"; ;
            }
            else if (CronValue == 1440)
            {
                model.Cron = $"0 0 0 * * ?"; ;
            }
            else
            {
                model.Cron = "0 * * * * ?"; ;
            }
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
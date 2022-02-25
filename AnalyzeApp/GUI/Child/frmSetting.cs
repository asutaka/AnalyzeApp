using AnalyzeApp.Common;
using AnalyzeApp.GUI.Usr;
using AnalyzeApp.GUI.Usr.UsrFollow;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmSetting : XtraForm
    {
        private userConfig usrConfig = null;
        private userFollowSetting usrSetting = null;
        public frmSetting()
        {
            InitializeComponent();
            usrConfig = new userConfig { Dock = DockStyle.Fill };
            usrSetting = new userFollowSetting(false) { Dock = DockStyle.Fill };
            SetupData();
        }

        private static frmSetting _instance = null;
        public static frmSetting Instance()
        {
            _instance = _instance ?? new frmSetting();
            return _instance;
        }

        private void SetupData()
        {
            pnlMain.Controls.Clear();
            if (chkScreen.IsOn)
                pnlMain.Controls.Add(usrSetting);
            else
                pnlMain.Controls.Add(usrConfig);
        }

        private void chkScreen_Toggled(object sender, EventArgs e)
        {
            SetupData();
        }

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            var basicModel = usrConfig.GetConfigData();
            var advanceModel = usrSetting.GetConfigData();

            var bkgr = new BackgroundWorker();
            bkgr.DoWork += (object sender1, DoWorkEventArgs e1) =>
            {
                APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.BasicSetting, Setting = JsonConvert.SerializeObject(basicModel) }).GetAwaiter().GetResult();
                APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.AdvanceSetting, Setting = JsonConvert.SerializeObject(advanceModel) }).GetAwaiter().GetResult();
                Config.BasicSetting = basicModel;
                Config.AdvanceSetting = advanceModel;
                MessageBox.Show("Đã lưu dữ liệu!");
                bkgr.Dispose();
            };
            bkgr.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            usrConfig.ResetConfig();
            usrSetting.ResetConfig();
            SetupData();
        }
    }
}
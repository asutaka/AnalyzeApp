using System;
using DevExpress.XtraEditors;
using System.Linq;
using System.Windows.Forms;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Common;
using System.ComponentModel;
using System.Threading;
using Quartz;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Job;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Diagnostics;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmWatchList : XtraForm
    {
        private WaitFunc _frmWaitForm = new WaitFunc();
        private BackgroundWorker _bkgr;
        private const int MAXIMUM = 30;
        private int count = 1;

        #region Job
        private ScheduleMember job = new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<WatchListScheduleJob>(), StaticVal.Scron_WatchList, nameof(WatchListScheduleJob));
        #endregion
        #region Contructor
        private frmWatchList()
        {
            InitializeComponent();
            this.Enabled = false;
            _bkgr = new BackgroundWorker();
            _bkgr.DoWork += bkgrInitData_DoWork;
            _bkgr.RunWorkerCompleted += bkgrInitData_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }
        private static frmWatchList _instance = null;
        public static frmWatchList Instance()
        {
            _instance = _instance ?? new frmWatchList();
            return _instance;
        }
        #endregion
        #region Function
        public void InitData()
        {
            if (!this.Visible)
            {
                job.Pause();
            }
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                grid.BeginUpdate();
                grid.DataSource = StaticVal.lstRealTimeDisplay;
                grid.EndUpdate();
            });
        }
        private void AddNewRow(string coin, string coinName)
        {
            StaticVal.lstRealTimeDisplay.Add(new Top30Model { 
                STT = count++,
                Coin = coin,
                CoinName = coinName,
                Count = 0,
                Rate = 0,
                RefValue = 0,
                Value = 0,
                BottomRecent = 0,
                RateValue = 0,
                WaveRecent = 0,
                CountTime = 0
            });
            var tmp = 1;
        }
        #endregion
        #region Event
        private void bkgrInitData_DoWork(object sender, DoWorkEventArgs e)
        {
            _frmWaitForm.Show("Đang xử lý dữ liệu");
            if (Config.RealTimes.Count() > MAXIMUM)
                Config.RealTimes = Config.RealTimes.Take(MAXIMUM).ToList();

            foreach (var item in Config.RealTimes)
            {
                AddNewRow(item.S, item.AN);
            }
            Thread.Sleep(200);
            _frmWaitForm.Close();
        }
        private void bkgrInitData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmbCoin.Properties.ValueMember = "S";
            cmbCoin.Properties.DisplayMember = "AN";
            cmbCoin.Properties.DataSource = StaticVal.lstCoinFilter;
            this.Enabled = true;
            InitData();
        }
        private void cmbCoin_EditValueChanged(object sender, EventArgs e1)
        {
            if (cmbCoin.EditValue == null
                || string.IsNullOrWhiteSpace(cmbCoin.EditValue.ToString()))
                return;

            if (Config.RealTimes.Any(x => x.S == cmbCoin.EditValue.ToString()))
            {
                MessageBox.Show($"Coin {cmbCoin.EditValue} đã tồn tại trên danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCoin.EditValue = null;
                return;
            }

            if (Config.RealTimes.Count() >= MAXIMUM)
            {
                MessageBox.Show($"Số lượng phần tử đã vượt quá chỉ định!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCoin.EditValue = null;
                return;
            }
            StaticVal.IsRealTimeAction = true;
            var coin = cmbCoin.EditValue.ToString();
            var coinName = cmbCoin.Text;
            Config.RealTimes.Add(new CryptonDetailDataModel
            {
                S = coin,
                AN = coinName
            });
            APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.RealtimeList, Setting = JsonConvert.SerializeObject(Config.RealTimes) });
            AddNewRow(coin, coinName);
            cmbCoin.EditValue = null;
            StaticVal.IsRealTimeAction = false;
        }
        private void frmRealTime_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                job.Pause();
            }
            else
            {
                if (!job.IsStarted())
                {
                    job.Start();
                }
                else
                {
                    job.Resume();
                }
            }
        }
        #endregion

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = gridView1.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                var cellValue = gridView1.GetRowCellValue(info.RowHandle, "Coin").ToString();
                ProcessStartInfo sInfo = new ProcessStartInfo($"{ConstVal.COIN_SINGLE}{cellValue.Replace("USDT", "_USDT")}");
                Process.Start(sInfo);
            }
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                StaticVal.IsRealTimeAction = true;
                int[] rows = gridView1.GetSelectedRows();
                if (rows != null && rows.Length > 0)
                {
                    if (MessageBox.Show("Xóa coin được chọn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _frmWaitForm.Show("Đang xử lý dữ liệu");
                        grid.Enabled = false;
                        Thread.Sleep(1000);
                        var cellValue = gridView1.GetRowCellValue(rows[0], "Coin").ToString();
                        var entity = Config.RealTimes.FirstOrDefault(x => x.S == cellValue);
                        if (entity != null)
                        {
                            Config.RealTimes.Remove(entity);
                        }
                        APIService.Instance().UpdateSetting(new SettingModel { Id = (int)enumSetting.RealtimeList, Setting = JsonConvert.SerializeObject(Config.RealTimes)});
                        var entityShow = StaticVal.lstRealTimeDisplay.FirstOrDefault(x => x.Coin == cellValue);
                        if (entityShow != null)
                        {
                            StaticVal.lstRealTimeDisplay.Remove(entityShow);
                        }
                        InitData();
                        StaticVal.IsRealTimeAction = false;
                        grid.Enabled = true;
                        _frmWaitForm.Close();
                    }
                   
                }
            }
        }
    }
}
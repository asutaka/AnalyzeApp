using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.GUI.Child;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraTab;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeApp.GUI
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private WaitFunc _frmWaitForm = new WaitFunc();
        private BackgroundWorker _bkgr;
        private frmMain()
        {
            InitializeComponent();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin");
            StaticVal.IsAccessMain = true;
            ribbon.Enabled = false;
            _bkgr = new BackgroundWorker();
            _bkgr.DoWork += bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
            StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
            StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<TradeListNotifyJob>(), StaticVal.Scron_TradeList_Noti, nameof(TradeListNotifyJob)));


            //StaticValues.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticValues.ScheduleMngObj.GetScheduler(), JobBuilder.Create<FollowListJob>(), StaticValues.followList.Cron, nameof(FollowListJob)));
        }

        private static frmMain _instance = null;
        public static frmMain Instance()
        {
            _instance = _instance ?? new frmMain();
            return _instance;
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            _frmWaitForm.Show("Thiết lập ban đầu");
            DataMng.StoredData(enumInterval.OneHour).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 1H complete";
            });
            
            var config = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if(config == null
                || config.StatusLoadData != (int)enumStatusLoadData.Complete1H)
            {
                Thread.Sleep(200);
                _frmWaitForm.Close();
                MessageBox.Show("Lỗi load dữ liệu không thành công!");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }

            var lstTask = new List<Task>();
            //1H
            Dictionary<string, IEnumerable<BinanceKline>> dic1H = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic1H.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneHour));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic1H = dic1H;
            _frmWaitForm.Close();
        }
        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bkgr.DoWork -= bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrConfig_RunWorkerCompleted;
            _bkgr.DoWork += bkgrAnalyze_DoWork;
            _bkgr.RunWorkerCompleted += bkgrAnalyze_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }
        private void bkgrAnalyze_DoWork(object sender, DoWorkEventArgs e)
        {
            _frmWaitForm.Show("Phân tích dữ liệu");
            StaticVal.lstCryptonRank = CalculateMng.Top30();
            Thread.Sleep(200);
            _frmWaitForm.Close();
        }
        private void bkgrAnalyze_RunWorkerCompleted(object sender1, RunWorkerCompletedEventArgs e1)
        {
            ribbon.Enabled = true;
            _bkgr.DoWork -= bkgrAnalyze_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrAnalyze_RunWorkerCompleted;
            _bkgr.DoWork += bkgrStoreOther_DoWork;
            _bkgr.RunWorkerCompleted += bkgrStoreOther_RunWorkerCompleted;
            tabControl.AddTab(frmTop30.Instance());
            StartUp.Load();
            _bkgr.RunWorkerAsync();
        }

        private void bkgrStoreOther_DoWork(object sender, DoWorkEventArgs e)
        {
            var lstTask = new List<Task>();
            #region 15M
            DataMng.StoredData(enumInterval.FifteenMinute).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Store data 15M complete";
            });
            var config15M = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if (config15M == null
                || config15M.StatusLoadData != (int)enumStatusLoadData.Complete15M)
            {
                MessageBox.Show("Lỗi load dữ liệu không thành công!");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
            lstTask.Clear();
            Dictionary<string, IEnumerable<BinanceKline>> dic15M = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic15M.Add(item.S, DataMng.LoadSource(item.S, enumInterval.FifteenMinute));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic15M = dic15M;
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 15M complete";
            });
            #endregion
            #region 4H
            DataMng.StoredData(enumInterval.FourHour).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Store data 4H complete";
            });
            var config4H = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if (config4H == null
                || config4H.StatusLoadData != (int)enumStatusLoadData.Complete4H)
            {
                MessageBox.Show("Lỗi load dữ liệu không thành công!");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
            lstTask.Clear();
            Dictionary<string, IEnumerable<BinanceKline>> dic4H = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic4H.Add(item.S, DataMng.LoadSource(item.S, enumInterval.FourHour));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic4H = dic4H;
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 4H complete";
            });
            #endregion
            #region 1D
            DataMng.StoredData(enumInterval.OneDay).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Store data 1D complete";
            });
            var config1D = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if (config1D == null
                || config1D.StatusLoadData != (int)enumStatusLoadData.Complete1D)
            {
                MessageBox.Show("Lỗi load dữ liệu không thành công!");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
            lstTask.Clear();
            Dictionary<string, IEnumerable<BinanceKline>> dic1D = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic1D.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneDay));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic1D = dic1D;
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 1D complete";
            });
            #endregion
            #region 1W
            DataMng.StoredData(enumInterval.OneWeek).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Store data 1W complete";
            });
            var config1W = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if (config1W == null
                || config1W.StatusLoadData != (int)enumStatusLoadData.Complete1W)
            {
                MessageBox.Show("Lỗi load dữ liệu 1 tuần không thành công!");
            }
            lstTask.Clear();
            Dictionary<string, IEnumerable<BinanceKline>> dic1W = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic1W.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneWeek));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic1W = dic1W;
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 1W complete";
            });
            #endregion
            #region 1W
            DataMng.StoredData(enumInterval.OneMonth).GetAwaiter().GetResult();
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Store data 1M complete";
            });
            var config1M = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
            if (config1M == null
                || config1M.StatusLoadData != (int)enumStatusLoadData.Complete1W)
            {
                MessageBox.Show("Lỗi load dữ liệu 1 tháng không thành công!");
            }
            lstTask.Clear();
            Dictionary<string, IEnumerable<BinanceKline>> dic1M = new Dictionary<string, IEnumerable<BinanceKline>>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    dic1M.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneMonth));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            StaticVal.dic1Month = dic1M;
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Caption = "Load data 1M complete";
            });
            #endregion
            lblStatus.Caption = "Load all data complete";
        }
        private void bkgrStoreOther_RunWorkerCompleted(object sender1, RunWorkerCompletedEventArgs e1)
        {
            _bkgr.DoWork -= bkgrStoreOther_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrStoreOther_RunWorkerCompleted;
        }

        private void barBtnInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProfile.Instance().Show();
        }

        private void barBtnTop30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmTop30.Instance());
            });
        }

        private void barBtnListTrade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmTradeList.Instance());
            });
        }

        private void barBtnListFollow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmFollow.Instance());
            });
        }

        private void barBtnBlackList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmBlackList.Instance());
            });
        }

        private void barBtnConfigFx_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmSetting.Instance());
            });
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*kill all running process
            * https://stackoverflow.com/questions/8507978/exiting-a-c-sharp-winforms-application
            */
            try
            {
                foreach (var process in Process.GetProcessesByName(ConstVal.serviceName))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain: {ex.Message}");
            }
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            Environment.Exit(0);
        }

        private void tabControl_CloseButtonClick(object sender, EventArgs e)
        {
            //if (tabControl.TabPages.Count == 1)
            //    return;
            var EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//Get the text of the closed tab
            foreach (XtraTabPage page in tabControl.TabPages)//Traverse to get the same text as the closed tab
            {
                if (page.Text == name)
                {
                    tabControl.TabPages.Remove(page);
                    return;
                }
            }
        }

        private void barBtnRealTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmWatchList.Instance());
            });
        }

        private void barBtnStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = false;
            barBtnStop.Enabled = true;

            StaticVal.scheduleMng.StartAllJob();
        }

        private void barBtnStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = true;
            barBtnStop.Enabled = false;

            StaticVal.scheduleMng.StopAllJob();
        }

        private void barBtnMCDX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmMCDX.Instance());
            });
        }
    }
}
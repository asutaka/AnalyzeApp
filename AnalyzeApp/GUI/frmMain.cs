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
            ribbon.Enabled = false;
            StaticVal.IsAccessMain = true;
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
            while (true)
            {
                var config = APIService.Instance().GetConfigTable().GetAwaiter().GetResult();
                if(config != null && config.StatusLoadData != (int)enumStatusLoadData.Loading)
                {
                    Dictionary<string, IEnumerable<BinanceKline>> dic15M = new Dictionary<string, IEnumerable<BinanceKline>>();
                    Dictionary<string, IEnumerable<BinanceKline>> dic1H = new Dictionary<string, IEnumerable<BinanceKline>>();
                    Dictionary<string, IEnumerable<BinanceKline>> dic4H = new Dictionary<string, IEnumerable<BinanceKline>>();
                    Dictionary<string, IEnumerable<BinanceKline>> dic1D = new Dictionary<string, IEnumerable<BinanceKline>>();
                    Dictionary<string, IEnumerable<BinanceKline>> dic1W = new Dictionary<string, IEnumerable<BinanceKline>>();
                    Dictionary<string, IEnumerable<BinanceKline>> dic1Month = new Dictionary<string, IEnumerable<BinanceKline>>();

                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            dic15M.Add(item.S, DataMng.LoadSource(item.S, enumInterval.FifteenMinute));
                            dic1H.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneHour));
                            dic4H.Add(item.S, DataMng.LoadSource(item.S, enumInterval.FourHour));
                            dic1D.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneDay));
                            dic1W.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneWeek));
                            dic1Month.Add(item.S, DataMng.LoadSource(item.S, enumInterval.OneMonth));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                    StaticVal.dic15M = dic15M;
                    StaticVal.dic1H = dic1H;
                    StaticVal.dic4H = dic4H;
                    StaticVal.dic1D = dic1D;
                    StaticVal.dic1W = dic1W;
                    StaticVal.dic1Month = dic1Month;

                    Thread.Sleep(200);
                    break;
                }
                Thread.Sleep(1000);
            }

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

            tabControl.AddTab(frmTop30.Instance());
            StartUp.Load();
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
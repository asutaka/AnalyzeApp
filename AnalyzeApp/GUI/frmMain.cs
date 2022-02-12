using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.Data;
using AnalyzeApp.GUI.Child;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraTab;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeApp.GUI
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private WaitFunc _frmWaitForm = new WaitFunc();
        private BackgroundWorker _bkgr;
        //private bool _checkConnection;
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
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<TradeListNotifyJob>(), StaticVal.Scron_TradeList_Noti, nameof(TradeListNotifyJob)));


            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<CurrentDataJob>(), "* * * * * ?", nameof(CurrentDataJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data15MJob>(), "0 0/15 * * * ?", nameof(Data15MJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data1HJob>(), "0 0 0/1 * * ?", nameof(Data1HJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data4HJob>(), "0 0 0/4 * * ?", nameof(Data4HJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data1DJob>(), "0 0 0 * * ?", nameof(Data1DJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data1WJob>(), "0 0 0 1/7 * ?", nameof(Data1WJob)));
            //StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<Data1WJob>(), "0 0 0 1 * ?", nameof(Data1WJob)));
            //StaticVal.ScheduleMngObj.StartAllJob();

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
            //dtStartConfig = DateTime.Now;
            _frmWaitForm.Show("Thiết lập ban đầu");
            var lstTask = new List<Task>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    StaticVal.dicDatasource1H.Add(item.S, SeedData.LoadDatasource(item.S, enumInterval.OneHour));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            Thread.Sleep(200);
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
            //dtStartCalculate = DateTime.Now;
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
            _bkgr.DoWork += bkgrPrepareRealTime_DoWork;
            _bkgr.RunWorkerCompleted += bkgrPrepareRealTime_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }

        private void bkgrPrepareRealTime_DoWork(object sender1, DoWorkEventArgs e1)
        {
            //15M
            if (Config.AdvanceSetting1.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
                || Config.AdvanceSetting2.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
                || Config.AdvanceSetting3.LstInterval.Contains((int)enumTimeZone.ThirteenMinute)
                || Config.AdvanceSetting4.LstInterval.Contains((int)enumTimeZone.ThirteenMinute))
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            StaticVal.dicDatasource15M.Add(item.S, SeedData.LoadDatasource(item.S, (int)enumInterval.ThirteenMinute));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                };
                wrkr.RunWorkerAsync();
            }
            //4H
            if (Config.AdvanceSetting1.LstInterval.Contains((int)enumTimeZone.FourHour)
              || Config.AdvanceSetting2.LstInterval.Contains((int)enumTimeZone.FourHour)
              || Config.AdvanceSetting3.LstInterval.Contains((int)enumTimeZone.FourHour)
              || Config.AdvanceSetting4.LstInterval.Contains((int)enumTimeZone.FourHour))
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            StaticVal.dicDatasource4H.Add(item.S, SeedData.LoadDatasource(item.S, enumInterval.FourHour));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                };
                wrkr.RunWorkerAsync();
            }
            //1D
            if (Config.AdvanceSetting1.LstInterval.Contains((int)enumTimeZone.OneDay)
              || Config.AdvanceSetting2.LstInterval.Contains((int)enumTimeZone.OneDay)
              || Config.AdvanceSetting3.LstInterval.Contains((int)enumTimeZone.OneDay)
              || Config.AdvanceSetting4.LstInterval.Contains((int)enumTimeZone.OneDay))
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            StaticVal.dicDatasource1D.Add(item.S, SeedData.LoadDatasource(item.S, enumInterval.OneDay));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                };
                wrkr.RunWorkerAsync();
            }
            //1W
            if (Config.AdvanceSetting1.LstInterval.Contains((int)enumTimeZone.OneWeek)
              || Config.AdvanceSetting2.LstInterval.Contains((int)enumTimeZone.OneWeek)
              || Config.AdvanceSetting3.LstInterval.Contains((int)enumTimeZone.OneWeek)
              || Config.AdvanceSetting4.LstInterval.Contains((int)enumTimeZone.OneWeek))
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            StaticVal.dicDatasource1W.Add(item.S, SeedData.LoadDatasource(item.S, enumInterval.OneWeek));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                };
                wrkr.RunWorkerAsync();
            }
            //1Month
            if (Config.AdvanceSetting1.LstInterval.Contains((int)enumTimeZone.OneMonth)
              || Config.AdvanceSetting2.LstInterval.Contains((int)enumTimeZone.OneMonth)
              || Config.AdvanceSetting3.LstInterval.Contains((int)enumTimeZone.OneMonth)
              || Config.AdvanceSetting4.LstInterval.Contains((int)enumTimeZone.OneMonth))
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    var lstTask = new List<Task>();
                    foreach (var item in StaticVal.lstCoinFilter)
                    {
                        var task = Task.Run(() =>
                        {
                            StaticVal.dicDatasource1Month.Add(item.S, SeedData.LoadDatasource(item.S, enumInterval.OneMonth));
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                };
                wrkr.RunWorkerAsync();
            }
        }

        private void bkgrPrepareRealTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmTop30.Instance());
            });
            try
            {
                foreach (var process in Process.GetProcessesByName(ConstVal.serviceName))
                {
                    process.Kill();
                }
                Process.Start($"{Directory.GetCurrentDirectory()}\\service\\{ConstVal.serviceName}.exe");
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain: {ex.Message}");
            }
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
            //var tmp = StaticValues.ScheduleMngObj.GetSchedules().ElementAt(0);
            //if (tmp.IsStarted())
            //{
            //    tmp.Pause();
            //    tmp.Resume();
            //}
            //else
            //{
            //    tmp.Start();
            //}
            this.Invoke((MethodInvoker)delegate
            {
                tabControl.AddTab(frmFollowList.Instance());
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
                tabControl.AddTab(frmConfigFx.Instance());
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
                tabControl.AddTab(frmRealTime.Instance());
            });
        }

        private void barBtnStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = false;
            barBtnStop.Enabled = true;

            StaticVal.ScheduleMngObj.StartAllJob();
        }

        private void barBtnStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barBtnStart.Enabled = true;
            barBtnStop.Enabled = false;

            StaticVal.ScheduleMngObj.StopAllJob();
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
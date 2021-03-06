using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Quartz;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmMCDX : XtraForm
    {
        private ScheduleMember jobCalculate = new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<MCDXCalculateJob>(), StaticVal.Scron_MCDX_Calculate, nameof(MCDXCalculateJob));
        private ScheduleMember jobValue = new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<MCDXValueScheduleJob>(), StaticVal.Scron_MCDX_Value, nameof(MCDXValueScheduleJob));
        private frmMCDX()
        {
            InitializeComponent();
        }

        private static frmMCDX _instance = null;
        public static frmMCDX Instance()
        {
            _instance = _instance ?? new frmMCDX();
            return _instance;
        }
        private void Init()
        {
            try
            {
                if (StaticVal.IsExecMCDX)
                    return;
                StaticVal.IsExecMCDX = true;
                StaticVal.lstMCDX = CalculateMng.MCDX();
                InitData();
                StaticVal.IsExecMCDX = false;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMCDX:Init: {ex.Message}");
            }
        }
        public void InitData()
        {
            if (!this.Visible)
            {
                jobCalculate.Pause();
                jobValue.Pause();
            }
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                grid.BeginUpdate();
                grid.DataSource = StaticVal.lstMCDX;
                grid.EndUpdate();
            });
        }

        private void frmMCDX_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                jobCalculate.Pause();
                jobValue.Pause();
            }
            else
            {
                if (!jobCalculate.IsStarted())
                {
                    jobCalculate.Start();
                }
                else
                {
                    jobCalculate.Resume();
                }
                if (!jobValue.IsStarted())
                {
                    jobValue.Start();
                }
                else
                {
                    jobValue.Resume();
                }
            }
        }

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

        private void frmMCDX_Load(object sender1, EventArgs e1)
        {
            try
            {
                var wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                    Init();
                    wrkr.Dispose();
                };
                wrkr.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMCDX:frmMCDX_Load: {ex.Message}");
            }
        }
    }
}
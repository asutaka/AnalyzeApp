using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Quartz;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Child
{
    public partial class frmTop30 : XtraForm
    {
        private ScheduleMember jobValue = new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Top30ScheduleJob>(), StaticVal.Scron_Top30, nameof(Top30ScheduleJob));
        private frmTop30()
        {
            InitializeComponent();
            InitData();
        }

        private static frmTop30 _instance = null;
        public static frmTop30 Instance()
        {
            _instance = _instance ?? new frmTop30();
            return _instance;
        }

        public void InitData()
        {
            if (!this.Visible)
            {
                jobValue.Pause();
            }
            if (!this.IsHandleCreated)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                grid.BeginUpdate();
                grid.DataSource = StaticVal.lstCryptonRank;
                grid.EndUpdate();
            });
        }

        private void frmTop30_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                jobValue.Pause();
            }
            else
            {
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
    }
}
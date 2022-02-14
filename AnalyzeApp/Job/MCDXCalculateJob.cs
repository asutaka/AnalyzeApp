using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.GUI.Child;
using Quartz;
using System;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class MCDXCalculateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (StaticVal.IsExecMCDX)
                    return;
                StaticVal.IsExecMCDX = true;
                StaticVal.lstMCDX = CalculateMng.MCDX();
                frmMCDX.Instance().InitData();
                StaticVal.IsExecMCDX = false;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"MCDXCalculateJob:Execute: {ex.Message}");
            }
        }
    }
}

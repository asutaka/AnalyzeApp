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
                if (StaticValtmp.IsExecMCDX)
                    return;
                StaticValtmp.IsExecMCDX = true;
                StaticValtmp.lstMCDX = CalculateMng.MCDX();
                frmMCDX.Instance().InitData();
                StaticValtmp.IsExecMCDX = false;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"MCDXCalculateJob:Execute: {ex.Message}");
            }
        }
    }
}

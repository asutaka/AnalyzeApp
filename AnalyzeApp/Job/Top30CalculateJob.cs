using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.GUI.Child;
using Quartz;
using System;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class Top30CalculateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (StaticVal.IsExecTop30)
                    return;
                StaticVal.IsExecTop30 = true;
                StaticVal.lstCryptonRank = CalculateMng.Top30();
                frmTop30.Instance().InitData();
                StaticVal.IsExecTop30 = false;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"Top30CalculateJob:Execute: {ex.Message}");
            }
        }
    }
}

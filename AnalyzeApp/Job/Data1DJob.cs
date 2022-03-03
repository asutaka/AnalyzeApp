using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    internal class Data1DJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data1DJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(3721);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.OneDay);
                StaticVal.dic1D = dicResult;
                
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1DJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data1DJob End!");
        }
    }
}

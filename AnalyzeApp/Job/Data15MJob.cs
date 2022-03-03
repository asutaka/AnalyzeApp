using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    internal class Data15MJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data15MJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(1553);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.FifteenMinute);
                StaticVal.dic15M = dicResult;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data15MJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data15MJob End!");
        }
    }
}

using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    internal class Data1HJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data1HJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(1907);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.OneHour);
                StaticVal.dic1H = dicResult;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1HJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data1HJob End!");
        }
    }
}

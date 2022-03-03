using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    internal class Data4HJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data4HJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(2653);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.FourHour);
                StaticVal.dic4H = dicResult;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data4HJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data4HJob End!");
        }
    }
}

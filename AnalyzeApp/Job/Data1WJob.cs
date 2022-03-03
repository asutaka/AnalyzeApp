using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    internal class Data1WJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data1WJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(3824);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.OneWeek);
                StaticVal.dic1W = dicResult;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1WJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data1WJob End!");
        }
    }
}

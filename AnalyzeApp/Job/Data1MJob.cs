using AnalyzeApp.Common;
using Quartz;
using System;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class Data1MJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NLogLogger.LogInfo($"{DateTime.Now}: Data1MJob Start!");
            try
            {
                do
                {
                    Thread.Sleep(4353);
                }
                while (StaticVal.IsSyncData);

                StaticVal.IsSyncData = true;
                var dicResult = DataMng.SyncData(Model.ENUM.enumInterval.OneMonth);
                StaticVal.dic1Month = dicResult;
               
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1MJob|Execute: {ex.Message}");
            }
            StaticVal.IsSyncData = false;
            NLogLogger.LogInfo($"{DateTime.Now}: Data1MJob End!");
        }
    }
}

using Quartz;

namespace AnalyzeApp.Job
{
    internal class CurrentDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            StaticVal.binanceTicks = StaticVal.binanceClient.Spot.Market.GetTickersAsync().GetAwaiter().GetResult().Data;
        }
    }
}

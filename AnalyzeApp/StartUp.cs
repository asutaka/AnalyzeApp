using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp
{
    public class StartUp
    {
        public static async Task Load()
        {
            try
            {
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data15MJob>(), "0 0/15 * * * ?", nameof(Data15MJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1HJob>(), "0 0 0/1 * * ?", nameof(Data1HJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data4HJob>(), "0 0 0/4 * * ?", nameof(Data4HJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1DJob>(), "0 0 0 * * ?", nameof(Data1DJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1WJob>(), "0 0 0 1/7 * ?", nameof(Data1WJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1MJob>(), "0 0 0 1 * ?", nameof(Data1MJob)).Start();

                var ticket = (await StaticVal.binanceClient.Spot.Market.GetTickersAsync()).Data.ToList();
                StaticVal.binanceTicks = ticket;
                var binanceTick = ticket;
                var subscribeResult = StaticVal.binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data => {
                    // Handle data when it is received
                    var lExists = binanceTick.Where(x => data.Data.Any(y => y.Symbol == x.Symbol));
                    if (lExists != null && lExists.Any())
                    {
                        binanceTick = binanceTick.Except(lExists).ToList();
                    }
                    binanceTick.AddRange(data.Data);
                    StaticVal.binanceTicks = binanceTick;
                });
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"StartUp|Load:{ex.Message}");
            }
        }
    }
}

using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using Quartz;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyzeApp
{
    public class StartUp
    {
        public static async Task Load()
        {
            try
            {
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data15MJob>(), "0 0/15 * * * ?", nameof(Data15MJob)).Start();
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1HJob>(), "5 0 0/1 * * ?", nameof(Data1HJob)).Start();
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data4HJob>(), "10 0 0/4 * * ?", nameof(Data4HJob)).Start();
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1DJob>(), "20 0 0 * * ?", nameof(Data1DJob)).Start();
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1WJob>(), "30 0 0 1/7 * ?", nameof(Data1WJob)).Start();
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1MJob>(), "40 0 0 1 * ?", nameof(Data1MJob)).Start();

                var ticket = await StaticVal.binanceClient.SpotApi.ExchangeData.GetTickersAsync();
                StaticVal.binanceTicks = ticket.Data.ToList();
                var binanceTick = StaticVal.binanceTicks;
                var isLock = false;
                var subscribeResult = await StaticVal.binanceSocketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(data =>
                {
                    if (!isLock)
                    {
                        isLock = true;
                        var lData = data.Data.ToList();
                        var lExists = binanceTick.Where(x => lData.Any(y => y.Symbol == x.Symbol));
                        if (lExists != null && lExists.Any())
                        {
                            binanceTick = binanceTick.Except(lExists).ToList();
                        }
                        binanceTick.AddRange(lData);
                        StaticVal.binanceTicks = binanceTick;
                        isLock = false;
                    }
                });

                //var isLog = true;
                //var dt1 = DateTime.Now;
                //BackgroundWorker wrkr = new BackgroundWorker();
                //wrkr.DoWork += (object sender, DoWorkEventArgs e) =>
                //{
                //    Console.WriteLine($"phunv: START {dt1}");
                //    while (true)
                //    {
                //        if (StaticVal.binanceSocketClient.IncomingKbps > 0 && !isLog)
                //            isLog = true;
                //        if (isLog)
                //        {
                //            Console.WriteLine($"phunv_time: {DateTime.Now}, byte:{StaticVal.binanceSocketClient.IncomingKbps}");
                //            var div = (DateTime.Now - dt1).TotalSeconds;
                //            if (StaticVal.binanceSocketClient.IncomingKbps == 0 && div > 60)
                //            {
                //                isLog = false;
                //                Console.WriteLine($"phunv: END {div} at {DateTime.Now}");
                //            }
                //        }
                //        Thread.Sleep(1000);
                //    }
                //};
                //wrkr.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"StartUp|Load:{ex.Message}");
            }
        }
    }
}

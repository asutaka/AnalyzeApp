using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using Binance.Net;
using Binance.Net.Objects;
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
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data15MJob>(), "0 0/15 * * * ?", nameof(Data15MJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1HJob>(), "0 0 0/1 * * ?", nameof(Data1HJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data4HJob>(), "0 0 0/4 * * ?", nameof(Data4HJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1DJob>(), "0 0 0 * * ?", nameof(Data1DJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1WJob>(), "0 0 0 1/7 * ?", nameof(Data1WJob)).Start();
                new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<Data1MJob>(), "0 0 0 1 * ?", nameof(Data1MJob)).Start();
               
                //BackgroundWorker wrkr = new BackgroundWorker();
                //wrkr.DoWork += (object sender, DoWorkEventArgs e) =>
                //{
                //    var ticket = StaticVal.binanceClient.Spot.Market.GetTickersAsync().GetAwaiter().GetResult();
                //    StaticVal.binanceTicks = ticket.Data.ToList();
                //    wrkr.Dispose();
                //};
                //wrkr.RunWorkerAsync();
                //Thread.Sleep(2000);
                //new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<SubcribeJob>(), "0/10 * * * * ?", nameof(SubcribeJob)).Start();

                BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
                {
                    AutoReconnect = true,
                    ReconnectInterval = TimeSpan.FromMinutes(1),
                });
                var ticket = await StaticVal.binanceClient.Spot.Market.GetTickersAsync();
                StaticVal.binanceTicks = ticket.Data.ToList();
                var binanceTick = StaticVal.binanceTicks;
                var isLock = false;
                var subscribeResult = await StaticVal.binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data =>
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

                //var ticket = (await StaticVal.binanceClient.Spot.Market.GetTickersAsync()).Data.ToList();
                //StaticVal.binanceTicks = ticket;
                //var binanceTick = ticket;
                //var subscribeResult = StaticVal.binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data => {
                //    // Handle data when it is received
                //    var lExists = binanceTick.Where(x => data.Data.Any(y => y.Symbol == x.Symbol));
                //    if (lExists != null && lExists.Any())
                //    {
                //        binanceTick = binanceTick.Except(lExists).ToList();
                //    }
                //    binanceTick.AddRange(data.Data);
                //    StaticVal.binanceTicks = binanceTick;
                //});
                var isLog = true;
                var dt1 = DateTime.Now;
                BackgroundWorker wrkr = new BackgroundWorker();
                wrkr.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    Console.WriteLine($"phunv: START {dt1}");
                    while (true)
                    {
                        if (StaticVal.binanceSocketClient.IncomingKbps > 0 && !isLog)
                            isLog = true;
                        if (isLog)
                        {
                            Console.WriteLine($"phunv_time: {DateTime.Now}, byte:{StaticVal.binanceSocketClient.IncomingKbps}");
                            var div = (DateTime.Now - dt1).TotalSeconds;
                            if (StaticVal.binanceSocketClient.IncomingKbps == 0 && div > 60)
                            {
                                isLog = false;
                                Console.WriteLine($"phunv: END {div} at {DateTime.Now}");
                            }
                        }
                        //if (StaticVal.binanceSocketClient.IncomingKbps == 0)
                        //{
                        //    var dt2 = DateTime.Now;
                        //    var div = dt2 - dt1;
                        //    if (div.TotalSeconds > 60)
                        //    {
                        //        StaticVal.binanceSocketClient.UnsubscribeAllAsync();
                        //        Thread.Sleep(2000);
                        //        dt1 = DateTime.Now;
                        //        var subscribe = StaticVal.binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data =>
                        //        {
                        //            // Handle data when it is received
                        //            var lExists = binanceTick.Where(x => data.Data.Any(y => y.Symbol == x.Symbol));
                        //            if (lExists != null && lExists.Any())
                        //            {
                        //                binanceTick = binanceTick.Except(lExists).ToList();
                        //            }
                        //            binanceTick.AddRange(data.Data);
                        //            StaticVal.binanceTicks = binanceTick;
                        //        });
                        //        Console.WriteLine($"END: {dt2}");
                        //    }
                        //}
                        Thread.Sleep(1000);
                    }
                };
                wrkr.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"StartUp|Load:{ex.Message}");
            }
        }
    }
}

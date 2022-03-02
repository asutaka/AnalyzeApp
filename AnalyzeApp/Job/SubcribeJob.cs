using AnalyzeApp.Common;
using Quartz;
using System;
using System.Linq;
using System.Threading;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class SubcribeJob : IJob
    {
        int tmp = 0;
        public void Execute(IJobExecutionContext context)
        {
            if (tmp > 0)
                return;
            tmp = 1;
            Console.WriteLine($"access Job: {StaticVal.binanceSocketClient.IncomingKbps}; Time: {DateTime.Now}");
            //if(StaticVal.binanceSocketClient.IncomingKbps == 0)
            //{
                Console.WriteLine($"retry Job: {DateTime.Now}");
                //StaticVal.binanceSocketClient.UnsubscribeAllAsync();
                //Thread.Sleep(2000);
                var binanceTick = StaticVal.binanceTicks;
                var isLock = false;
                var subscribeResult = StaticVal.binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data => {
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
                }).GetAwaiter().GetResult();

                subscribeResult.Data.ConnectionLost += () => Console.WriteLine("Connection lost");
                subscribeResult.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
            //}
        }
    }
}

using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using Binance.Net.Enums;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    internal class Data1WJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                NLogLogger.LogInfo($"{DateTime.Now}: Data1WJob Start!");
                var lstTask = new List<Task>();
                var lCoin = StaticVal.lstCoin;
                var dicResult = StaticVal.dic1W;
                foreach (var item in lCoin)
                {
                    var task = Task.Run(() =>
                    {
                        var lData = StaticVal.binanceClient.Spot.Market.GetKlinesAsync(item.S, KlineInterval.OneWeek).GetAwaiter().GetResult().Data;
                        if (lData == null)
                        {
                            return;
                        }
                        var lResult = lData.Select(x => new BinanceKline
                        {
                            BaseVolume = x.BaseVolume,
                            Close = x.Close,
                            CloseTime = ((DateTimeOffset)x.CloseTime).ToUnixTimeMilliseconds(),
                            High = x.High,
                            Low = x.Low,
                            Open = x.Open,
                            OpenTime = ((DateTimeOffset)x.OpenTime).ToUnixTimeMilliseconds(),
                            QuoteVolume = x.QuoteVolume,
                            TakerBuyBaseVolume = x.TakerBuyBaseVolume,
                            TakerBuyQuoteVolume = x.TakerBuyQuoteVolume
                        });
                        dicResult[item.S] = lResult;
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                StaticVal.dic1W = dicResult;
                NLogLogger.LogInfo($"{DateTime.Now}: Data1WJob End!");
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1WJob|Execute: {ex.Message}");
            }
        }
    }
}

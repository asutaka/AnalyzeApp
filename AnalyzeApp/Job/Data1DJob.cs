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
    internal class Data1DJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                NLogLogger.LogInfo($"{DateTime.Now}: Data1DJob Start!");
                var lstTask = new List<Task>();
                var lCoin = StaticVal.lstCoin;
                var dicResult = StaticVal.dic1D;
                foreach (var item in lCoin)
                {
                    var task = Task.Run(() =>
                    {
                        var lData = StaticVal.binanceClient.SpotApi.ExchangeData.GetKlinesAsync(item.S, KlineInterval.OneDay).GetAwaiter().GetResult().Data;
                        if (lData == null)
                        {
                            return;
                        }
                        var lResult = lData.Select(x => new BinanceKline
                        {
                            Volume = x.Volume,
                            Close = x.ClosePrice,
                            CloseTime = ((DateTimeOffset)x.CloseTime).ToUnixTimeMilliseconds(),
                            High = x.HighPrice,
                            Low = x.LowPrice,
                            Open = x.OpenPrice,
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
                StaticVal.dic1D = dicResult;
                NLogLogger.LogInfo($"{DateTime.Now}: Data1DJob End!");
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1DJob|Execute: {ex.Message}");
            }
        }
    }
}

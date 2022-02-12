﻿using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using Binance.Net.Enums;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    internal class Data1HJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                NLogLogger.LogInfo($"{DateTime.Now}: Data1HJob Start!");
                var lstTask = new List<Task>();
                var lCoin = StaticVal.lstCoin;
                var dicResult = StaticVal.dic1H;
                foreach (var item in lCoin)
                {
                    var task = Task.Run(() =>
                    {
                        var lData = StaticVal.binanceClient.Spot.Market.GetKlinesAsync(item.S, KlineInterval.OneHour).GetAwaiter().GetResult().Data;
                        var lResult = lData.Select(x => new BinanceKline
                        {
                            BaseVolume = x.BaseVolume,
                            Close = x.Close,
                            CloseTime = x.CloseTime,
                            High = x.High,
                            Low = x.Low,
                            Open = x.Open,
                            OpenTime = x.OpenTime,
                            QuoteVolume = x.QuoteVolume,
                            TakerBuyBaseVolume = x.TakerBuyBaseVolume,
                            TakerBuyQuoteVolume = x.TakerBuyQuoteVolume
                        });
                        dicResult[item.S] = lResult;
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                StaticVal.dic1H = dicResult;
                NLogLogger.LogInfo($"{DateTime.Now}: Data1HJob End!");
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Data1HJob|Execute: {ex.Message}");
            }
        }
    }
}
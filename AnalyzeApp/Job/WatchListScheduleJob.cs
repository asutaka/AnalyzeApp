using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.GUI.Child;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class WatchListScheduleJob : IJob
    {
        private int FLAG = CommonMethod.GetFlag(Config.BasicSetting);
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (StaticVal.IsRealTimeAction)
                {
                    return;
                }
                var lstTask = new List<Task>();
                var lstResult = StaticVal.lstRealTimeDisplay;
                foreach (var item in lstResult)
                {
                    var task = Task.Run(() =>
                    {
                        var coin = item.Coin;
                        var entityBinanceTick = DataMng.GetCoinBinanceTick(coin);
                        if (entityBinanceTick == null)
                        {
                            return;
                        }
                        item.PrevDayClosePrice = entityBinanceTick.PrevDayClosePrice;
                        item.PriceChangePercent = entityBinanceTick.PriceChangePercent;
                        item.WeightedAveragePrice = entityBinanceTick.WeightedAveragePrice;

                        var curValue = (double)entityBinanceTick.LastPrice;
                        if (curValue <= 0)
                            return;
                        if (item.RefValue <= 0)
                        {
                            item.RefValue = curValue;
                        }

                        if (item.CountTime <= 0)
                        {
                            item.BottomRecent = (double)DataMng.GetBottomVal(coin, Model.ENUM.enumInterval.OneHour);
                            item.RefValue = curValue;
                        }

                        if(item.CountTime <= 0 || item.Rate <= 0)
                        {
                            var rank = CalculateMng.CalculateCryptonRank(coin, item.CoinName);
                            item.Count = rank.Count;
                            item.Rate = rank.Rate;
                        }

                        if (item.CountTime >= FLAG)
                        {
                            item.CountTime = 0;
                        }
                        item.Value = curValue;
                        var rateValue = Math.Round((-1 + (curValue / item.RefValue)) * 100, 2);
                        var waveRecent = item.BottomRecent <= 0 ? 0 : Math.Round((-1 + (curValue / item.BottomRecent)) * 100, 2);
                        item.RateValue = rateValue;
                        item.WaveRecent = waveRecent;
                        item.CountTime++;
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                if (StaticVal.IsRealTimeAction)
                {
                    return;
                }

                StaticVal.lstRealTimeDisplay = lstResult;
                frmWatchList.Instance().InitData();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"RealtimeValueScheduleJob:Execute: {ex.Message}");
            }
        }
    }
}

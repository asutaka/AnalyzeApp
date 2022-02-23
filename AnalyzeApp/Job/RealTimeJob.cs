using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class RealTimeJob : IJob
    {
        private bool isPrepare;
        //List<RealTimeModel> lstResult = new List<RealTimeModel>();
        public void Execute(IJobExecutionContext context)
        {
            //try
            //{
            //    if (!isPrepare)
            //    {
            //        Prepare();
            //    }
            //    var lstTask = new List<Task>();
            //    foreach (var item in lstResult)
            //    {
            //        var task = Task.Run(() =>
            //        {
            //            var model = new RealTimeModel();
            //            //MCDX
            //            if (Config.SpecialSetting.IsActiveMCDX)
            //            {
            //                item.MCDX.Value = CalculateMng.MCDX(item.Coin).Item2;
            //                if (item.MCDX.Value == 20)
            //                    item.MCDX.Level = 5;
            //                else if (item.MCDX.Value >= 18)
            //                    item.MCDX.Level = 4;
            //                else if (item.MCDX.Value >= 15)
            //                    item.MCDX.Level = 3;
            //                else if (item.MCDX.Value >= 10)
            //                    item.MCDX.Level = 2;
            //                else if (item.MCDX.Value > 1)
            //                    item.MCDX.Level = 1;
            //                else item.MCDX.Level = 0;
            //            }
            //            else
            //            {
            //                item.MCDX.Level = -1;
            //            }
            //            //Special
            //            if (Config.SpecialSetting.IsActiveSpecial)
            //            {
            //                item.Special.Value = CalculateMng.SPecial(item.Coin).Item2;
            //                if (item.Special.Value > 0)
            //                    item.Special.Level = 5;
            //                else item.Special.Level = 0;
            //            }
            //            else
            //            {
            //                item.Special.Level = -1;
            //            }
            //            //Thiết lập 2
            //            if (Config.AdvanceSetting1.IsActive)
            //            {
            //                item.Config2.Value = CalculateMng.ConfigData(item.Coin, Config.AdvanceSetting1).Item2;
            //                item.Config2.Level = 1;
            //            }
            //            else
            //            {
            //                item.Config2.Level = -1;
            //            }
            //            //Thiết lập 3
            //            if (Config.AdvanceSetting2.IsActive)
            //            {
            //                item.Config3.Value = CalculateMng.ConfigData(item.Coin, Config.AdvanceSetting2).Item2;
            //                item.Config3.Level = 1;
            //            }
            //            else
            //            {
            //                item.Config3.Level = -1;
            //            }
            //            //Thiết lập 4
            //            if (Config.AdvanceSetting3.IsActive)
            //            {
            //                item.Config4.Value = CalculateMng.ConfigData(item.Coin, Config.AdvanceSetting3).Item2;
            //                item.Config4.Level = 1;
            //            }
            //            else
            //            {
            //                item.Config4.Level = -1;
            //            }
            //            //Thiết lập 5
            //            if (Config.AdvanceSetting4.IsActive)
            //            {
            //                item.Config5.Value = CalculateMng.ConfigData(item.Coin, Config.AdvanceSetting4).Item2;
            //                item.Config5.Level = 1;
            //            }
            //            else
            //            {
            //                item.Config5.Level = -1;
            //            }
            //        });
            //        lstTask.Add(task);
            //    }
            //    Task.WaitAll(lstTask.ToArray());
            //}
            //catch (Exception ex)
            //{
            //    NLogLogger.PublishException(ex, $"FollowListJob:Execute: {ex.Message}");
            //}
        } 
        //private void Prepare()
        //{
        //    if (Config.SpecialSetting.IsActiveTop30)
        //    {
        //        lstResult.AddRange(StaticVal.lstCryptonRank.Take(3).Select(x => new RealTimeModel
        //        {
        //            Coin = x.Coin,
        //            Total = 100,
        //            Top30 = new RealTimeLevelModel { Level = 5, Value = 100 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //        lstResult.AddRange(StaticVal.lstCryptonRank.Skip(3).Take(2).Select(x => new RealTimeModel
        //        {
        //            Coin = x.Coin,
        //            Total = 80,
        //            Top30 = new RealTimeLevelModel { Level = 4, Value = 80 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //        lstResult.AddRange(StaticVal.lstCryptonRank.Skip(5).Take(5).Select(x => new RealTimeModel
        //        {
        //            Coin = x.Coin,
        //            Total = 60,
        //            Top30 = new RealTimeLevelModel { Level = 3, Value = 60 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //        lstResult.AddRange(StaticVal.lstCryptonRank.Skip(10).Take(5).Select(x => new RealTimeModel
        //        {
        //            Coin = x.Coin,
        //            Total = 40,
        //            Top30 = new RealTimeLevelModel { Level = 2, Value = 40 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //        lstResult.AddRange(StaticVal.lstCryptonRank.Skip(15).Take(15).Select(x => new RealTimeModel
        //        {
        //            Coin = x.Coin,
        //            Total = 20,
        //            Top30 = new RealTimeLevelModel { Level = 1, Value = 20 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //        lstResult.AddRange(StaticVal.lstCoinFilter.Where(x => !StaticVal.lstCryptonRank.Any(y => y.Coin == x.S)).Select(x => new RealTimeModel
        //        {
        //            Coin = x.S,
        //            Total = 0,
        //            Top30 = new RealTimeLevelModel { Level = 0, Value = 0 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //    }
        //    else
        //    {
        //        lstResult.AddRange(StaticVal.lstCoinFilter.Select(x => new RealTimeModel
        //        {
        //            Coin = x.S,
        //            Total = 0,
        //            Top30 = new RealTimeLevelModel { Level = -1, Value = -1 },
        //            MCDX = new RealTimeLevelModel(),
        //            Special = new RealTimeLevelModel(),
        //            Config2 = new RealTimeLevelModel(),
        //            Config3 = new RealTimeLevelModel(),
        //            Config4 = new RealTimeLevelModel(),
        //            Config5 = new RealTimeLevelModel()
        //        }));
        //    }
        //    isPrepare = true;
        //}
        //private class RealTimeModel
        //{
        //    public string Coin { get; set; }
        //    public double Total { get; set; }
        //    public RealTimeLevelModel Top30 { get; set; }
        //    public RealTimeLevelModel MCDX { get; set; }
        //    public RealTimeLevelModel Special { get; set; }
        //    public RealTimeLevelModel Config2 { get; set; }
        //    public RealTimeLevelModel Config3 { get; set; }
        //    public RealTimeLevelModel Config4 { get; set; }
        //    public RealTimeLevelModel Config5 { get; set; }
        //}
        //private class RealTimeLevelModel
        //{
        //    public double Value { get; set; }
        //    public short Level { get; set; }
        //}
    }
}

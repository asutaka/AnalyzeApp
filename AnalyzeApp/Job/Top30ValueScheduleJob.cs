using AnalyzeApp.Common;
using AnalyzeApp.Data;
using AnalyzeApp.GUI.Child;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution] /*impt: no multiple instances executed concurrently*/
    public class Top30ValueScheduleJob : IJob
    {
        private int FLAG = CommonMethod.GetFlag(Config.BasicSetting);
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                if (StaticVal.IsRealTimeDeleted)
                    return;
                var lstTask = new List<Task>();
                var lstResult = StaticVal.lstCryptonRank;
                foreach (var item in lstResult)
                {
                    var task = Task.Run(() =>
                    {
                        var coin = item.Coin;
                        var curValue = SeedData.GetCurrentVal(coin);
                        if (curValue <= 0)
                            return;
                        if (item.RefValue <= 0)
                        {
                            item.RefValue = curValue;
                        }

                        if (item.CountTime <= 0)
                        {
                            item.BottomRecent = SeedData.GetBottomVal(coin);
                            item.RefValue = curValue;
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
                if (StaticVal.IsRealTimeDeleted)
                    return;
                StaticVal.lstCryptonRank = lstResult;
                frmTop30.Instance().InitData();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"Top30ValueScheduleJob:Execute: {ex.Message}");
            }
        }
    }
}

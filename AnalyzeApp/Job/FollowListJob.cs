using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class FollowListJob : IJob
    {
        private readonly FollowSettingModel followList = Config.FollowSetting;
        private readonly BasicSettingModel _model = Config.BasicSetting;
        public void Execute(IJobExecutionContext context)
        {
            if (followList == null 
                || !followList.IsNotify
                || followList.Coins == null
                || !followList.Coins.Any()
                || _model == null)
                return;
            foreach (var item in followList.Coins)
            {
                SubFunction(item);
            }
        }

        private void SubFunction(string coin)
        {
            var lstTask = new List<Task>();
            lstTask.Add(Task.Run(() =>
            {
                SubFunction2(coin, followList.FollowSettingMode1);
            }));
            lstTask.Add(Task.Run(() =>
            {
                SubFunction2(coin, followList.FollowSettingMode2);
            }));
            lstTask.Add(Task.Run(() =>
            {
                SubFunction2(coin, followList.FollowSettingMode3);
            }));
            lstTask.Add(Task.Run(() =>
            {
                SubFunction2(coin, followList.FollowSettingMode4);
            }));
            lstTask.Add(Task.Run(() =>
            {
                SubFunction2(coin, followList.FollowSettingMode5);
            }));
            Task.WaitAll(lstTask.ToArray());
        }

        private void SubFunction2(string coin, FollowSettingModeModel model)
        {
            if (model == null 
                || model.lFollowSettingModeDetail == null
                || !model.lFollowSettingModeDetail.Any())
                return;
            foreach (var item in model.lFollowSettingModeDetail)
            {
                SubFunction3(coin, item);
            }
        }

        private void SubFunction3(string coin, FollowSettingModeDetailModel model)
        {
            var lData = GetDictionaryData(coin, (enumInterval)model.Interval);
            if (lData == null || !lData.Any())
                return;
            var count = lData.Count();
            if(model.lFollowSetting_Macd != null && model.lFollowSetting_Macd.Any())
            {
                var macdInput = model.lFollowSetting_Macd.First();
                var max = CalculateMng.Max(_model.MACD_Value.High, _model.MACD_Value.Low, _model.MACD_Value.Signal);
                if (count < max)
                    return;
                var macdOutput = CalculateMng.MACD(lData.Select(x => (double)x.Close).ToArray(), _model.MACD_Value.High, _model.MACD_Value.Low, _model.MACD_Value.Signal, count);
            }
        }

        private IEnumerable<BinanceKline> GetDictionaryData(string coin, enumInterval interval)
        {
            switch (interval)
            {
                case enumInterval.FifteenMinute:
                    {
                        return StaticVal.dic15M.FirstOrDefault(x => x.Key == coin).Value;
                    }
                case enumInterval.OneHour:
                    {
                        return StaticVal.dic1H.FirstOrDefault(x => x.Key == coin).Value;
                    }
                case enumInterval.FourHour:
                    {
                        return StaticVal.dic4H.FirstOrDefault(x => x.Key == coin).Value;
                    }
                case enumInterval.OneDay:
                    {
                        return StaticVal.dic1D.FirstOrDefault(x => x.Key == coin).Value;
                    }
                case enumInterval.OneWeek:
                    {
                        return StaticVal.dic1W.FirstOrDefault(x => x.Key == coin).Value;
                    }
                case enumInterval.OneMonth:
                    {
                        return StaticVal.dic1Month.FirstOrDefault(x => x.Key == coin).Value;
                    }
            }
            return null;
        }
    }
}

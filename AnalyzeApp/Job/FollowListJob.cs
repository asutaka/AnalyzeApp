using AnalyzeApp.Analyze;
using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TicTacTec.TA.Library.Core;

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
            var lClose = lData.Select(x => (double)x.Close).ToArray();
            var lVolume = lData.Select(x => (double)x.BaseVolume).ToArray();
            #region MACD
            if (model.lFollowSetting_Macd != null && model.lFollowSetting_Macd.Any())
            {
                var macdInput = model.lFollowSetting_Macd.First();
                var max = CalculateMng.Max(_model.MACD_Value.High, _model.MACD_Value.Low, _model.MACD_Value.Signal);
                if (count < max)
                    return;
                var macdOutput = CalculateMng.MACD(lClose, _model.MACD_Value.High, _model.MACD_Value.Low, _model.MACD_Value.Signal, count, 3);
                var ratio = 0.15 + (double)macdInput.RatioMax * 2;

                if ((enumCross)macdInput.Option == enumCross.Cross_Above)
                {
                    if (macdOutput.Last() < 0)
                        return;
                }
                else if ((enumCross)macdInput.Option == enumCross.Cross_Below)
                {
                    if (macdOutput.Last() > 0)
                        return;
                }
                else if ((enumCross)macdInput.Option == enumCross.Cross_NearAbove)
                {
                    if (macdOutput.Last() < -ratio)
                        return;
                }
                else if ((enumCross)macdInput.Option == enumCross.Cross_NearBelow)
                {
                    if (macdOutput.Last() > ratio)
                        return;
                }
            }
            #endregion

            #region MA
            if (model.lFollowSetting_Ma != null && model.lFollowSetting_Ma.Any())
            {
                foreach (var item in model.lFollowSetting_Ma)
                {
                    var type = (enumMA)item.Mode == enumMA.MA ? MAType.Sma : MAType.Ema;
                    var valMA1 = CalculateMng.MA(lClose, type, (int)item.Value1, count);
                    var valMA2 = CalculateMng.MA(lClose, type, (int)item.Value2, count);
                    if (valMA1 <= 0 || valMA2 <= 0)
                        return;
                    var valDiv = valMA1 - valMA2;
                    var valRate = valDiv / valMA1;
                    var ratio = 0.15 + (double)item.RatioMax * 2;

                    if ((enumCross)item.Option == enumCross.Cross_Above)
                    {
                        if (valDiv < 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_Below)
                    {
                        if (valDiv > 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearAbove)
                    {
                        if (valRate < -ratio)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearBelow)
                    {
                        if (valRate > ratio)
                            return;
                    }
                }
            }
            #endregion

            #region Price
            if (model.lFollowSetting_Price != null && model.lFollowSetting_Price.Any())
            {
                foreach (var item in model.lFollowSetting_Price)
                {
                    var type = (enumMA)item.Mode == enumMA.MA ? MAType.Sma : MAType.Ema;
                    var valMA = CalculateMng.MA(lClose, type, (int)item.Value, count);
                    var currentVal = DataMng.GetCurrentVal(coin);
                    if (valMA <= 0 || currentVal <= 0)
                        return;
                    var valDiv = (double)currentVal - valMA;
                    var valRate = valDiv / (double)currentVal;
                    var ratio = 0.15 + (double)item.RatioMax * 2;

                    if ((enumCross)item.Option == enumCross.Cross_Above)
                    {
                        if (valDiv < 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_Below)
                    {
                        if (valDiv > 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearAbove)
                    {
                        if (valRate < -ratio)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearBelow)
                    {
                        if (valRate > ratio)
                            return;
                    }
                }
            }
            #endregion

            #region MCDX
            if (model.lFollowSetting_Mcdx != null && model.lFollowSetting_Mcdx.Any())
            {
                var mcdxInput = model.lFollowSetting_Mcdx.First();
                double mcdxVal = 0;
                var entity = StaticVal.lstCryptonRank.FirstOrDefault(x => x.Coin == coin);
                if(entity == null)
                {
                    mcdxVal = CalculateMng.MCDX(coin).Item2;
                }
                else
                {
                    mcdxVal = entity.Value;
                }
                if (mcdxVal == 0
                    || mcdxInput.Value == 0)
                    return;
                if((enumAboveBelow)mcdxInput.Option == enumAboveBelow.Above)
                {
                    if (mcdxVal < (double)mcdxInput.Value)
                        return;
                }
                else 
                {
                    if (mcdxVal > (double)mcdxInput.Value)
                        return;
                }
            }
            #endregion

            #region RSI
            if (model.lFollowSetting_Rsi != null && model.lFollowSetting_Rsi.Any())
            {
                var rsiInput = model.lFollowSetting_Rsi.First();
                var rsiOutput = CalculateMng.RSI(lClose, _model.RSI_Value, count);
                
                if (_model.RSI_Value == 0
                    || rsiOutput == 0)
                    return;
                if ((enumAboveBelow)rsiInput.Option == enumAboveBelow.Above)
                {
                    if (rsiOutput < (double)rsiInput.Value)
                        return;
                }
                else
                {
                    if (rsiOutput > (double)rsiInput.Value)
                        return;
                }
            }
            #endregion

            #region ADX
            if (model.lFollowSetting_Adx != null && model.lFollowSetting_Adx.Any())
            {
                var lHigh = lData.Select(x => (double)x.High).ToArray();
                var lLow = lData.Select(x => (double)x.Low).ToArray();

                var adxInput = model.lFollowSetting_Adx.First();
                var adxOutput = CalculateMng.ADX(lHigh, lLow, lClose, _model.ADX_Value, count);

                if (_model.ADX_Value == 0
                    || adxOutput == 0)
                    return;
                if ((enumAboveBelow)adxInput.Option == enumAboveBelow.Above)
                {
                    if (adxOutput < (double)adxInput.Value)
                        return;
                }
                else
                {
                    if (adxOutput > (double)adxInput.Value)
                        return;
                }
            }
            #endregion

            #region Volume
            if (model.lFollowSetting_Volume != null && model.lFollowSetting_Volume.Any())
            {
                foreach (var item in model.lFollowSetting_Volume)
                {
                    var type = (enumMA)item.Mode == enumMA.MA ? MAType.Sma : MAType.Ema;
                    var currentVol = CalculateMng.MA(lVolume, MAType.Sma, 20, count);
                    var valMA = CalculateMng.MA(lVolume, type, (int)item.Value, count);
                    if (valMA <= 0 || currentVol <= 0 || currentVol == valMA)
                        return;
                    var valDiv = (double)currentVol - valMA;
                    var valRate = valDiv / (double)currentVol;
                    var ratio = 0.15 + (double)item.RatioMax * 2;

                    if ((enumCross)item.Option == enumCross.Cross_Above)
                    {
                        if (valDiv < 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_Below)
                    {
                        if (valDiv > 0)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearAbove)
                    {
                        if (valRate < -ratio)
                            return;
                    }
                    else if ((enumCross)item.Option == enumCross.Cross_NearBelow)
                    {
                        if (valRate > ratio)
                            return;
                    }
                }
            }
            #endregion

            #region Volume2
            if (model.lFollowSetting_Volume2 != null && model.lFollowSetting_Volume2.Any())
            {
                var volInput = model.lFollowSetting_Volume2.First();
                var volOutput = CalculateMng.MA(lVolume, MAType.Sma, 20, count);

                if (volOutput == 0)
                    return;
                if ((enumAboveBelow)volInput.Option == enumAboveBelow.Above)
                {
                    if (volOutput < (double)volInput.Value)
                        return;
                }
                else
                {
                    if (volOutput > (double)volInput.Value)
                        return;
                }
            }
            #endregion
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

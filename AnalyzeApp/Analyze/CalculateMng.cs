using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacTec.TA.Library;

namespace AnalyzeApp.Analyze
{
    public static class CalculateMng
    {
        public static List<Top30Model> Top30()
        {
            var count = 1;
            var lstResult = new List<Top30Model>();
            var lstTask = new List<Task>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    lstResult.Add(CalculateCryptonRank(item.S, item.AN));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            lstResult = lstResult.Where(x => x != null).OrderByDescending(x => x.Count).ThenByDescending(x => x.Rate).Take(30).ToList();
            if(lstResult != null)
            {
                lstResult.ForEach(x => x.STT = count++);
            }
            return lstResult;
        }

        public static Top30Model CalculateCryptonRank(string coin, string coinName)
        {
            try
            {
                int count = 1;
                decimal sum = 0;
                IEnumerable<BinanceKline> lSource = GetSource(coin);
                if (lSource == null || !lSource.Any())
                    return new Top30Model { Coin = coin, Count = count, Rate = (double)Math.Round(sum / count, 2) };

                long dtMin = 0, dtMax = 0, dtMin_Temp = 0;
                int leftMax = 0, rightMin = 0, rightMax = 0;
                decimal min = 0, max = 0, min_Temp = 0;
                foreach (var item in lSource)
                {
                    CheckMinMax();
                    if (rightMax >= 2)
                    {
                        var rate = Math.Round((max - min) * 100 / min, 0);
                        if (leftMax >= 2 && rate >= 10)
                        {
                            sum += rate;
                            count++;
                        }
                        min = min_Temp;
                        dtMin = dtMin_Temp;
                        min_Temp = 0;
                        dtMin_Temp = 0;
                        rightMin = 0;
                        rightMax = 0;
                        leftMax = 0;
                        max = 0;
                        dtMax = 0;
                        CheckMinMax();
                    }
                    else if (rightMax > 0)
                    {
                        min_Temp = item.Low;
                        dtMin_Temp = item.OpenTime;
                    }

                    void CheckMinMax()
                    {
                        if (min == 0)
                        {
                            min = item.Low;
                            dtMin = item.OpenTime;
                        }
                        if (item.Low < min)
                        {
                            rightMin = 0;
                            min = item.Low;
                            dtMin = item.OpenTime;
                        }
                        else
                        {
                            rightMin++;
                        }
                        //reset
                        if (rightMin == 0)
                        {
                            max = 0;
                            leftMax = 0;
                            rightMax = 0;
                        }
                        else
                        {
                            if (max < item.High)
                            {
                                rightMax = 0;
                                leftMax++;
                                max = item.High;
                                dtMax = item.OpenTime;
                            }
                            else
                            {
                                rightMax++;
                            }
                        }
                    }
                }
                var outputModel = new Top30Model { Coin = coin, CoinName = coinName, Count = count, Rate = (double)Math.Round(sum / count, 2) };
                var entityBinanceTick = DataMng.GetCoinBinanceTick(coin);
                if (entityBinanceTick != null)
                {
                    outputModel.RefValue = (double)entityBinanceTick.LastPrice;
                    outputModel.PrevDayClosePrice = entityBinanceTick.PrevDayClosePrice;
                    outputModel.PriceChangePercent = entityBinanceTick.PriceChangePercent;
                    outputModel.WeightedAveragePrice = entityBinanceTick.WeightedAveragePrice;
                }
                return outputModel;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng|CalculateCryptonRank|{coin}: {ex.Message}");
                return new Top30Model { Coin = coin, CoinName = coinName, Count = 1, Rate = 0 };
            }
        }

        public static List<MCDXModel> MCDX()
        {
            var count = 1;
            var lstResult = new List<MCDXModel>();
            var lstTask = new List<Task>();
            foreach (var item in StaticVal.lstCoinFilter)
            {
                var task = Task.Run(() =>
                {
                    var val = MCDX(item.S);
                    if (val.Item1)
                    {
                        lstResult.Add(new MCDXModel
                        {
                            Coin = item.S,
                            CoinName = item.AN,
                            MCDXValue = val.Item2,
                        });
                    }
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            lstResult = lstResult.OrderByDescending(x => x.MCDXValue).Take(30).ToList();
            if (lstResult != null)
            {
                lstResult.ForEach(x => x.STT = count++);
            }
            return lstResult;
        }

        public static (bool, double) MCDX(string coin)
        {
            var data = DataMng.GetCurrentData(coin, (enumInterval)Config.BasicSetting.TimeCalculate);
            if (data == null || !data.Any())
                return (false, 0);
            var arrClose = data.Select(x => (double)x.Close).ToArray();
            var count = arrClose.Count();
            if(count < 50)
                return (false, 0);

            double[] output1 = new double[1000];
            double[] output2 = new double[1000];
            Core.Rsi(0, count - 1, arrClose, 50, out int outBegIdx1, out int outNBElement1, output1);
            Core.Rsi(0, count - 1, arrClose, 40, out int outBegIdx2, out int outNBElement2, output2);
            var rsi50 = output1[count - 51];
            var rsi40 = output2[count - 41];
            var banker_rsi = 1.5 * (rsi50 - 50);
            if (banker_rsi > 20)
                banker_rsi = 20;
            if (banker_rsi < 0)
                banker_rsi = 0;
            var signal = Config.BasicSetting.MCDX_Value;
            return (banker_rsi >= signal, banker_rsi);
        }

        public static IEnumerable<BinanceKline> GetSource(string coin)
        {
            IEnumerable<BinanceKline> lSource = null;
            switch ((enumInterval)Config.BasicSetting.TimeCalculate)
            {
                case enumInterval.FifteenMinute:
                    lSource = StaticVal.dic15M.First(x => x.Key == coin).Value;
                    break;
                case enumInterval.OneHour:
                    lSource = StaticVal.dic1H.First(x => x.Key == coin).Value;
                    break;
                case enumInterval.FourHour:
                    lSource = StaticVal.dic4H.First(x => x.Key == coin).Value;
                    break;
                case enumInterval.OneDay:
                    lSource = StaticVal.dic1D.First(x => x.Key == coin).Value;
                    break;
                case enumInterval.OneWeek:
                    lSource = StaticVal.dic1W.First(x => x.Key == coin).Value;
                    break;
                case enumInterval.OneMonth:
                    lSource = StaticVal.dic1Month.First(x => x.Key == coin).Value;
                    break;
                default: break;
            }
            return lSource;
        }



        public static (bool, double) ConfigData(string coin, AdvanceSettingModel model)
        {
            double result = 0;
            var lstTask = new List<Task>();
            foreach (var item in model.LstInterval)
            {
                if (item == (int)enumInterval.FifteenMinute)
                {
                    var interval = (int)enumInterval.FifteenMinute;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement15M, model.LstIndicator15M, interval);
                    });
                    lstTask.Add(task);
                }
                else if (item == (int)enumInterval.OneHour)
                {
                    var interval = (int)enumInterval.OneHour;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement1H, model.LstIndicator1H, interval);
                    });
                    lstTask.Add(task);
                }
                else if (item == (int)enumInterval.FourHour)
                {
                    var interval = (int)enumInterval.FourHour;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement4H, model.LstIndicator4H, interval);
                    });
                    lstTask.Add(task);
                }
                else if (item == (int)enumInterval.OneDay)
                {
                    var interval = (int)enumInterval.OneDay;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement1D, model.LstIndicator1D, interval);
                    });
                    lstTask.Add(task);
                }
                else if (item == (int)enumInterval.OneWeek)
                {
                    var interval = (int)enumInterval.OneWeek;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement1W, model.LstIndicator1W, interval);
                    });
                    lstTask.Add(task);
                }
                else if (item == (int)enumInterval.OneMonth)
                {
                    var interval = (int)enumInterval.OneMonth;
                    var task = Task.Run(() =>
                    {
                        result += (double)CalculateFromInterval(coin, model.LstElement1M, model.LstIndicator1M, interval);
                    });
                    lstTask.Add(task);
                }
            }
            Task.WaitAll(lstTask.ToArray());
            return (result >= (double)model.Point, result);
        }

        private static decimal CalculateFromInterval(string coin, List<ElementModel> elementModels, List<IndicatorModel> indicatorModels, int interval)
        {
            var lstOutputIndicator = new List<OutputIndicatorModel>();
            var data = DataMng.GetCurrentData(coin, enumInterval.OneHour);
            if (data == null || !data.Any())
                return 0;
            var arrOpen = data.Select(x => (double)x.Open).ToArray();
            var arrClose = data.Select(x => (double)x.Close).ToArray();
            var arrHigh = data.Select(x => (double)x.High).ToArray();
            var arrLow = data.Select(x => (double)x.Low).ToArray();
            var arrVolumne = data.Select(x => (double)x.Volume).ToArray();
            var count = arrClose.Count();

            var lstTask = new List<Task>();
            foreach (var item in elementModels.To<List<GeneralModel>>())
            {
                var task = Task.Run(() =>
                {
                    lstOutputIndicator.Add(CalculateIndicator(coin, item, arrOpen, arrClose, arrHigh, arrLow, arrVolumne, count));
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            return MethodMark(coin, lstOutputIndicator, indicatorModels);
        }
        private static OutputIndicatorModel CalculateIndicator(string coin, GeneralModel model, double[] arrOpen, double[] arrClose, double[] arrHigh, double[] arrLow, double[] arrVolumne, int count)
        {
            var outputModel = new OutputIndicatorModel { Indicator = model.Indicator, Period = model.Period, Value = 0 };
            double[] output1 = new double[1000];
            double[] output2 = new double[1000];
            double[] output3 = new double[1000];
            if (model.Indicator == (int)enumChooseData.MA)
            {
                Core.MovingAverage(0, count - 1, arrClose, model.Period, Core.MAType.Sma, out int outBegIdx, out int outNBElement, output1);
                outputModel.Value = output1[count - model.Period];
            }
            else if (model.Indicator == (int)enumChooseData.EMA)
            {
                Core.MovingAverage(0, count - 1, arrClose, model.Period, Core.MAType.Ema, out int outBegIdx, out int outNBElement, output1);
                outputModel.Value = output1[count - model.Period];
            }
            else if (model.Indicator == (int)enumChooseData.Volumne)
            {
                Core.MovingAverage(0, count - 1, arrVolumne, model.Period, Core.MAType.Sma, out int outBegIdx, out int outNBElement, output1);
                outputModel.Value = output1[count - model.Period];
            }
            else if (model.Indicator == (int)enumChooseData.CandleStick_1)
            {
                var takeNum = 1;
                if (model.Period == (int)enumCandleStick.O)
                {
                    outputModel.Value = arrOpen.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.H)
                {
                    outputModel.Value = arrHigh.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.L)
                {
                    outputModel.Value = arrLow.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.C)
                {
                    outputModel.Value = arrClose.ElementAt(count - takeNum);
                }
            }
            else if (model.Indicator == (int)enumChooseData.CandleStick_2)
            {
                var takeNum = 2;
                if (model.Period == (int)enumCandleStick.O)
                {
                    outputModel.Value = arrOpen.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.H)
                {
                    outputModel.Value = arrHigh.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.L)
                {
                    outputModel.Value = arrLow.ElementAt(count - takeNum);
                }
                if (model.Period == (int)enumCandleStick.C)
                {
                    outputModel.Value = arrClose.ElementAt(count - takeNum);
                }
            }
            else if (model.Indicator == (int)enumChooseData.MACD)
            {
                Core.Macd(0, count - 1, arrClose, model.Low, model.High, model.Signal, out int outBegIdx, out int outNbElement, output1, output2, output3);
                outputModel.Value = output1[count - 1];
            }
            else if (model.Indicator == (int)enumChooseData.RSI)
            {
                Core.Rsi(0, count - 1, arrClose, model.Period, out int outBegIdx, out int outNBElement, output1);
                outputModel.Value = output1[count - model.Period];
            }
            else if (model.Indicator == (int)enumChooseData.ADX)
            {
                Core.Adx(0, count - 1, arrHigh, arrLow, arrClose, model.Period, out int outBegIdx, out int outNBElement, output1);
                outputModel.Value = output1[count - model.Period];
            }
            else if (model.Indicator == (int)enumChooseData.CurrentValue)
            {
                outputModel.Value = (double)DataMng.GetCurrentVal(coin);
            }
            return outputModel;
        }
        private static decimal MethodMark(string coin, List<OutputIndicatorModel> lstModel, List<IndicatorModel> indicatorModels)
        {
            decimal point = 0;
            var lstTask = new List<Task>();
            foreach (var item in indicatorModels)
            {
                var task = Task.Run(() =>
                {
                    point += MethodMarkUnit(coin, lstModel, item);
                });
                lstTask.Add(task);
            }
            Task.WaitAll(lstTask.ToArray());
            return point;
        }
        private static decimal MethodMarkUnit(string coin, List<OutputIndicatorModel> lstModel, IndicatorModel indicator)
        {
            decimal point = 0;
            var indicator1 = indicator.Element1.To<GeneralModel>();
            var indicator2 = indicator.Element2.To<GeneralModel>();
            double result = (double)indicator.Result;

            if (indicator.Unit == (int)enumUnit.Ratio)
            {
                var currentValue = (double)DataMng.GetCurrentVal(coin);
                result = (double)indicator.Result * currentValue / 100;
            }
            double firstValue = lstModel.FirstOrDefault(x => x.Indicator == indicator1.Indicator && x.Period == indicator1.Period).Value;
            double secondValue = 0;
            if (indicator2 != null)
                secondValue = lstModel.FirstOrDefault(x => x.Indicator == indicator2.Indicator && x.Period == indicator2.Period).Value;
            double div = firstValue - secondValue;
            if (div < 0)
                return point;
            if (indicator.Operator == (int)enumOperator.Equal
                && div == result)
            {
                point += indicator.Point;
            }
            else if (indicator.Operator == (int)enumOperator.Greater
                && div > result)
            {
                point += indicator.Point;
            }
            else if (indicator.Operator == (int)enumOperator.GreaterOrEqual
                && div >= result)
            {
                point += indicator.Point;
            }
            else if (indicator.Operator == (int)enumOperator.LessThan
                && div < result)
            {
                point += indicator.Point;
            }
            else if (indicator.Operator == (int)enumOperator.LessThanOrEqual
                && div <= result)
            {
                point += indicator.Point;
            }
            return point;
        }

        public static double ADX(double[] arrHigh, double[] arrLow, double[] arrClose, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Adx(0, count - 1, arrHigh, arrLow, arrClose, period, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:ADX: {ex.Message}");
            }
            return 0;
        }
        public static double MA(double[] arrInput, Core.MAType type, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.MovingAverage(0, count - 1, arrInput, period, Core.MAType.Sma, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:MA: {ex.Message}");
            }
            return 0;
        }
        public static double MACD(double[] arrInput, int high, int low, int signal, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Macd(0, count - 1, arrInput, low, high, signal, out var outBegIdx, out var outNbElement, new double[1000], new double[1000], output);
                return output[count - 1];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:MACD: {ex.Message}");
            }
            return 0;
        }
        public static IEnumerable<double> MACD(double[] arrInput, int high, int low, int signal, int count, int take)
        {
            try
            {
                var output = new double[1000];
                Core.Macd(0, count - 1, arrInput, low, high, signal, out var outBegIdx, out var outNbElement, new double[1000], new double[1000], output);
                return output.Skip(count - (take + 1)).Take(take);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:MACD: {ex.Message}");
            }
            return null;
        }
        public static (double, double) MCDX(double[] arrInput, int count)
        {
            try
            {
                var rsi50 = RSI(arrInput, 50, count);
                var rsi40 = RSI(arrInput, 40, count); ;
                //
                var banker_rsi = 1.5 * (rsi50 - 50);
                if (banker_rsi > 20)
                    banker_rsi = 20;
                if (banker_rsi < 0)
                    banker_rsi = 0;
                //
                var hot_rsi = 0.7 * (rsi40 - 30);
                if (hot_rsi > 20)
                    hot_rsi = 20;
                if (hot_rsi < 0)
                    hot_rsi = 0;

                return (banker_rsi, hot_rsi);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:MCDX: {ex.Message}");
            }
            return (0, 0);

        }
        public static double RSI(double[] arrInput, int period, int count)
        {
            try
            {
                var output = new double[1000];
                Core.Rsi(0, count - 1, arrInput, period, out var outBegIdx, out var outNBElement, output);
                return output[count - period];
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CalculateMng:RSI: {ex.Message}");
            }
            return 0;
        }

        public static decimal Max(decimal x, decimal y, decimal z = 0, decimal t = 0)
        {
            var max = x < y ? x : y;
            max = max < z ? z : max;
            max = max < t ? t : max;
            return max;
        }

        private class OutputIndicatorModel
        {
            public int Indicator { get; set; }
            public int Period { get; set; }
            public double Value { get; set; }
        }
    }
}

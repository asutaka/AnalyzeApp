using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Binance.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyzeApp.Common
{
    public class DataMng
    {
        public static List<CryptonDetailDataModel> GetCoin()
        {
            var cryptonModel = CommonMethod.DownloadJsonFile<CryptonDataModel>(ConstVal.COIN_LIST);
            var output = cryptonModel.Data.Where(x => x.S.Substring(x.S.Length - 4) == "USDT").OrderBy(x => x.S).ToList();
            return output;
        }

        public static IEnumerable<BinanceKline> LoadSource(string coin, enumInterval interval)
        {
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}\\data\\{interval.GetDisplayName()}\\{coin}.txt";
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<IEnumerable<BinanceKline>>(json);
                    return result;
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|LoadSource: {ex.Message}");
                return new List<BinanceKline>();
            }
        }

        public static decimal GetBottomVal(string coin, enumInterval interval)
        {
            IEnumerable<BinanceKline> lData = null;
            switch (interval)
            {
                case enumInterval.ThirteenMinute:
                    lData = StaticVal.dic15M[coin]; break;
                case enumInterval.OneHour:
                    lData = StaticVal.dic1H[coin]; break;
                case enumInterval.FourHour:
                    lData = StaticVal.dic4H[coin]; break;
                case enumInterval.OneDay:
                    lData = StaticVal.dic1D[coin]; break;
                case enumInterval.OneWeek:
                    lData = StaticVal.dic1W[coin]; break;
                case enumInterval.OneMonth:
                    lData = StaticVal.dic1Month[coin]; break;
            }
            if (lData == null || lData.Count() < 15)
                return 0;
            var count = lData.Count();
            decimal min = lData.ElementAt(count - 1).Close;
            var countConfirm = 0;
            for (int i = count - 2; i > count - 15; i--)
            {
                var val = lData.ElementAt(i - 1).Close;
                if (min <= val)
                {
                    if (++countConfirm == 3)
                    {
                        return min;
                    }
                }
                else
                {
                    countConfirm = 0;
                    min = val;
                }
            }
            return min;
        }

        public static decimal GetCurrentVal(string coin)
        {
            if (StaticVal.binanceTicks == null)
                Thread.Sleep(100);
            var entity = StaticVal.binanceTicks.FirstOrDefault(x => x.Symbol == coin);
            if (entity == null)
                return 0;
            return entity.LastPrice;
        }

        public static IBinanceTick GetCoinBinanceTick(string coin)
        {
            if (StaticVal.binanceTicks == null)
                Thread.Sleep(100);
            var entity = StaticVal.binanceTicks.FirstOrDefault(x => x.Symbol == coin);
            return entity;
        }

        #region StoredData
        public static async Task StoredData()
        {
            try
            {
                //update flag
                await APIService.Instance().UpdateConfigTable(new ConfigTableModel { StatusLoadData = (int)enumStatusLoadData.Loading });
                //
                var lDataSetting = await APIService.Instance().GetDataSettings();
                if (lDataSetting != null)
                {
                    var lstTask = new List<Task>();
                    long curTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    foreach (var item in lDataSetting)
                    {
                        var task = Task.Run(() =>
                        {
                            var div = curTime - item.UpdatedTime;
                            switch (item.Interval)
                            {
                                case (int)enumInterval.ThirteenMinute:
                                    {
                                        if (div > 900000)
                                        {
                                            SyncDataInterval(enumInterval.ThirteenMinute);
                                        }
                                        break;
                                    }
                                case (int)enumInterval.OneHour:
                                    {
                                        if (div > 3600000)
                                        {
                                            SyncDataInterval(enumInterval.OneHour);
                                        }
                                        break;
                                    }
                                case (int)enumInterval.FourHour:
                                    {
                                        if (div > 14400000)
                                        {
                                            SyncDataInterval(enumInterval.FourHour);
                                        }
                                        break;
                                    }
                                case (int)enumInterval.OneDay:
                                    {
                                        if (div > 86400000)
                                        {
                                            SyncDataInterval(enumInterval.OneDay);
                                        }
                                        break;
                                    }
                                case (int)enumInterval.OneWeek:
                                    {
                                        if (div > 604800000)
                                        {
                                            SyncDataInterval(enumInterval.OneWeek);
                                        }
                                        break;
                                    }
                                case (int)enumInterval.OneMonth:
                                    {
                                        if (div > 2419200000)
                                        {
                                            SyncDataInterval(enumInterval.OneMonth);
                                        }
                                        break;
                                    }
                            }
                            APIService.Instance().UpdateDataSettings(new DataSettingModel { Interval = item.Interval, UpdatedTime = curTime }).GetAwaiter().GetResult();
                        });
                        lstTask.Add(task);
                    }
                    Task.WaitAll(lstTask.ToArray());
                    //update flag
                    await APIService.Instance().UpdateConfigTable(new ConfigTableModel { StatusLoadData = (int)enumStatusLoadData.Endload });
                }
            }
            catch (Exception ex)
            {
                try
                {
                    await APIService.Instance().UpdateConfigTable(new ConfigTableModel { StatusLoadData = (int)enumStatusLoadData.ErrorLoad });
                }
                catch (Exception ex2)
                {
                    NLogLogger.PublishException(ex2, $"DataMng|LoadData|ErrorLoad:{ex2.Message}");
                }
                NLogLogger.PublishException(ex, $"DataMng|LoadData:{ex.Message}");
            }
        }
        private static void SyncDataInterval(enumInterval interval)
        {
            try
            {
                var lCoin = GetCoin();
                var lstTask = new List<Task>();
                foreach (var item in lCoin)
                {
                    var task = Task.Run(() =>
                    {
                        SyncDataValue(item.S, interval);
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|SyncDataInterval: {ex.Message}");
            }
        }
        private static void SyncDataValue(string coin, enumInterval interval)
        {
            try
            {
                var pathFile = $"{Directory.GetCurrentDirectory()}\\data\\{interval.GetDisplayName()}\\{coin}.txt";
                IEnumerable<BinanceKline> arrSource;
                var index = 0;
                do
                {
                    arrSource = GetSource(coin, interval);
                }
                while (index < 3 && arrSource == null);
                if (arrSource == null || !arrSource.Any())
                {
                    return;
                }
                //
                File.Create(pathFile).Close();
                string json = JsonConvert.SerializeObject(arrSource);
                //write string to file
                File.WriteAllText(pathFile, json);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|SyncDataValue: {ex.Message}");
            }
        }
        private static IEnumerable<BinanceKline> GetSource(string coin, enumInterval interval, long startTime = 0, long endTime = 0)
        {
            try
            {
                string strTime = string.Empty;
                if (startTime > 0)
                    strTime += $"&startTime={startTime}";
                if (endTime > 0)
                    strTime += $"&endTime={endTime}";
                var url = $"{ConstVal.COIN_DETAIL}symbol={coin}&interval={interval.GetDisplayName()}{strTime}";
                var arrData = CommonMethod.DownloadJsonArray(url);
                return arrData.Select(x => new BinanceKline
                {
                    OpenTime = (long)x[0],
                    Open = decimal.Parse(x[1].ToString()),
                    High = decimal.Parse(x[2].ToString()),
                    Low = decimal.Parse(x[3].ToString()),
                    Close = decimal.Parse(x[4].ToString()),
                    BaseVolume = decimal.Parse(x[5].ToString()),
                    CloseTime = (long)x[6],
                    QuoteVolume = decimal.Parse(x[7].ToString()),
                    TakerBuyBaseVolume = decimal.Parse(x[9].ToString()),
                    TakerBuyQuoteVolume = decimal.Parse(x[10].ToString())
                });
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|GetSource:{ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
//15m: 10 ngày
//1h: 1 tháng
//4h: 3 tháng
//1d: 2 năm
//1w: 10 năm
//1m: no limit

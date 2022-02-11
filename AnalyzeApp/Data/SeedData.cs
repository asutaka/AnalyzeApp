using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Binance.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace AnalyzeApp.Data
{
    public class SeedData
    {
        public static void SubcribeData()
        {
            try
            {
                var socketClient = new BinanceSocketClient();
                var subscribeResult = socketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(data =>
                {
                    if (data != null)
                    {
                        StaticVal.source = data.Data;
                    }
                });
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SeedData|SubcribeData: {ex.Message}");
            }
        }
        public static List<CryptonDetailDataModel> GetCryptonList()
        {
            var cryptonModel = CommonMethod.DownloadJsonFile<CryptonDataModel>(ConstVal.COIN_LIST);
            var output = cryptonModel.Data.Where(x => x.S.Substring(x.S.Length - 4) == "USDT"
                                            && !x.S.Substring(0, x.S.Length - 4).Contains("USD")
                                            && !x.AN.Contains("Fan Token")
                                            && !x.S.Contains("UP")
                                            && !x.S.Contains("DOWN"))
                                    .OrderBy(x => x.S).ToList();
            return output;
        }

        public static List<CryptonDetailDataModel> GetCryptonListWithFilter()
        {
            var cryptonModel = CommonMethod.DownloadJsonFile<CryptonDataModel>(ConstVal.COIN_LIST);
            var output = cryptonModel.Data.Where(x => !Config.BlackLists.Any(y => y.S == x.S)
                                            && x.S.Substring(x.S.Length - 4) == "USDT"
                                            && !x.S.Substring(0, x.S.Length - 4).Contains("USD")
                                            && !x.AN.Contains("Fan Token")
                                            && !x.S.Contains("UP")
                                            && !x.S.Contains("DOWN"))
                                    .OrderBy(x => x.S).ToList();
            return output;
        }

        public static List<CandleStickDataModel> LoadDatasource(string code, enumInterval interval)
        {
            var lstModel = new List<CandleStickDataModel>();
            try
            {
                var url = $"{ConstVal.COIN_DETAIL}symbol={code}&interval={interval.GetDisplayName()}";
                var arrData = CommonMethod.DownloadJsonArray(url);

                lstModel = arrData.Select(x => new CandleStickDataModel
                {
                    Time = ((int)((long)x[0] / 1000)).UnixTimeStampToDateTime(),
                    Open = float.Parse(x[1].ToString()),
                    High = float.Parse(x[2].ToString()),
                    Low = float.Parse(x[3].ToString()),
                    Close = float.Parse(x[4].ToString()),
                    Volumne = float.Parse(x[5].ToString())
                }).ToList();
                return lstModel;
            }
            catch
            {
                return lstModel;
            }
        }

        public static double GetBottomVal(string code)
        {
            var url = $"{ConstVal.COIN_DETAIL}symbol={code}&interval={enumInterval.OneHour.GetDisplayName()}&limit=15";
            var arrData = CommonMethod.DownloadJsonArray(url);
            if (arrData == null || arrData.Count < 15)
                return 0;
            var count = arrData.Count;
            double min = double.Parse(arrData[0][3].ToString());
            var countConfirm = 0;
            for (int i = 1; i < count; i++)
            {
                var val = double.Parse(arrData[i][3].ToString());
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

        public static double GetCurrentVal(string coin)
        {
            var url = $"{ConstVal.COIN_VAL}symbol={coin}";
            var data = CommonMethod.DownloadJsonFile<CurrentPriceModel>(url);
            if (data == null)
                return 0;
            return data.price;
        }

        public static Coin24hModel GetCoin24h(string coin)
        {
            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            StaticVal.client.BaseAddress = new Uri("");
            StaticVal.client.DefaultRequestHeaders.Accept.Clear();
            StaticVal.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = StaticVal.client.GetStringAsync($"{ConstVal.COIN_24H}symbol={coin}").GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<Coin24hModel>(response);
            return data;
        }

        public static CoinDeptModel GetCoinDept(string coin)
        {
            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            StaticVal.client.BaseAddress = new Uri("");
            StaticVal.client.DefaultRequestHeaders.Accept.Clear();
            StaticVal.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = StaticVal.client.GetStringAsync($"{ConstVal.COIN_DEPT}symbol={coin}").GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<CoinDeptModel>(response);
            return data;
        }

        public static List<CoinTradeModel> GetCoinTrade(string coin)
        {
            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            StaticVal.client.BaseAddress = new Uri("");
            StaticVal.client.DefaultRequestHeaders.Accept.Clear();
            StaticVal.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = StaticVal.client.GetStringAsync($"{ConstVal.COIN_TRADE}symbol={coin}").GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<List<CoinTradeModel>>(response);
            return data;
        }

        public static List<CoinAggTradeModel> GetCoinAggTrade(string coin)
        {
            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            //public static HttpClient client = new HttpClient();
            StaticVal.client.BaseAddress = new Uri("");
            StaticVal.client.DefaultRequestHeaders.Accept.Clear();
            StaticVal.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = StaticVal.client.GetStringAsync($"{ConstVal.COIN_AggTRADE}symbol={coin}").GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<List<CoinAggTradeModel>>(response);
            return data;
        }

        private class CurrentPriceModel
        {
            public string symbol { get; set; }
            public double price { get; set; }
        }
    }
}

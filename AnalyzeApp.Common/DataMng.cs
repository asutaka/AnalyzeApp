using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public static JArray GetSource(string coin, enumInterval interval, long startTime = 0, long endTime = 0)
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
                return arrData;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|GetSource:{ex.Message}");
                return null;
            }
        }
        #region StoredData
        public static async Task StoredData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(ConstVal.api);
            try
            {
                //update flag
                var model = new ConfigTableModel { StatusLoadData = (int)enumStatusLoadData.Loading };
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/UpdateConfigTable", c);
                //
                var responseDataSetting = await client.GetAsync("Setting/GetDataSettings");
                if (responseDataSetting != null)
                {
                    var lDataSetting = JsonConvert.DeserializeObject<IEnumerable<DataSettingModel>>(await responseDataSetting.Content.ReadAsStringAsync());
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
                                var modelTime = new DataSettingModel { Interval = item.Interval, UpdatedTime = curTime };
                                HttpContent content = new StringContent(JsonConvert.SerializeObject(modelTime), Encoding.UTF8, "application/json");
                                client.PostAsync("Setting/UpdateDataSettings", content).GetAwaiter().GetResult();
                            });
                            lstTask.Add(task);
                        }
                        Task.WaitAll(lstTask.ToArray());
                        //update flag
                        model.StatusLoadData = (int)enumStatusLoadData.Endload;
                        c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        await client.PostAsync("Setting/UpdateConfigTable", c);
                    }
                }
            }
            catch (Exception ex)
            {
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
                JArray arrSource;
                var index = 0;
                do
                {
                    arrSource = GetSource(coin, interval);
                }
                while (index < 3 && arrSource == null);
                if (arrSource == null)
                {
                    return;
                }
                //
                File.Create(pathFile).Close();
                //
                using (StreamWriter stream = new StreamWriter(pathFile, true))
                {
                    foreach (var line in arrSource)
                    {
                        stream.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|SyncDataValue: {ex.Message}");
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

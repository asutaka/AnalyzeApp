using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace AnalyzeApp.DataService
{
    public class Worker : BackgroundService
    {
        private readonly string _api;

        public Worker(IConfiguration configuration)
        {
            _api = configuration["API"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(_api);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //var appCheck = Process.GetProcessesByName(ConstVal.appName);
                    //if (appCheck == null || !appCheck.Any())
                    //{
                    //    await StopAsync(stoppingToken);
                    //}
                    var response = await client.GetAsync("Setting/GetConfigTable");
                    var data = JsonConvert.DeserializeObject<ConfigTableModel>(await response.Content.ReadAsStringAsync());
                    if (data != null)
                    //if (data != null && data.StatusLoadData == (int)enumStatusLoadData.None)
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
                                                        SyncDataInterval(enumInterval.ThirteenMinute, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                            case (int)enumInterval.OneHour:
                                                {
                                                    if (div > 3600000)
                                                    {
                                                        SyncDataInterval(enumInterval.OneHour, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                            case (int)enumInterval.FourHour:
                                                {
                                                    if (div > 14400000)
                                                    {
                                                        SyncDataInterval(enumInterval.FourHour, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                            case (int)enumInterval.OneDay:
                                                {
                                                    if (div > 86400000)
                                                    {
                                                        SyncDataInterval(enumInterval.OneDay, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                            case (int)enumInterval.OneWeek:
                                                {
                                                    if (div > 604800000)
                                                    {
                                                        SyncDataInterval(enumInterval.OneWeek, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                            case (int)enumInterval.OneMonth:
                                                {
                                                    if (div > 2419200000)
                                                    {
                                                        SyncDataInterval(enumInterval.OneMonth, (long)item.UpdatedTime);
                                                    }
                                                    break;
                                                }
                                        }
                                        var modelTime = new DataSettingModel { Interval = item.Interval, UpdatedTime = curTime };
                                        HttpContent content = new StringContent(JsonConvert.SerializeObject(modelTime), Encoding.UTF8, "application/json");
                                        client.PostAsync("Setting/UpdateDataSettings", content).GetAwaiter().GetResult();
                                    });
                                    lstTask.Add(task);
                                    ////phunv
                                    //break;
                                }
                                Task.WaitAll(lstTask.ToArray());
                                //update flag
                                model.StatusLoadData = (int)enumStatusLoadData.Endload;
                                c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                                await client.PostAsync("Setting/UpdateConfigTable", c);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex, $"Worker|ExecuteAsync:{ex.Message}");
                }
                NLogLogger.LogInfo($"{DateTime.Now}:DataService is running!");
                await Task.Delay(600, stoppingToken);
            }
        }

        private void SyncDataInterval(enumInterval interval, long startTime)
        {
            try
            {
                var lCoin = DataMng.GetCoin();
                var lstTask = new List<Task>();
                foreach (var item in lCoin)
                {
                    var task = Task.Run(() =>
                    {
                        SyncDataValue(item.S, interval, startTime);
                    });
                    lstTask.Add(task);
                    ////phunv
                    //break;
                }
                Task.WaitAll(lstTask.ToArray());
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"Worker|SyncDataInterval: {ex.Message}");
            }
        }

        private void SyncDataValue(string coin, enumInterval interval, long startTime)
        {
            try
            {
                var pathFile = $"{Directory.GetParent(Directory.GetCurrentDirectory())}\\data\\{interval.GetDisplayName()}\\{coin}.txt";
                if (!File.Exists(pathFile))
                {
                    startTime = 0;
                }
                JArray arrSource;
                var index = 0;
                do
                {
                    arrSource = DataMng.GetSource(coin, interval, startTime);
                }
                while (index < 3 && arrSource == null);
                if (arrSource == null)
                {
                    File.Delete(pathFile);
                    return;
                }
                //
                if (!File.Exists(pathFile))
                {
                    var file = File.Create(pathFile);
                    file.Close();
                }
                var lines = File.ReadAllLines(pathFile);
                var length = lines.Length;
                if (length > 0)
                {
                    if(length > 500)
                    {
                        var div = length - 500;
                        File.WriteAllLines(pathFile, lines.Skip(div).Take(lines.Length - (1 + div)).ToArray());
                    }
                    else
                    {
                        File.WriteAllLines(pathFile, lines.Take(lines.Length - 1).ToArray());
                    }
                }
                //
                using (StreamWriter stream = new StreamWriter(pathFile, true))
                {
                    foreach (var line in arrSource)
                    {
                        stream.WriteLine(line);
                    }
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"Worker|SyncDataValue: {ex.Message}");
            }
        }
    }
}
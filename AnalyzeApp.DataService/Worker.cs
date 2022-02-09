using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            while (!stoppingToken.IsCancellationRequested)
            {
                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri(_api);
                var response = await client.GetAsync("Setting/GetConfigTable");
                var data = JsonConvert.DeserializeObject<ConfigTableModel>(response.Content.ToString());
                if (data.StatusLoadData == (int)enumStatusLoadData.None)
                {
                    //var response1 = await client.PostAsync("Setting/UpdateConfigTable", new ConfigTableModel { StatusLoadData = (int)enumStatusLoadData.Loading });
                }
                NLogLogger.LogInfo($"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
using AnalyzeApp.Model.ENTITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeApp.Common
{
    public class APIService
    {
        private HttpClient client = new HttpClient();
        private APIService()
        {
            InitData();
        }
        private static APIService _instance = null;
        public static APIService Instance()
        {
            _instance = _instance ?? new APIService();
            return _instance;
        }
        private void InitData()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(ConstVal.api);
        }

        public async Task<ProfileModel> GetProfile()
        {
            try
            {
                var response = await client.GetAsync("Setting/GetProfile");
                return JsonConvert.DeserializeObject<ProfileModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetProfile: {ex.Message}");
            }
            return null;
        }

        public async Task<IEnumerable<SettingModel>> GetSettings()
        {
            try
            {
                var response = await client.GetAsync("Setting/GetSettings");
                return JsonConvert.DeserializeObject<IEnumerable<SettingModel>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetSettings: {ex.Message}");
            }
            return null;
        }

        public async Task<IEnumerable<DataSettingModel>> GetDataSettings()
        {
            try
            {
                var response = await client.GetAsync("Setting/GetDataSettings");
                return JsonConvert.DeserializeObject<IEnumerable<DataSettingModel>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetDataSettings: {ex.Message}");
            }
            return null;
        }

        public async Task<ConfigTableModel> GetConfigTable()
        {
            try
            {
                var response = await client.GetAsync("Setting/GetConfigTable");
                return JsonConvert.DeserializeObject<ConfigTableModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetConfigTable: {ex.Message}");
            }
            return null;
        }

        public async Task<long> InsertProfile(ProfileModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/InsertProfile", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|InsertProfile: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> DeleteProfile()
        {
            try
            {
                HttpContent c = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/DeleteProfile", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|DeleteProfile: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> InsertSetting(SettingModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/InsertSetting", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|InsertSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> UpdateSetting(SettingModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/UpdateSetting", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|UpdateSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> DeleteSetting(int id)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/DeleteSetting", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|DeleteSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> InsertDataSettings(DataSettingModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/InsertDataSettings", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|InsertDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> UpdateDataSettings(DataSettingModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/UpdateDataSettings", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|UpdateDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> DeleteDataSettings(int id)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/DeleteDataSettings", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|DeleteDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> UpdateConfigTable(ConfigTableModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/UpdateConfigTable", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|UpdateConfigTable: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> SendMessage(NotifyModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(StaticVal.profile.Phone))
                {
                    NLogLogger.LogInfo("Phone is Empty!");
                    return -1;
                }

                model.Phone = StaticVal.profile.Phone;
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Telegram/SendMessage", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|SendMessage: {ex.Message}");
            }
            return -1;
        }

        public async Task<Dictionary<string, IEnumerable<BinanceKline>>> GetData(DataInputModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("Setting/GetData", c);
                return JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<BinanceKline>>>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetData: {ex.Message}");
            }
            return null;
        }
    }
}

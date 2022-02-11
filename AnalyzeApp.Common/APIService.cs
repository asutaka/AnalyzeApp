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

        public async Task<UserModel> GetUser()
        {
            try
            {
                var response = await client.GetAsync("Setting/GetUser");
                return JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|GetUser: {ex.Message}");
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

        public async Task<long> InsertUser(UserModel model)
        {
            try
            {
                HttpContent c = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/InsertUser", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|InsertUser: {ex.Message}");
            }
            return -1;
        }

        public async Task<long> DeleteUser()
        {
            try
            {
                HttpContent c = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                await client.PostAsync("Setting/DeleteUser", c);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"APIService|DeleteUser: {ex.Message}");
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
    }
}

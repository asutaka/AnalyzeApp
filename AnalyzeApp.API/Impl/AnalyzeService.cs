using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Model;
using Binance.Net.Clients;
using Binance.Net.Enums;

namespace AnalyzeApp.API.Impl
{
    public class AnalyzeService : IAnalyzeService
    {
        private readonly IAnalyzeRepo _repo;
        public AnalyzeService(IAnalyzeRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<SettingModel>> GetSettings()
        {
            return await _repo.GetSettings();
        }

        public async Task<ProfileModel> GetProfile()
        {
            return await _repo.GetProfile();
        }

        public async Task<int> InsertNotify(NotifyModel model)
        {
            return await _repo.InsertNotify(model);    
        }
        public async Task<List<NotifyModel>> GetNotify(int top)
        {
            return await _repo.GetNotify(top);
        }

        public async Task<bool> Verify(VerifyModel model)
        {
            string phone, apiId, apiHash;
            if (model.IsService)
            {
                phone = ConstVal.phoneService;
                apiId = ConstVal.apiIdService;
                apiHash = ConstVal.apiHashService;
            }
            else
            {
                phone = ConstVal.phoneSupport;
                apiId = ConstVal.apiIdSupport;
                apiHash = ConstVal.apiHashSupport;
            }
            return await TeleClient.GenerateSession(phone, apiId, apiHash, model.VerifyCode, model.IsService);
        }

        public async Task<int> InsertProfile(ProfileModel model)
        {
            return await _repo.InsertProfile(model);
        }

        public async Task<int> DeleteProfile()
        {
            return await _repo.DeleteProfile();
        }

        public async Task<int> InsertSetting(SettingModel model)
        {
            return await _repo.InsertSetting(model);
        }

        public async Task<int> UpdateSetting(SettingModel model)
        {
            return await _repo.UpdateSetting(model);
        }

        public async Task<int> DeleteSetting(int id)
        {
            return await _repo.DeleteSetting(id);
        }

        public async Task<List<DataSettingModel>> GetDataSettings()
        {
            return await _repo.GetDataSettings();
        }

        public async Task<int> InsertDataSettings(DataSettingModel model)
        {
            return await _repo.InsertDataSettings(model);
        }

        public async Task<int> UpdateDataSettings(DataSettingModel model)
        {
            return await _repo.UpdateDataSettings(model);
        }

        public async Task<int> DeleteDataSettings(int interval)
        {
            return await _repo.DeleteDataSettings(interval);
        }

        public async Task<ConfigTableModel> GetConfigTable()
        {
            return await _repo.GetConfigTable();
        }

        public async Task<int> UpdateConfigTable(ConfigTableModel model)
        {
            return await _repo.UpdateConfigTable(model);
        }

        public Dictionary<string, IEnumerable<BinanceKlineModel>> GetData(DataInputModel model)
        {
            Dictionary<string, IEnumerable<BinanceKlineModel>> dicResult = new Dictionary<string, IEnumerable<BinanceKlineModel>>();
            try
            {
                var option = GetKlineInterval();
                var lTask = new List<Task>();
                var binanceClient = new BinanceClient();
                foreach (var item in model.Coins)
                {
                    var task = Task.Run(() =>
                    {
                        var lData = binanceClient.SpotApi.ExchangeData.GetKlinesAsync(item, option).GetAwaiter().GetResult().Data;
                        if (lData == null)
                        {
                            dicResult.Add(item, new List<BinanceKlineModel>());
                            return;
                        }

                        var lResult = lData.Select(x => new BinanceKlineModel
                        {
                            Volume = x.Volume,
                            Close = x.ClosePrice,
                            CloseTime = ((DateTimeOffset)x.CloseTime).ToUnixTimeMilliseconds(),
                            High = x.HighPrice,
                            Low = x.LowPrice,
                            Open = x.OpenPrice,
                            OpenTime = ((DateTimeOffset)x.OpenTime).ToUnixTimeMilliseconds(),
                            QuoteVolume = x.QuoteVolume,
                            TakerBuyBaseVolume = x.TakerBuyBaseVolume,
                            TakerBuyQuoteVolume = x.TakerBuyQuoteVolume,
                        });
                        dicResult.Add(item, lResult);
                    });
                    lTask.Add(task);
                }
                Task.WaitAll(lTask.ToArray());
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, ex.Message);
            }

            return dicResult;

            KlineInterval GetKlineInterval()
            {
                switch ((enumInterval)model.Interval)
                {
                    case enumInterval.FifteenMinute: return KlineInterval.FifteenMinutes;
                    case enumInterval.OneHour: return KlineInterval.OneHour;
                    case enumInterval.FourHour: return KlineInterval.FourHour;
                    case enumInterval.OneDay: return KlineInterval.OneDay;
                    case enumInterval.OneWeek: return KlineInterval.OneWeek;
                    case enumInterval.OneMonth: return KlineInterval.OneMonth;
                    default:
                        break;
                }
                return KlineInterval.FifteenMinutes;
            }
        }
    }
}

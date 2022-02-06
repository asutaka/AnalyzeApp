using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Model;

namespace AnalyzeApp.API.Impl
{
    public class AnalyzeService : IAnalyzeService
    {
        private readonly IAnalyzeRepo _repo;
        public AnalyzeService(IAnalyzeRepo repo)
        {
            _repo = repo;
        }
        public async Task<int> CreateTableCoin(string coin)
        {
            return await _repo.CreateTableCoin(coin);
        }

        public async Task<int> DeleteCoin(string coin, double time)
        {
            return await _repo.DeleteCoin(coin, time);
        }

        public async Task<List<DataModel>> GetData(string coin, int top)
        {
            return await _repo.GetData(coin, top);
        }

        public async Task<List<SettingModel>> GetSettings()
        {
            return await _repo.GetSettings();
        }

        public async Task<UserModel> GetUser()
        {
            return await _repo.GetUser();
        }

        public async Task<int> InsertCoin(DataModel model)
        {
            return await _repo.InsertCoin(model);
        }

        public async Task<List<NotifyModel>> GetNotify(int top)
        {
            return await _repo.GetNotify(top);
        }

        public async Task<int> InsertNotify(NotifyModel model)
        {
            return await _repo.InsertNotify(model);    
        }

        public async Task<int> DeleteNotify(double time)
        {
            return await _repo.DeleteNotify(time);
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
    }
}

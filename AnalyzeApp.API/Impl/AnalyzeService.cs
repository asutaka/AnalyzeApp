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

        public async Task<List<SettingModel>> GetSettings()
        {
            return await _repo.GetSettings();
        }

        public async Task<UserModel> GetUser()
        {
            return await _repo.GetUser();
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

        public async Task<int> InsertUser(UserModel model)
        {
            return await _repo.InsertUser(model);
        }

        public async Task<int> DeleteUser()
        {
            return await _repo.DeleteUser();
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
    }
}

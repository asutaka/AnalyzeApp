using AnalyzeApp.API.Model;

namespace AnalyzeApp.API.Interface
{
    public interface IAnalyzeRepo
    {
        Task<UserModel> GetUser();
        Task<int> InsertUser(UserModel model);
        Task<int> DeleteUser();
        Task<List<SettingModel>> GetSettings();
        Task<int> InsertSetting(SettingModel model);
        Task<int> UpdateSetting(SettingModel model);
        Task<int> DeleteSetting(int id);
        Task<int> CreateTableCoin(string coin);
        Task<int> InsertCoin(DataModel model);
        Task<int> DeleteCoin(string coin, double time);
        Task<List<DataModel>> GetData(string coin, int top);
        Task<List<NotifyModel>> GetNotify(int top);
        Task<int> InsertNotify(NotifyModel model);
        Task<int> DeleteNotify(double time);
    }
}

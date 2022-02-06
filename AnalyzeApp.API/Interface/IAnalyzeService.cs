using AnalyzeApp.API.Model;

namespace AnalyzeApp.API.Interface
{
    public interface IAnalyzeService
    {
        Task<UserModel> GetUser();
        Task<List<SettingModel>> GetSettings();
        Task<int> CreateTableCoin(string coin);
        Task<int> InsertCoin(DataModel model);
        Task<int> DeleteCoin(string coin, double time);
        Task<List<DataModel>> GetData(string coin, int top);
        Task<List<NotifyModel>> GetNotify(int top);
        Task<int> InsertNotify(NotifyModel model);
        Task<int> DeleteNotify(double time);
        Task<bool> Verify(VerifyModel model);
    }
}

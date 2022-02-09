using AnalyzeApp.API.Model;

namespace AnalyzeApp.API.Interface
{
    public interface IAnalyzeService
    {
        Task<UserModel> GetUser();
        Task<int> InsertUser(UserModel model);
        Task<int> DeleteUser();

        Task<List<SettingModel>> GetSettings();
        Task<int> InsertSetting(SettingModel model);
        Task<int> UpdateSetting(SettingModel model);
        Task<int> DeleteSetting(int id);

        Task<List<DataSettingModel>> GetDataSettings();
        Task<int> InsertDataSettings(DataSettingModel model);
        Task<int> UpdateDataSettings(DataSettingModel model);
        Task<int> DeleteDataSettings(int interval);

        Task<int> InsertNotify(NotifyModel model);
        Task<List<NotifyModel>> GetNotify(int top);
        Task<bool> Verify(VerifyModel model);
        Task<ConfigTableModel> GetConfigTable();
        Task<int> UpdateConfigTable(ConfigTableModel model);
    }
}

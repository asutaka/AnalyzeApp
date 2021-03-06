using AnalyzeApp.API.Model;

namespace AnalyzeApp.API.Interface
{
    public interface IAnalyzeRepo
    {
        Task<ProfileModel> GetProfile();
        Task<int> InsertProfile(ProfileModel model);
        Task<int> DeleteProfile();
        Task<List<SettingModel>> GetSettings();
        Task<int> InsertSetting(SettingModel model);
        Task<int> UpdateSetting(SettingModel model);
        Task<int> DeleteSetting(int id);
        Task<List<DataSettingModel>> GetDataSettings();
        Task<int> InsertDataSettings(DataSettingModel model);
        Task<int> UpdateDataSettings(DataSettingModel model);
        Task<int> DeleteDataSettings(int id);
        Task<List<NotifyModel>> GetNotify(int top);
        Task<int> InsertNotify(NotifyModel model);
        Task<int> DeleteNotify(double time);
        Task<ConfigTableModel> GetConfigTable();
        Task<int> UpdateConfigTable(ConfigTableModel model);
    }
}

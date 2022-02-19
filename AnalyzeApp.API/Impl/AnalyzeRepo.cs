using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace AnalyzeApp.API.Impl
{
    public class AnalyzeRepo : IAnalyzeRepo
    {
        protected readonly string _connString;
        public AnalyzeRepo(string connectionString)
        {
            _connString = !string.IsNullOrWhiteSpace(connectionString) ?
                connectionString :
                throw new ArgumentNullException(nameof(connectionString));
        }
        public async Task<ProfileModel> GetProfile()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<ProfileModel>("select * from ProfileTable", commandTimeout: 5, commandType: CommandType.Text);
                    if (result != null && result.Any())
                        return result.First();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetProfile: {ex.Message}");
            }
            return new ProfileModel();
        }

        public async Task<int> InsertProfile(ProfileModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Phone", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Phone);
                    parameter.Add(name: "@Code", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Code);
                    parameter.Add(name: "@Email", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Email);
                    parameter.Add(name: "@LinkAvatar", dbType: DbType.String, direction: ParameterDirection.Input, value: model.LinkAvatar);
                    parameter.Add(name: "@IsNotify", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: model.IsNotify);
                    var result = await conn.ExecuteAsync($"INSERT INTO ProfileTable(Phone, Code, Email, LinkAvatar, IsNotify) VALUES (@Phone, @Code, @Email, @LinkAvatar, @IsNotify)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertProfile: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteProfile()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync($"DELETE from ProfileTable", commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteProfile: {ex.Message}");
            }
            return -1;
        }

        public async Task<List<SettingModel>> GetSettings()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<SettingModel>("select * from SettingTable", commandTimeout: 5, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetSettings: {ex.Message}");
            }
            return new List<SettingModel>();
        }

        public async Task<int> InsertSetting(SettingModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.Id);
                    parameter.Add(name: "@Setting", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Setting);
                    var result = await conn.ExecuteAsync($"INSERT INTO SettingTable(Id, Setting) VALUES (@Id, @Setting)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> UpdateSetting(SettingModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.Id);
                    parameter.Add(name: "@Setting", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Setting);

                    var result = await conn.ExecuteAsync($"update SettingTable set Setting = @Setting where Id = @Id", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:UpdateSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteSetting(int id)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: id);
                    var result = await conn.ExecuteAsync($"delete from SettingTable where Id = @Id", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteSetting: {ex.Message}");
            }
            return -1;
        }

        public async Task<List<NotifyModel>> GetNotify(int top)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Top", dbType: DbType.Int32, direction: ParameterDirection.Input, value: top);
                    var list = await conn.QueryAsync<NotifyModel>($"select * from NotifyTable order by timecreate asc limit @Top", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetNotify: {ex.Message}");
            }
            return new List<NotifyModel>();
        }

        public async Task<int> InsertNotify(NotifyModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@TimeCreate", dbType: DbType.Double, direction: ParameterDirection.Input, value: DateTimeOffset.Now.Ticks);
                    parameter.Add(name: "@Phone", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Phone);
                    parameter.Add(name: "@Content", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Content);
                    parameter.Add(name: "@IsService", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: model.IsService);
                    var result = await conn.ExecuteAsync($"INSERT INTO NotifyTable(TimeCreate, Phone, Content, IsService) VALUES (@TimeCreate,@Phone,@Content, @IsService)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertNotify: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteNotify(double time)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@TimeCreate", dbType: DbType.Double, direction: ParameterDirection.Input, value: time);
                    var result = await conn.ExecuteAsync($"DELETE from NotifyTable WHERE TimeCreate = @TimeCreate", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteNotify: {ex.Message}");
            }
            return -1;
        }

        public async Task<List<DataSettingModel>> GetDataSettings()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<DataSettingModel>("select * from DataTable", commandTimeout: 5, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetDataSettings: {ex.Message}");
            }
            return new List<DataSettingModel>();
        }

        public async Task<int> InsertDataSettings(DataSettingModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Interval", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.Interval);
                    parameter.Add(name: "@UpdatedTime", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.UpdatedTime);
                    var result = await conn.ExecuteAsync($"INSERT INTO DataTable(Interval, UpdatedTime) VALUES (@Interval, @UpdatedTime)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> UpdateDataSettings(DataSettingModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Interval", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.Interval);
                    parameter.Add(name: "@UpdatedTime", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.UpdatedTime);

                    var result = await conn.ExecuteAsync($"update DataTable set UpdatedTime = @UpdatedTime where Interval = @Interval", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:UpdateDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteDataSettings(int interval)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Interval", dbType: DbType.Int32, direction: ParameterDirection.Input, value: interval);
                    var result = await conn.ExecuteAsync($"delete from DataTable where Inteval = @Interval", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteDataSettings: {ex.Message}");
            }
            return -1;
        }

        public async Task<ConfigTableModel> GetConfigTable()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<ConfigTableModel>("select * from ConfigTable", commandTimeout: 5, commandType: CommandType.Text);
                    if (result != null && result.Any())
                        return result.First();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetConfigTable: {ex.Message}");
            }
            return null;
        }

        public async Task<int> UpdateConfigTable(ConfigTableModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@StatusLoadData", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.StatusLoadData);

                    var result = await conn.ExecuteAsync($"update ConfigTable set StatusLoadData = @StatusLoadData", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:UpdateConfigTable: {ex.Message}");
            }
            return -1;
        }
    }
}

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
        public async Task<UserModel> GetUser()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<UserModel>("select * from UserTable", commandTimeout: 5, commandType: CommandType.Text);
                    if (result != null && result.Any())
                        return result.First();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetUser: {ex.Message}");
            }
            return new UserModel();
        }

        public async Task<int> InsertUser(UserModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Phone", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Phone);
                    parameter.Add(name: "@Code", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Code);
                    var result = await conn.ExecuteAsync($"INSERT INTO UserTable(Phone, Code) VALUES (@Phone, @Code)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertUser: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteUser()
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync($"DELETE from UserTable", commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteUser: {ex.Message}");
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

                    var result = await conn.ExecuteAsync($"update from SettingTable set Setting = @Setting where Id = @Id", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
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

        public async Task<int> CreateTableCoin(string coin)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Tbl", dbType: DbType.String, direction: ParameterDirection.Input, value: coin);
                    var result = await conn.ExecuteAsync($"CREATE TABLE @Tbl (T real, O real, H real, L real, C real, V real,ValType integer)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:CreateTableCoin: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> InsertCoin(DataModel model)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Tbl", dbType: DbType.String, direction: ParameterDirection.Input, value: model.Coin);
                    parameter.Add(name: "@T", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.T);
                    parameter.Add(name: "@O", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.O);
                    parameter.Add(name: "@H", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.H);
                    parameter.Add(name: "@L", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.L);
                    parameter.Add(name: "@C", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.C);
                    parameter.Add(name: "@V", dbType: DbType.Double, direction: ParameterDirection.Input, value: model.V);
                    parameter.Add(name: "@ValType", dbType: DbType.Int32, direction: ParameterDirection.Input, value: model.ValType);
                    var result = await conn.ExecuteAsync($"INSERT INTO @Tbl(T, O, H, L, C, V, ValType) VALUES (@T,@O,@H,@L,@C,@V,@ValType)", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:InsertCoin: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> DeleteCoin(string coin, double time)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Tbl", dbType: DbType.String, direction: ParameterDirection.Input, value: coin);
                    parameter.Add(name: "@T", dbType: DbType.Double, direction: ParameterDirection.Input, value: time);
                    var result = await conn.ExecuteAsync($"DELETE from @Tbl WHERE T < @T", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteCoin: {ex.Message}");
            }
            return -1;
        }

        public async Task<List<DataModel>> GetData(string coin, int top)
        {
            try
            {
                using (var conn = new SqliteConnection(_connString))
                {
                    await conn.OpenAsync();
                    var parameter = new DynamicParameters();
                    parameter.Add(name: "@Tbl", dbType: DbType.String, direction: ParameterDirection.Input, value: coin);
                    parameter.Add(name: "@Top", dbType: DbType.Int32, direction: ParameterDirection.Input, value: top);
                    var list = await conn.QueryAsync<DataModel>($"select * from @Tbl order by T desc LIMIT @Top", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:GetData: {ex.Message}");
            }
            return new List<DataModel>();
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
                    var list = await conn.QueryAsync<NotifyModel>($"select * from NotifyTable order by timecreate desc limit @Top", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
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
                    parameter.Add(name: "@T", dbType: DbType.Double, direction: ParameterDirection.Input, value: time);
                    var result = await conn.ExecuteAsync($"DELETE from NotifyTable WHERE T = @T", param: parameter, commandTimeout: 5, commandType: CommandType.Text);
                    return result;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AnalyzeRepo:DeleteNotify: {ex.Message}");
            }
            return -1;
        }
    }
}

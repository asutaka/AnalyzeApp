using System.ComponentModel.DataAnnotations;
using TL;
using WTelegram;

namespace AnalyzeApp.API
{
    public class TeleClient
    {
        private static Client _clientService, _clientSupport;
        private static string _api_id, _api_hash, _phone_number, _session_pathname, _verification_code;
        private static string Config(string what)
        {
            switch (what)
            {
                case "api_id": return _api_id;
                case "api_hash": return _api_hash;
                case "phone_number": return _phone_number;
                case "session_pathname": return _session_pathname;
                case "verification_code": return _verification_code;
                default: return null;
            }
        }

        private static async Task<bool> InitTelegram(bool isService)
        {
            try
            {
                if (isService)
                {
                    _api_id = ConstVal.apiIdService;
                    _api_hash = ConstVal.apiHashService;
                    _phone_number = ConstVal.phoneService;
                    _session_pathname = $"{_phone_number.Replace("+", "")}.session";

                    _clientService = new Client(Config);
                    await _clientService.ConnectAsync();
                }
                else
                {
                    _api_id = ConstVal.apiIdSupport;
                    _api_hash = ConstVal.apiHashSupport;
                    _phone_number = ConstVal.phoneSupport;
                    _session_pathname = $"{_phone_number.Replace("+", "")}.session";

                    _clientSupport = new Client(Config);
                    await _clientSupport.ConnectAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"TeleClient:GenerateSession: {ex.Message}");
                return false;
            }
        }

        public static async Task<int> SendMessage(string phone, string content, bool isService = false)
        {
            try
            {
                if (!await InitTelegram(isService))
                    return (int)enumTelegramSendMessage.UnknownError;
                _clientSupport = new Client(Config);
                await _clientSupport.ConnectAsync();
                var phoneUser = phone.PhoneFormat();
                if (string.IsNullOrWhiteSpace(phoneUser))
                    return (int)enumTelegramSendMessage.PhoneInValid;
                if (isService)
                {
                    var result = await _clientService.Contacts_ImportContacts(new[] { new InputPhoneContact { phone = phoneUser } });
                    if (result != null)
                    {
                        await _clientService.SendMessageAsync(result.users.First().Value, content);
                    }
                }
                else
                {
                    var result = await _clientSupport.Contacts_ImportContacts(new[] { new InputPhoneContact { phone = phoneUser } });
                    if (result != null)
                    {
                        await _clientSupport.SendMessageAsync(result.users.First().Value, content);
                    }
                }
                Thread.Sleep(1000);
                return (int)enumTelegramSendMessage.Success;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"TeleClient:SendMessage:  { ex.Message }");
                return (int)enumTelegramSendMessage.UnknownError;
            }
        }

        public static async Task<bool> GenerateSession(string phoneNumber, string apiId, string apiHash, string verifyCode, bool isService)
        {
            try
            {
                _api_id = apiId;
                _api_hash = apiHash;
                _phone_number = phoneNumber;
                _session_pathname = $"{_phone_number.Replace("+", "")}.session";
                _verification_code = verifyCode;
                if (isService)
                {
                    _clientService = new Client(Config);
                    await _clientService.ConnectAsync();
                    var user = await _clientService.LoginUserIfNeeded();
                    NLogLogger.LogInfo($"You are logged-in as {user.username ?? user.first_name + " " + user.last_name} (id {user.id})");
                }
                else
                {
                    _clientSupport = new Client(Config);
                    await _clientSupport.ConnectAsync();
                    var user = await _clientSupport.LoginUserIfNeeded();
                    NLogLogger.LogInfo($"You are logged-in as {user.username ?? user.first_name + " " + user.last_name} (id {user.id})");
                }
                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"TeleClient|GenerateSession: {ex.Message}");
                return false;
            }
        }

        private enum enumTelegramSendMessage
        {
            [Display(Name = "Thành công")]
            Success = 0,
            [Display(Name = "SĐT không hợp lệ")]
            PhoneInValid = -1,
            [Display(Name = "Telegram không sẵn sàng")]
            TelegramNotReady = -2,
            [Display(Name = "Lỗi không xác định")]
            UnknownError = -99
        }
    }
}

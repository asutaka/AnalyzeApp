using System.ComponentModel.DataAnnotations;

namespace AnalyzeApp.API
{
    public enum enumTelegramSendMessage
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

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

    public enum enumInterval
    {
        [Display(Name = "15m")]
        FifteenMinute = 0,
        [Display(Name = "1h")]
        OneHour = 1,
        [Display(Name = "4h")]
        FourHour = 2,
        [Display(Name = "1d")]
        OneDay = 3,
        [Display(Name = "1w")]
        OneWeek = 4,
        [Display(Name = "1m")]
        OneMonth = 5,
    }

    public enum enumSetting
    {
        [Display(Name = "Trade List")]
        TradeList = 1,
        [Display(Name = "Follow List")]
        FollowList = 2,
        [Display(Name = "Black List")]
        BlackList = 3,
        [Display(Name = "Realtime List")]
        RealtimeList = 4,
        [Display(Name = "Basic Setting")]
        BasicSetting = 5,
        [Display(Name = "Special Setting")]
        SpecialSetting = 6,
        [Display(Name = "Advance Setting 1")]
        AdvanceSetting1 = 7,
        [Display(Name = "Advance Setting 2")]
        AdvanceSetting2 = 8,
        [Display(Name = "Advance Setting 3")]
        AdvanceSetting3 = 9,
        [Display(Name = "Advance Setting 4")]
        AdvanceSetting4 = 10,
    }
}

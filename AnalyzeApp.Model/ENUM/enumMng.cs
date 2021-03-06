using System.ComponentModel.DataAnnotations;

namespace AnalyzeApp.Model.ENUM
{
    public enum enumTimeZone
    {
        [Display(Name = "15 Phút")]
        FifteenMinute = 15,
        [Display(Name = "1 Giờ")]
        OneHour = 60,
        [Display(Name = "4 Giờ")]
        FourHour = 240,
        [Display(Name = "1 Ngày")]
        OneDay = 1440,
        [Display(Name = "1 Tuần")]
        OneWeek = 10080,
        [Display(Name = "1 Tháng")]
        OneMonth = 302400,
    }
    public enum enumInterval
    {
        [Display(Name = "15m")]
        FifteenMinute = 15,
        [Display(Name = "1h")]
        OneHour = 60,
        [Display(Name = "4h")]
        FourHour = 240,
        [Display(Name = "1d")]
        OneDay = 1440,
        [Display(Name = "1w")]
        OneWeek = 10080,
        [Display(Name = "1m")]
        OneMonth = 302400,
    }
    public enum enumCandleStick
    {
        [Display(Name = "Open")]
        O = 0,
        [Display(Name = "High")]
        H = 1,
        [Display(Name = "Low")]
        L = 2,
        [Display(Name = "Close")]
        C = 3
    }
    public enum enumChooseData
    {
        [Display(Name = "MA")]
        MA = 0,
        [Display(Name = "EMA")]
        EMA = 1,
        [Display(Name = "Volumne")]
        Volumne = 2,
        [Display(Name = "Nến 1")]
        CandleStick_1 = 3,
        [Display(Name = "Nến 2")]
        CandleStick_2 = 4,
        [Display(Name = "MACD")]
        MACD = 5,
        [Display(Name = "RSI")]
        RSI = 6,
        [Display(Name = "ADX")]
        ADX = 7,
        [Display(Name = "Giá hiện tại")]
        CurrentValue = 8,
        [Display(Name = "MCDX")]
        MCDX = 9
    }
    public enum enumUnit
    {
        [Display(Name = "%")]
        Ratio = 0,
        [Display(Name = "giá trị")]
        Value = 1,
    }
    public enum enumOperator
    {
        [Display(Name = ">")]
        Greater = 0,
        [Display(Name = ">=")]
        GreaterOrEqual = 1,
        [Display(Name = "=")]
        Equal = 2,
        [Display(Name = "<=")]
        LessThanOrEqual = 3,
        [Display(Name = "<")]
        LessThan = 4
    }

    public enum enumOptionTime
    {
        [Display(Name = "3 ngày")]
        ThreeDays = 1,
        [Display(Name = "1 tháng")]
        AMonth = 2,
        [Display(Name = "3 tháng")]
        ThreeMonths = 3,
        [Display(Name = "6 tháng")]
        SixMonths = 4,
        [Display(Name = "1 năm")]
        AYear = 5
    }

    public enum enumTime
    {
        Seconds = 1,
        Minutes = 2,
        Hours = 3,
        DayOfMonth = 4,
        Month = 5,
        DayOfWeek = 6,
        Year = 7,
    }

    public enum enumPriority
    {
        [Display(Name = "Ưu tiên thấp")]
        Low = 0,
        [Display(Name = "Ưu tiên")]
        Normal = 1,
        [Display(Name = "Ưu tiên cao")]
        High = 2,
        [Display(Name = "Quan trọng")]
        Important = 3,
        [Display(Name = "Rất quan trọng")]
        VeryImportant = 4
    }

    public enum enumIntervalNotify
    {
        [Display(Name = "Một phút/ lần")]
        OneMinute = 60,
        [Display(Name = "Hai phút/ lần")]
        TwoMinute = 120,
        [Display(Name = "Năm phút/ lần")]
        FiveMinute = 300,
        [Display(Name = "Mười phút/ lần")]
        TenMinute = 600,
        [Display(Name = "Mười năm phút/ lần")]
        FifteenMinute = 900,
        [Display(Name = "Ba mươi phút/ lần")]
        ThirtyMintue = 1800,
        [Display(Name = "Một giờ/ lần")]
        OneHour = 3600,
        [Display(Name = "Hai giờ/ lần")]
        TwoHour = 7200,
        [Display(Name = "Bốn giờ/ lần")]
        FourHour = 14400,
        [Display(Name = "Năm giờ/ lần")]
        FiveHour = 18000,
        [Display(Name = "Mười hai giờ/ lần")]
        TwelveHour = 43200,
        [Display(Name = "Một ngày/ lần")]
        OneDay = 86400
    }

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

    public enum enumStatusLoadData
    {
        Loading = 1,
        Complete1H = 2,
        Complete15M = 3,
        Complete4H = 4,
        Complete1D = 5,
        Complete1W = 6,
        Complete1M = 7,
        ErrorLoad = 8,
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
        [Display(Name = "Advance Setting")]
        AdvanceSetting = 6,
    }

    public enum enumAboveBelow
    {
        [Display(Name = "Vượt trên")]
        Above = 1,
        [Display(Name = "Giảm dưới")]
        Below = 2,
    }

    public enum enumMA
    {
        [Display(Name = "MA")]
        MA = 1,
        [Display(Name = "EMA")]
        EMA = 2
    }

    public enum enumCross
    {
        [Display(Name = "Cắt lên")]
        Cross_Above = 1,
        [Display(Name = "Cắt xuống")]
        Cross_Below = 2,
        [Display(Name = "Gần cắt lên")]
        Cross_NearAbove = 3,
        [Display(Name = "Gần cắt xuống")]
        Cross_NearBelow = 4,
    }
}

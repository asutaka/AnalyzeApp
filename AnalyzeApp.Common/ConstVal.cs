namespace AnalyzeApp.Common
{
    public static class ConstVal
    {
        //Signin Google
        public const string clientId = "982215624266-3t08bs3sqk8fv1f0c36pe14mjp38daqq.apps.googleusercontent.com";
        public const string clientSecret = "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";
        public const string scopes = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public const string redirectURI = "urn:ietf:wg:oauth:2.0:oob";
        //Security
        public const string PasswordHash = "P@$Sw0rd";
        public const string SaltKey = "Phunv@AI";
        public const string VIKey = "Phunv@Huy456aA@1";
        //Time
        public const string timeURL = "https://api.timezonedb.com/v2/get-time-zone?key=PJBB6AVAAMO0&format=json&by=zone&zone=Asia/Bangkok";
        //Coin
        public const string COIN_LIST = "https://www.binance.com/bapi/asset/v2/public/asset-service/product/get-products?includeEtf=true";
        public const string COIN_DETAIL = "https://www.binance.com/api/v3/klines?";
        public const string COIN_SINGLE = "https://www.binance.com/vi/trade/";
        public const string COIN_VAL = "https://www.binance.com/api/v3/ticker/price?";
        public const string COIN_24H = "https://www.binance.com/api/v3/ticker/24hr?";
        public const string COIN_DEPT = "https://www.binance.com/api/v3/depth?";
        public const string COIN_TRADE = "https://www.binance.com/api/v3/trades?";
        public const string COIN_AggTRADE = "https://www.binance.com/api/v3/aggTrades?";
        //Telegram Buy Code
        public const string strService = "Danh sách gói code:" +
                                         "\nGói dùng thử 3 ngày: miễn phí(duy nhất 1 lần) - Mã: FREE" +
                                         "\nGói 1 tháng: xK/tháng - Mã: 1M" +
                                         "\nGói 3 tháng: yK/tháng - Mã: 3M" +
                                         "\nGói 6 tháng: zK/tháng - Mã: 6M" +
                                         "\nGói 1 năm: tK/tháng - Mã: 1Y" +
                                         "\n\nVui lòng chat tại đây để nhận tư vấn hoặc" +
                                         "\nchuyển tiền với nội dung: DK Mã_gói Email_sử_dụng SĐT_Telegram" +
                                         "\ntới STK:xxx - tại ngân hàng VCB, người nhận: Nguyễn Văn A";
        public const string phoneService = "+84582208920";
        public const string apiIdService = "5128731";
        public const string apiHashService = "a894a079cfeadf0bd0646f53bf2587c6";
        //Telegram Support
        public const string strSupport = "<Tin nhắn tự động>Bạn cần hỗ trợ gì?";
        public const string phoneSupport = "+84978894272";
        public const string apiIdSupport = "10685601";
        public const string apiHashSupport = "ea3a6821783d84a2c919c049a060d67f";
        //Google Drive
        public const string strVersion = "https://drive.google.com/file/d/1o-ahy7uHCK_sVkdJ_G7iaVWyDGUU7Nli/view?usp=sharing";
        public const string strBotBuyCode = "https://drive.google.com/file/d/1hakAvnesXwXbr4WsHx4ilZhfJPa4PjFi/view?usp=sharing";
        public const string strBotSupport = "https://drive.google.com/file/d/1hakAvnesXwXbr4WsHx4ilZhfJPa4PjFi/view?usp=sharing";
        //
        public const string api = "http://localhost:12399";
        public const string appName = "AnalyzeApp";
        public const string serviceName = "AnalyzeApp.TelegramService";
        public const string dataServiceName = "AnalyzeApp.DataService";
    }
}

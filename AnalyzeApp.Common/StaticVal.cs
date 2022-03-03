using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using System.Collections.Generic;

namespace AnalyzeApp.Common
{
    public static class StaticVal
    {
        //Variables
        public static BinanceClient binanceClient = new BinanceClient(new BinanceClientOptions { 
        });
        public static BinanceSocketClient binanceSocketClient = new BinanceSocketClient(new BinanceSocketClientOptions() { AutoReconnect = true});//, SocketNoDataTimeout = System.TimeSpan.FromSeconds(10)
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();

        //Flag
        public static bool IsRealTimeAction = false;
        public static bool IsExecMCDX = false;
        public static bool IsAccessMain = false;
        public static bool IsExecCheckCodeActive = false;
        public static bool IsCodeActive = false;
        public static bool IsTradeListChange = false;
        //Level
        public static int Level { get; set; }
        //Data Coin
        public static Dictionary<string, IEnumerable<BinanceKline>> dic15M = new Dictionary<string, IEnumerable<BinanceKline>>();
        public static Dictionary<string, IEnumerable<BinanceKline>> dic1H = new Dictionary<string, IEnumerable<BinanceKline>>();
        public static Dictionary<string, IEnumerable<BinanceKline>> dic4H = new Dictionary<string, IEnumerable<BinanceKline>>();
        public static Dictionary<string, IEnumerable<BinanceKline>> dic1D = new Dictionary<string, IEnumerable<BinanceKline>>();
        public static Dictionary<string, IEnumerable<BinanceKline>> dic1W = new Dictionary<string, IEnumerable<BinanceKline>>();
        public static Dictionary<string, IEnumerable<BinanceKline>> dic1Month = new Dictionary<string, IEnumerable<BinanceKline>>();
        //Local Data
        public static List<IBinanceTick> binanceTicks = new List<IBinanceTick>();
        public static List<CryptonDetailDataModel> lstCoin = new List<CryptonDetailDataModel>();
        public static List<CryptonDetailDataModel> lstCoinFilter = new List<CryptonDetailDataModel>();
        public static List<Top30Model> lstCryptonRank = new List<Top30Model>();
        //Display Data
        public static List<Top30Model> lstRealTimeDisplay = new List<Top30Model>();
        public static List<MCDXModel> lstMCDX = new List<MCDXModel>();
        public static List<SendNotifyModel> lstNotiTrade = new List<SendNotifyModel>();
        //Scron
        //Scron <second> <minute> <hour> <day-of-month> <month> <day-of-week> <year>
        public static string Scron_Top30 = "* * * * * ?";
        public static string Scron_WatchList = "* * * * * ?";
        public static string Scron_CheckStatus = "2 0 0/5 * * ?";
        public static string Scron_MCDX_Calculate = "2 0/5 * * * ?";
        public static string Scron_MCDX_Value = "* * * * * ?";
        public static string Scron_TradeList_Noti = "* * * * * ?";
        //Profile
        public static ProfileModel profile;
    }
}

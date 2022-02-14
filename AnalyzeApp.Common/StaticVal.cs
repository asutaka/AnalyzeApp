using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using System.Collections.Generic;

namespace AnalyzeApp.Common
{
    public static class StaticVal
    {
        //Variables
        public static BinanceClient binanceClient = new BinanceClient(new BinanceClientOptions() { });
        public static BinanceSocketClient binanceSocketClient = new BinanceSocketClient(new BinanceSocketClientOptions() { });
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
        //Flag
        public static bool IsRealTimeAction = false;
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
        //Scron
        //Scron <second> <minute> <hour> <day-of-month> <month> <day-of-week> <year>
        public static string Scron_Top30 = "* * * * * ?";
    }
}

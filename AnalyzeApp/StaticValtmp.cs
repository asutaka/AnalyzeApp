using AnalyzeApp.GUI;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.MarketData;
using System.Collections.Generic;
using System.Net.Http;

namespace AnalyzeApp
{
    public static class StaticValtmp
    {
        //Profile
        public static ProfileModel profile;
        //Flag
        public static bool IsAccessMain = false;
        public static bool IsExecCheckCodeActive = false;
        public static bool IsCodeActive = false;
        public static bool IsExecMCDX = false;
        public static bool IsTradeListChange = false;
        public static bool IsRealTimeDeleted = false;
        //Level
        public static int Level { get; set; }
        //main
        public static frmMain frmMainObj = null;
        
        //Scron <second> <minute> <hour> <day-of-month> <month> <day-of-week> <year>
        public static string Scron_CheckStatus = "0 0 0/5 * * ?";
        public static string Scron_MCDX_Calculate = "0 0/5 * * * ?";
        public static string Scron_MCDX_Value = "0/1 * * * * ?";
        public static string Scron_Top30_Value = "* * * * * ?";
        public static string Scron_TradeList_Noti = "0/1 * * * * ?";
        //Coin
        
        
        public static List<MCDXModel> lstMCDX = new List<MCDXModel>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource15M = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource4H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1D = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1W = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1Month = new Dictionary<string, List<CandleStickDataModel>>();

        

        public static List<Top30Model> lstRealTimeShow = new List<Top30Model>();
        //Local 
        public static List<SendNotifyModel> lstNotiTrade = new List<SendNotifyModel>();
        //Client
        public static HttpClient client = new HttpClient();
        
        //
        

        //
        public static IEnumerable<IBinanceTick> source;
    }
}

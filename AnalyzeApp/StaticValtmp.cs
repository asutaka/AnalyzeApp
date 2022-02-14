using AnalyzeApp.GUI;
using AnalyzeApp.Model.ENTITY;
using Binance.Net.Interfaces;
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
        //Level
        public static int Level { get; set; }
        //main
        public static frmMain frmMainObj = null;
        
        
        public static string Scron_CheckStatus = "0 0 0/5 * * ?";
        public static string Scron_MCDX_Calculate = "0 0/5 * * * ?";
        public static string Scron_MCDX_Value = "0/1 * * * * ?";
        
        public static string Scron_TradeList_Noti = "0/1 * * * * ?";
        //Coin
        
        
        public static List<MCDXModel> lstMCDX = new List<MCDXModel>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource15M = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource4H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1D = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1W = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1Month = new Dictionary<string, List<CandleStickDataModel>>();

        

        
        //Local 
        public static List<SendNotifyModel> lstNotiTrade = new List<SendNotifyModel>();
        //Client
        public static HttpClient client = new HttpClient();
        
        //
        

        //
        public static IEnumerable<IBinanceTick> source;
    }
}

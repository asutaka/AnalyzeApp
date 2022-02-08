﻿using AnalyzeApp.GUI;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using System.Collections.Generic;
using System.Net.Http;

namespace AnalyzeApp
{
    public static class StaticVal
    {
        //Profile
        public static ProfileModel profile;
        //Flag
        public static bool IsAccessMain = false;
        public static bool IsExecCheckCodeActive = false;
        public static bool IsCodeActive = false;
        public static bool IsExecMCDX = false;
        public static bool IsExecTop30 = false;
        public static bool IsTradeListChange = false;
        public static bool IsRealTimeDeleted = false;
        //Level
        public static int Level { get; set; }
        //main
        public static frmMain frmMainObj = null;
        public static ScheduleMng ScheduleMngObj = ScheduleMng.Instance();
        //Scron <second> <minute> <hour> <day-of-month> <month> <day-of-week> <year>
        public static string Scron_CheckStatus = "0 0 0/5 * * ?";
        public static string Scron_MCDX_Calculate = "0 0/5 * * * ?";
        public static string Scron_MCDX_Value = "0/1 * * * * ?";
        public static string Scron_Top30_Calculate = "0 0 0/6 * * ?";
        public static string Scron_Top30_Value = "0/1 * * * * ?";
        public static string Scron_TradeList_Noti = "0/1 * * * * ?";
        //Coin
        public static List<CryptonDetailDataModel> lstCoin = new List<CryptonDetailDataModel>();
        public static List<CryptonDetailDataModel> lstCoinFilter = new List<CryptonDetailDataModel>();
        public static List<Top30Model> lstCryptonRank = new List<Top30Model>();
        public static List<MCDXModel> lstMCDX = new List<MCDXModel>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource15M = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource4H = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1D = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1W = new Dictionary<string, List<CandleStickDataModel>>();
        public static Dictionary<string, List<CandleStickDataModel>> dicDatasource1Month = new Dictionary<string, List<CandleStickDataModel>>();
        //Setting Model
        public static BasicSettingModel basicModel;
        public static SpecialSettingModel specialModel;
        public static AdvanceSettingModel advanceModel1;
        public static AdvanceSettingModel advanceModel2;
        public static AdvanceSettingModel advanceModel3;
        public static AdvanceSettingModel advanceModel4;

        public static List<CryptonDetailDataModel> lstBlackList;
        public static List<CryptonDetailDataModel> lstRealTime;
        public static List<Top30Model> lstRealTimeShow = new List<Top30Model>();
        public static TradeListModel tradeList;
        public static FollowModel followList;
        //Local 
        public static List<SendNotifyModel> lstNotiTrade = new List<SendNotifyModel>();
        //Client
        public static HttpClient client = new HttpClient();
        //
        public static IEnumerable<Binance.Net.Interfaces.IBinanceTick> source;
    }
}

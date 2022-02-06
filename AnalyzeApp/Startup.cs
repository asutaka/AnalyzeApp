using AnalyzeApp.Common;
using AnalyzeApp.Data;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using AnalyzeApp.Model.ENTITY;
using Quartz;
using System.Collections.Generic;

namespace AnalyzeApp
{
    public class Startup
    {
        private Startup()
        {
            InitData();
        }
        private static Startup _instance = null;
        public static Startup Instance()
        {
            _instance = _instance ?? new Startup();
            return _instance;
        }
        private void InitData()
        {
            //Load JSonFile
            StaticVal.basicModel = new BasicSettingModel().LoadJsonFile("basic_setting.json");
            var obj = new AdvanceSettingModel();
            StaticVal.advanceModel1 = obj.LoadJsonFile("advance_setting1.json");
            StaticVal.advanceModel2 = obj.LoadJsonFile("advance_setting2.json");
            StaticVal.advanceModel3 = obj.LoadJsonFile("advance_setting3.json");
            StaticVal.advanceModel4 = obj.LoadJsonFile("advance_setting4.json");
            StaticVal.specialModel = new SpecialSettingModel().LoadJsonFile("special_setting.json");
            StaticVal.lstRealTime = new List<CryptonDetailDataModel>().LoadJsonFile("realtimelist.json");
            StaticVal.lstBlackList = new List<CryptonDetailDataModel>().LoadJsonFile("blacklist.json");
            StaticVal.tradeList = new TradeListModel().LoadJsonFile("tradelist.json");
            StaticVal.followList = new FollowModel().LoadJsonFile("followlist.json");

            //Load ListCoin
            StaticVal.lstCoin = SeedData.GetCryptonList();
            StaticVal.lstCoinFilter = SeedData.GetCryptonListWithFilter();

            //Schedule
            StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
        }
    }
}

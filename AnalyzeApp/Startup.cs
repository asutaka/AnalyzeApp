using AnalyzeApp.Data;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using Quartz;

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
            ////subcribe
            //SeedData.SubcribeData();
            Config.LoadConfig().GetAwaiter().GetResult();

            //Load ListCoin
            StaticVal.lstCoin = SeedData.GetCryptonList();
            StaticVal.lstCoinFilter = SeedData.GetCryptonListWithFilter();

            //Schedule
            StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
        }
    }
}

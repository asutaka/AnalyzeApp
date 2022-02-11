using AnalyzeApp.Common;
using AnalyzeApp.Job;
using AnalyzeApp.Job.ScheduleJob;
using Quartz;
using System.Linq;

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
            StaticVal.lstCoin = DataMng.GetCoin();
            StaticVal.lstCoinFilter = StaticVal.lstCoin.Where(x => !Config.BlackLists.Any(y => y.S == x.S)).ToList();

            //Schedule
            StaticVal.ScheduleMngObj.AddSchedule(new ScheduleMember(StaticVal.ScheduleMngObj.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
        }
    }
}

using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnalyzeApp
{
    public class Config
    {
        public static async Task LoadConfig()
        {
            var lConfig = await APIService.Instance().GetSettings();
            foreach (var item in lConfig)
            {
                switch (item.Id)
                {
                    case (int)enumSetting.TradeList:
                        {
                            TradeList = JsonConvert.DeserializeObject<TradeListModel>(item.Setting);
                            if (TradeList == null)
                                TradeList = new TradeListModel();
                            break;
                        }
                    case (int)enumSetting.FollowList:
                        {
                            FollowSetting = JsonConvert.DeserializeObject<FollowSettingModel>(item.Setting);
                            if (FollowSetting == null)
                                FollowSetting = new FollowSettingModel();
                            break;
                        }
                    case (int)enumSetting.BlackList:
                        {
                            BlackLists = JsonConvert.DeserializeObject<List<CryptonDetailDataModel>>(item.Setting);
                            if (BlackLists == null)
                                BlackLists = new List<CryptonDetailDataModel>();
                            break;
                        }
                    case (int)enumSetting.RealtimeList:
                        {
                            RealTimes = JsonConvert.DeserializeObject<List<CryptonDetailDataModel>>(item.Setting);
                            if (RealTimes == null)
                                RealTimes = new List<CryptonDetailDataModel>();
                            break;
                        }
                    case (int)enumSetting.BasicSetting:
                        {
                            BasicSetting = JsonConvert.DeserializeObject<BasicSettingModel>(item.Setting);
                            if (BasicSetting == null)
                                BasicSetting = new BasicSettingModel();
                            break;
                        }
                    case (int)enumSetting.AdvanceSetting:
                        {
                            AdvanceSetting = JsonConvert.DeserializeObject<FollowSettingModel>(item.Setting);
                            if (AdvanceSetting == null)
                                AdvanceSetting = new FollowSettingModel();
                            break;
                        }
                }
            }
        }
        public static BasicSettingModel BasicSetting { get; set; }
        public static List<CryptonDetailDataModel> RealTimes { get; set; }
        public static List<CryptonDetailDataModel> BlackLists { get; set; }
        public static TradeListModel TradeList { get; set; }
        public static FollowSettingModel FollowSetting { get; set; }
        public static FollowSettingModel AdvanceSetting { get; set; }
    }
}


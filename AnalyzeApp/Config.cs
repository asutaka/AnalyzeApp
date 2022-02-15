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
                            break;
                        }
                    case (int)enumSetting.FollowList:
                        {
                            Follow = JsonConvert.DeserializeObject<FollowModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.BlackList:
                        {
                            BlackLists = JsonConvert.DeserializeObject<List<CryptonDetailDataModel>>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.RealtimeList:
                        {
                            RealTimes = JsonConvert.DeserializeObject<List<CryptonDetailDataModel>>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.BasicSetting:
                        {
                            BasicSetting = JsonConvert.DeserializeObject<BasicSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.SpecialSetting:
                        {
                            SpecialSetting = JsonConvert.DeserializeObject<SpecialSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.AdvanceSetting1:
                        {
                            AdvanceSetting1 = JsonConvert.DeserializeObject<AdvanceSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.AdvanceSetting2:
                        {
                            AdvanceSetting2 = JsonConvert.DeserializeObject<AdvanceSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.AdvanceSetting3:
                        {
                            AdvanceSetting3 = JsonConvert.DeserializeObject<AdvanceSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.AdvanceSetting4:
                        {
                            AdvanceSetting4 = JsonConvert.DeserializeObject<AdvanceSettingModel>(item.Setting);
                            break;
                        }
                    case (int)enumSetting.PrivateSetting:
                        {
                            PrivateSetting = JsonConvert.DeserializeObject<PrivateSettingModel>(item.Setting);
                            break;
                        }
                }
            }
        }
        public static BasicSettingModel BasicSetting { get; set; }
        public static AdvanceSettingModel AdvanceSetting1 { get; set; }
        public static AdvanceSettingModel AdvanceSetting2 { get; set; }
        public static AdvanceSettingModel AdvanceSetting3 { get; set; }
        public static AdvanceSettingModel AdvanceSetting4 { get; set; }
        public static SpecialSettingModel SpecialSetting { get; set; }
        public static List<CryptonDetailDataModel> RealTimes { get; set; }
        public static List<CryptonDetailDataModel> BlackLists { get; set; }
        public static TradeListModel TradeList { get; set; }
        public static FollowModel Follow { get; set; }
        public static FollowSettingModel FollowSetting { get; set; }
        public static PrivateSettingModel PrivateSetting { get; set; }
    }
}


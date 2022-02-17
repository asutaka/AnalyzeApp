using System.Collections.Generic;

namespace AnalyzeApp.Model.ENTITY
{
    public class FollowSettingModel
    {
        public bool IsNotify { get; set; }// bật notify hay không
        public int Interval { get; set; }// 1 lần duy nhất, mỗi ngày một lần, theo chu kỳ
        public string Cron { get; set; }
        public List<string> Coins { get; set; }
        public FollowSettingModeModel FollowSettingMode1 { get; set; }
        public FollowSettingModeModel FollowSettingMode2 { get; set; }
        public FollowSettingModeModel FollowSettingMode3 { get; set; }
        public FollowSettingModeModel FollowSettingMode4 { get; set; }
        public FollowSettingModeModel FollowSettingMode5 { get; set; }
    }
    public class FollowSettingModeModel
    {
        public string Title { get; set; }
        public List<FollowSettingModeDetailModel> lFollowSettingModeDetail { get; set; }
    }
    public class FollowSettingModeDetailModel
    {
        public int Interval { get; set; }
        public List<FollowSetting_MacdModel> lFollowSetting_Macd { get; set; }
        public List<FollowSetting_MaModel> lFollowSetting_Ma { get; set; }
        public List<FollowSetting_PriceModel> lFollowSetting_Price { get; set; }
        public List<FollowSetting_VolumeModel> lFollowSetting_Volume { get; set; }
        public List<FollowSetting_McdxModel> lFollowSetting_Mcdx { get; set; }
        public List<FollowSetting_RsiModel> lFollowSetting_Rsi { get; set; }
        public List<FollowSetting_AdxModel> lFollowSetting_Adx { get; set; }
    }
    public class FollowSetting_MacdModel
    {
        public int Option { get; set; }
        public decimal RatioMax { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_MaModel
    {
        public int Mode { get; set; }
        public int Option { get; set; }
        public decimal RatioMax { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_PriceModel
    {
        public int Mode { get; set; }
        public int Option { get; set; }
        public decimal RatioMax { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_VolumeModel
    {
        public int Mode { get; set; }
        public int Option { get; set; }
        public decimal RatioMax { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_Volume2Model
    {
        public int Mode { get; set; }
        public int Option { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_McdxModel
    {
        public int Option { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_RsiModel
    {
        public int Option { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
    public class FollowSetting_AdxModel
    {
        public int Option { get; set; }
        public decimal Value { get; set; }
        public decimal Point { get; set; }
    }
}

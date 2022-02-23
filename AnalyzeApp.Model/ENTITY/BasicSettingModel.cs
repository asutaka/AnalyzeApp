namespace AnalyzeApp.Model.ENTITY
{
    public class BasicSettingModel
    {
        //basic
        public int TimeSet_Value { get; set; }
        public int CandleStick_Value { get; set; }
        public int Volume_Value { get; set; }
        public int MA_Value { get; set; }
        public int EMA_Value { get; set; }
        public int RSI_Value { get; set; }
        public int ADX_Value { get; set; }
        public int MCDX_Value { get; set; }
        public MACD_BasicSettingModel MACD_Value { get; set; }

        //advance
        public int TimeCalculate { get; set; }
        public int RealtimeInterval { get; set; }
        public bool PriceAction { get; set; }
    }
}

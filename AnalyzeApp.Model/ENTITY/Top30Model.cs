namespace AnalyzeApp.Model.ENTITY
{
    public class Top30Model
    {
        public int STT { get; set; }
        public string Coin { get; set; }
        public string CoinName { get; set; }
        public int Count { get; set; }
        public double Rate { get; set; }
        public double RefValue { get; set; }
        public double Value { get; set; }
        public double BottomRecent { get; set; }
        public double RateValue { get; set; }
        public double WaveRecent { get; set; }
        public int CountTime { get; set; }
        public decimal PrevDayClosePrice { get; set; }
        public decimal PriceChangePercent { get; set; }
        public decimal WeightedAveragePrice { get; set; }
    }
}

namespace AnalyzeApp.Model.ENTITY
{
    public class BinanceKline
    {
        public long OpenTime { get; set; }
        public long CloseTime { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal BaseVolume { get; set; }
        public decimal QuoteVolume { get; set; }
        public decimal TakerBuyBaseVolume { get; set; }
        public decimal TakerBuyQuoteVolume { get; set; }
    }
}

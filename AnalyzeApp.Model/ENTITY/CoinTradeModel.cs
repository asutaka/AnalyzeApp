namespace AnalyzeApp.Model.ENTITY
{
    public class CoinTradeModel
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double QuoteQty { get; set; }
        public long Time { get; set; }
        public bool IsBuyerMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}

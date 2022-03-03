namespace AnalyzeApp.Model.ENTITY
{
    public class SendNotifyModel
    {
        public string Coin { get; set; }
        public decimal Value { get; set; }
        public long SendTime { get; set; }
        public bool IsAbove { get; set; }
    }
}

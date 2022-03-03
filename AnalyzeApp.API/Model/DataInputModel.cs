namespace AnalyzeApp.API.Model
{
    public class DataInputModel
    {
        public int Interval { get; set; }
        public IEnumerable<string> Coins { get; set; }
    }
}

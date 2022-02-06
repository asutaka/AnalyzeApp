using System.Text.Json.Serialization;

namespace AnalyzeApp.API.Model
{
    public class DataModel
    {
        [JsonIgnore]
        public string Coin { get; set; }
        public double T { get; set; }
        public double O { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public double V { get; set; }
        public int ValType { get; set; }
    }
}

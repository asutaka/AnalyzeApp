using Newtonsoft.Json;

namespace AnalyzeApp.Model.ENTITY
{
    public class TimeModel
    {
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }
    }
}

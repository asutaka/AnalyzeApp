using System.Text.Json.Serialization;

namespace AnalyzeApp.API.Model
{
    public class NotifyModel
    {
        [JsonIgnore]
        public double TimeCreate { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public bool IsService { get; set; }
    }
}

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AnalyzeApp.API.Model
{
    public class NotifyModel
    {
        [JsonIgnore]
        public double TimeCreate { get; set; }
        [DefaultValue("")]
        public string Phone { get; set; }
        [DefaultValue("")]
        public string Content { get; set; }
        [DefaultValue(false)]
        public bool IsService { get; set; }
    }
}

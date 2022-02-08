using System.ComponentModel;

namespace AnalyzeApp.API.Model
{
    public class SettingModel
    {
        public int Id { get; set; }
        [DefaultValue("")]
        public string Setting { get; set; }
    }
}

using System.ComponentModel;

namespace AnalyzeApp.Model.ENTITY
{
    public class SettingModel
    {
        public int Id { get; set; }
        [DefaultValue("")]
        public string Setting { get; set; }
    }
}

using System.Collections.Generic;

namespace AnalyzeApp.Model.ENTITY
{
    public class BasicSettingModel
    {
        public int TimeZone { get; set; }
        public int Interval { get; set; }
        public List<GeneralModel> ListModel { get; set; }
    }
}

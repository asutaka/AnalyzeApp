using System.Collections.Generic;

namespace AnalyzeApp.Model.ENTITY
{
    public class DataInputModel
    {
        public int Interval { get; set; }
        public IEnumerable<string> Coins { get; set; }
    }
}

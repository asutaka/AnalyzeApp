using System.ComponentModel;

namespace AnalyzeApp.API.Model
{
    public class UserModel
    {
        [DefaultValue("")]
        public string Phone { get; set; }
        [DefaultValue("")]
        public string Code { get; set; }
    }
}

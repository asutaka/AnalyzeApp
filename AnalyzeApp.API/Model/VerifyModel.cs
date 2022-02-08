using System.ComponentModel;

namespace AnalyzeApp.API.Model
{
    public class VerifyModel
    {
        [DefaultValue("")]
        public string VerifyCode { get; set; }
        public bool IsService { get; set; }
    }
}

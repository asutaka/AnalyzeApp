using System.ComponentModel;

namespace AnalyzeApp.API.Model
{
    public class ProfileModel
    {
        [DefaultValue("")]
        public string Phone { get; set; }
        [DefaultValue("")]
        public string Code { get; set; }
        [DefaultValue("")]
        public string Email { get; set; }
        [DefaultValue("")]
        public string LinkAvatar { get; set; }
        public bool IsNotify { get; set; }
    }
}

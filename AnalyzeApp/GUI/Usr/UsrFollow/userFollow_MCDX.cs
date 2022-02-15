using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MCDX : UserControl
    {
        public userFollow_MCDX()
        {
            InitializeComponent();
        }
        public FollowSetting_McdxModel GetData()
        {
            return new FollowSetting_McdxModel
            {
                IsValid = chkValid.IsOn
            };
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

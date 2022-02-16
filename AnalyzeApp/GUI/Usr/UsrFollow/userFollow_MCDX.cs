using AnalyzeApp.Model.ENTITY;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollow_MCDX : UserControl
    {
        public userFollow_MCDX(FollowSetting_McdxModel model)
        {
            InitializeComponent();
            InitData(model);
        }

        private void InitData(FollowSetting_McdxModel model)
        {
            if (model == null)
                model = new FollowSetting_McdxModel();
            chkValid.IsOn = model.IsValid;
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

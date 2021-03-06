using AnalyzeApp.Common;
using System.Linq;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userCoinTrace : UserControl
    {
        private readonly string _code = string.Empty;
        public userCoinTrace()
        {
            InitializeComponent();
        }
        public userCoinTrace(string code)
        {
            InitializeComponent();
            _code = code;
            SetData();
        }

        public string GetCoin()
        {
            return _code;
        }

        private void SetData()
        {
            var entity = StaticVal.lstCoin.FirstOrDefault(x => x.S == _code);
            if(entity != null)
            {
                txtCoin.Text = entity.S;
                txtDescription.Text = entity.AN;
            }
        }

        private void lblClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
    }
}

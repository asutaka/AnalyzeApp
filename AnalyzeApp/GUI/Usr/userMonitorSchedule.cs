using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userMonitorSchedule : XtraUserControl
    {
        public userMonitorSchedule()
        {
            InitializeComponent();
        }
        public RichTextBox GetRichTextBox()
        {
            return txtLog;
        }
        public CheckBox GetCheckBox()
        {
            return chkBox;
        }
    }
}

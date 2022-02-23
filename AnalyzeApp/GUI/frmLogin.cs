using AnalyzeApp.Common;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AnalyzeApp.GUI
{
    public partial class frmLogin : XtraForm
    {
        public bool IsSuccess = false;
        private frmLogin()
        {
            InitializeComponent();
            var bkgr = new BackgroundWorker();
            bkgr.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                Config.LoadConfig().GetAwaiter().GetResult();
                //Load ListCoin
                StaticVal.lstCoin = DataMng.GetCoin();
                StaticVal.lstCoinFilter = StaticVal.lstCoin.Where(x => !Config.BlackLists.Any(y => y.S == x.S)).ToList();
                IsSuccess = true;
                bkgr.Dispose();
            };
            bkgr.RunWorkerAsync();
        }

        private static frmLogin _instance = null;
        public static frmLogin Instance()
        {
            _instance = _instance ?? new frmLogin();
            return _instance;
        }

        public static void DisposeInstance()
        {
            _instance = null;
        }

        private void picGoogleSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                frmProfile.Instance().Show();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"UserLogin:GoogleSignIn: {ex.Message}");
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*kill all running process
            * https://stackoverflow.com/questions/8507978/exiting-a-c-sharp-winforms-application
            */
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
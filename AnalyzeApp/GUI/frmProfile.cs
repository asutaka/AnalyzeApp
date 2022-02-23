using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AnalyzeApp.GUI
{
    public partial class frmProfile : XtraForm
    {
        private ProfileModel _profile = null;
        private WaitFunc _frmWaitForm = new WaitFunc();
        private frmProfile()
        {
            InitializeComponent();
            Init();
        }

        private static frmProfile _instance = null;
        public static frmProfile Instance()
        {
            _instance = _instance ?? new frmProfile();
            return _instance;
        }

        private void Init()
        {
            BackgroundWorker wrkr = new BackgroundWorker();
            wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                _profile = APIService.Instance().GetProfile().GetAwaiter().GetResult();
                this.Invoke((MethodInvoker)delegate
                {
                    this.Enabled = true;
                    if (_profile != null)
                    {
                        StaticVal.profile = _profile;
                        txtPhone.Text = _profile.Phone;
                        txtEmail.Text = _profile.Email;
                        chkNotify.Checked = _profile.IsNotify;
                        if (!string.IsNullOrWhiteSpace(_profile.LinkAvatar))
                        {
                            try
                            {
                                picAvatar.Load(_profile.LinkAvatar);
                            }
                            catch (Exception ex)
                            {
                                NLogLogger.PublishException(ex, $"frmProfile|Init: {ex.Message}");
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(_profile.Code))
                        {
                            txtCode.Text = _profile.Code;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(txtEmail.Text))
                        txtEmail.Focus();
                    else 
                        btnOk.Focus();
                });
                wrkr.Dispose();
            };
            wrkr.RunWorkerAsync();
        }

        private bool UpdateUserModel()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                return false;
            if (txtEmail.Text.Trim() == _profile.Email
                && txtCode.Text.Trim() == _profile.Code
                && txtPhone.Text.Trim().PhoneFormat() == _profile.Phone
                && (string.IsNullOrWhiteSpace(picAvatar.ImageLocation)
                || picAvatar.ImageLocation == _profile.LinkAvatar))
                return false;

            _profile.Phone = txtPhone.Text.Trim().PhoneFormat();
            _profile.Code = txtCode.Text.Trim();
            _profile.Email = txtEmail.Text;
            _profile.IsNotify = chkNotify.Checked;
            _profile.LinkAvatar = string.IsNullOrWhiteSpace(picAvatar.ImageLocation) ? string.Empty : picAvatar.ImageLocation;

            BackgroundWorker wrkr = new BackgroundWorker();
            wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                APIService.Instance().DeleteProfile().GetAwaiter().GetResult();
                APIService.Instance().InsertProfile(_profile).GetAwaiter().GetResult();
                wrkr.Dispose();
            };
            wrkr.RunWorkerAsync();
            StaticVal.profile = _profile;
            return true;
        }

        private void CloseAppCheck()
        {
            if (!StaticVal.IsAccessMain)
            {
                /*kill all running process
                * https://stackoverflow.com/questions/8507978/exiting-a-c-sharp-winforms-application
                */
                Process.GetCurrentProcess().Kill();
                Application.Exit();
                Environment.Exit(0);
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    _instance = null;
                    this.Close();
                });
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CloseAppCheck();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            btnOk.Focus();
            btnPaste.Enabled = false;
            if(string.IsNullOrWhiteSpace(Clipboard.GetText().Trim())
                || Clipboard.GetText().Trim().Equals(txtCode.Text.Trim()))
            {
                btnPaste.Enabled = true;
            }
            else
            {
                txtCode.Text = Clipboard.GetText().Trim();
            }
        }

        private void txtCode_EditValueChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                return;
            }
            if (StaticVal.IsExecCheckCodeActive)
                return;
            btnPaste.Enabled = false;
            StaticVal.IsExecCheckCodeActive = true;
            picStatus.Image = Properties.Resources.yellow;

            BackgroundWorker wrkr = new BackgroundWorker();
            wrkr.DoWork += (object sender1, DoWorkEventArgs e1) => {
                _profile = APIService.Instance().GetProfile().GetAwaiter().GetResult();
                this.Invoke((MethodInvoker)delegate
                {
                    _frmWaitForm.Show("Kiểm tra trạng thái");
                    //var time = CommonMethod.GetTimeAsync().GetAwaiter().GetResult();
                    var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    var jsonModel = Security.Decrypt(txtCode.Text.Trim());
                    if (string.IsNullOrWhiteSpace(jsonModel))
                    {
                        StaticVal.IsCodeActive = false;
                    }
                    else
                    {
                        var model = JsonConvert.DeserializeObject<GenCodeModel>(jsonModel);
                        if (!txtEmail.Text.Trim().Contains(model.Email) || model.Expired <= time)
                        {
                            StaticVal.IsCodeActive = false;
                        }
                        else
                        {
                            StaticVal.IsCodeActive = true;
                            StaticVal.Level = model.Level;
                        }
                    }
                    //
                    picStatus.Image = StaticVal.IsCodeActive ? Properties.Resources.green : Properties.Resources.red;
                    btnPaste.Enabled = true;
                    if (!StaticVal.IsCodeActive)
                    {
                        txtCode.Text = string.Empty;
                    }
                    else
                    {
                        UpdateUserModel();
                        if (!StaticVal.IsAccessMain)
                        {
                            while (frmLogin.Instance().IsSuccess)
                            {
                                frmLogin.DisposeInstance();
                                Hide();
                                frmMain.Instance().Show();
                            }
                        }
                    }
                    StaticVal.IsExecCheckCodeActive = false;

                    Thread.Sleep(200);
                    _frmWaitForm.Close();
                });
                wrkr.Dispose();
            };
            wrkr.RunWorkerAsync();
        }

        private void frmProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAppCheck();
        }

        private void picSupport_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picSupport, "Liên hệ lấy code");
        }

        private void picAvatar_DoubleClick(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(openFileDialog1.FileName))
                {
                    try
                    {
                        var extend = openFileDialog1.SafeFileName.Split('.').Last();
                        var fileName = $"{Guid.NewGuid().ToString().Replace('-', '_')}.{extend}";
                        var path = $"{Directory.GetCurrentDirectory()}//avatar";
                        var newPathFile = $"{path}//{fileName}";
                        foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                        {
                            if (!string.IsNullOrWhiteSpace(_profile.LinkAvatar)
                                && _profile.LinkAvatar.Equals(file.Name))
                                continue;
                            file.Delete();
                        }
                        File.Copy(openFileDialog1.FileName, newPathFile);
                        picAvatar.Load(newPathFile);
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"frmProfile|picAvatar_DoubleClick: {ex.Message}");
                    }
                }
            }
        }

        private void picSupport_Click(object sender, EventArgs e)
        {
            if(_profile == null)
            {
                MessageBox.Show("Chưa có thông tin người nhận");
                return;
            }
            if (string.IsNullOrWhiteSpace(_profile.Phone)
                && string.IsNullOrWhiteSpace(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Chưa có thông tin SĐT liên hệ");
                return;
            }
            picSupport.Enabled = false;
            APIService.Instance().SendMessage(new NotifyModel { Content = ConstVal.strService, IsService = true });
            Thread.Sleep(1000);
            picSupport.Enabled = true;
        }
    }
}
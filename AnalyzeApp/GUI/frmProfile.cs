using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AnalyzeApp.GUI
{
    public partial class frmProfile : XtraForm
    {
        private ProfileModel _profile = StaticVal.profile;
        private BackgroundWorker _bkgr = new BackgroundWorker();
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
            wrkr.WorkerReportsProgress = true;

            wrkr.DoWork += (object sender, DoWorkEventArgs e) => {
                var objUser = APIService.Instance().GetUser().Result;
                this.Invoke((MethodInvoker)delegate
                {
                    if (objUser != null)
                    {
                        StaticVal.profile.Phone = objUser.Phone;
                        txtPhone.Text = objUser.Phone;
                        if (!string.IsNullOrWhiteSpace(objUser.Code))
                        {
                            StaticVal.profile.Code = objUser.Code;
                            txtCode.Text = objUser.Code;
                        }
                    }

                    lblUserName.Text = _profile.Name;
                    lblEmail.Text = _profile.Email;
                    lblLocale.Text = _profile.Locale;
                    if (!string.IsNullOrWhiteSpace(_profile.Picture))
                        picAvatar.Load(_profile.Picture);
                    btnOk.Focus();
                });
                _bkgr.DoWork += bkgrCheckStatus_DoWork;
                _bkgr.RunWorkerCompleted += bkgrCheckStatus_RunWorkerCompleted;
                wrkr.Dispose();
            };
            wrkr.RunWorkerAsync();
        }

        private void StateEdit(bool isEdit)
        {
            btnEdit.Visible = !isEdit;
            btnSave.Visible = isEdit;
            btnCancel.Visible = isEdit;
            txtPhone.Properties.ReadOnly = !isEdit;
            if(isEdit)
                txtPhone.Focus();
        }

        private bool UpdateUserModel()
        {
            var model = new UserModel
            {
                Phone = txtPhone.Text.Trim().PhoneFormat(),
                Code = txtCode.Text.Trim()
            };

            APIService.Instance().DeleteUser();
            APIService.Instance().InsertUser(model);
            StaticVal.profile.Phone = model.Phone;
            StaticVal.profile.Code = model.Code;
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

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            //PHUNV
            Hide();
            frmMain.Instance().Show();
            //PHUNV
            //CloseAppCheck();
        }

        private void btnPaste_Click(object sender, System.EventArgs e)
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

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            StateEdit(true);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            StateEdit(false);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            StateEdit(false);
            if (!UpdateUserModel())
            {
                MessageBox.Show("Cập nhật không thành công!");
            }
            txtPhone.Text = StaticVal.profile.Phone;
        }

        private void txtCode_EditValueChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                return;
            }
            if (StaticVal.IsExecCheckCodeActive)
                return;
            StaticVal.IsExecCheckCodeActive = true;
            picStatus.Image = Properties.Resources.yellow;
            _bkgr.RunWorkerAsync();
        }

        private void bkgrCheckStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            _frmWaitForm.Show("Kiểm tra trạng thái");
            //btnPaste.Enabled = false;
           
            var time = CommonMethod.GetTimeAsync().GetAwaiter().GetResult();
            var jsonModel = Security.Decrypt(txtCode.Text.Trim());
            if (string.IsNullOrWhiteSpace(jsonModel))
            {
                StaticVal.IsCodeActive = false;
            }
            else
            {
                var model = JsonConvert.DeserializeObject<GenCodeModel>(jsonModel);
                if(!_profile.Email.Contains(model.Email) || model.Expired <= time)
                {
                    StaticVal.IsCodeActive = false;
                }
                else
                {
                    StaticVal.IsCodeActive = true;
                    StaticVal.Level = model.Level;
                }
            }
            Thread.Sleep(200);
            _frmWaitForm.Close();
        }

        private void bkgrCheckStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            picStatus.Image = StaticVal.IsCodeActive ? Properties.Resources.green : Properties.Resources.red;
            //btnPaste.Enabled = true;

            if (!StaticVal.IsCodeActive)
            {
                txtCode.Text = string.Empty;
            }
            else
            {
                UpdateUserModel();
                if (!StaticVal.IsAccessMain)
                {
                    Hide();
                    frmMain.Instance().Show();
                }
            }
            StaticVal.IsExecCheckCodeActive = false;
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
    }
}
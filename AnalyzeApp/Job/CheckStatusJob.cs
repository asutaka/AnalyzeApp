using AnalyzeApp.Common;
using AnalyzeApp.GUI;
using AnalyzeApp.Model.ENTITY;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Windows.Forms;

namespace AnalyzeApp.Job
{
    public class CheckStatusJob : IJob
    {
        private const string _fileName = "user.json";
        public void Execute(IJobExecutionContext context)
        {
            if (StaticValtmp.IsExecCheckCodeActive)
                return;
            StaticValtmp.IsExecCheckCodeActive = true;
            var time = CommonMethod.GetTimeAsync().GetAwaiter().GetResult();

            var objUser = new UserModel().LoadJsonFile(_fileName);
            var jsonModel = Security.Decrypt(objUser.Code);
            if (string.IsNullOrWhiteSpace(jsonModel))
            {
                StaticValtmp.IsCodeActive = false;
            }
            else
            {
                var model = JsonConvert.DeserializeObject<GenCodeModel>(jsonModel);
                if (!StaticValtmp.profile.Email.Contains(model.Email) || model.Expired <= time)
                {
                    StaticValtmp.IsCodeActive = false;
                }
                else
                {
                    StaticValtmp.IsCodeActive = true;
                }
            }

            if (!StaticValtmp.IsCodeActive)
            {
                StaticValtmp.IsAccessMain = false;
                try
                {
                    if (StaticVal.scheduleMng != null)
                    {
                        foreach (var item in StaticVal.scheduleMng.GetSchedules())
                        {
                            item.Pause();
                        }
                    }
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex, $"CheckStatusJob: {ex.Message}");
                }

                //về màn hình đăng nhập
                StaticValtmp.frmMainObj.BeginInvoke((MethodInvoker)delegate
                {
                    StaticValtmp.frmMainObj.Hide();
                    frmLogin.Instance().Show();
                });
            }
            StaticValtmp.IsExecCheckCodeActive = false;
        }
    }
}

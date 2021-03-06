using AnalyzeApp.Common;
using AnalyzeApp.GUI;
using AnalyzeApp.Model.ENTITY;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Windows.Forms;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class CheckStatusJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (StaticVal.IsExecCheckCodeActive)
                return;
            StaticVal.IsExecCheckCodeActive = true;
            //var time = CommonMethod.GetTimeAsync().GetAwaiter().GetResult();
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var profile = APIService.Instance().GetProfile().GetAwaiter().GetResult();
            if(profile == null)
            {
                StaticVal.IsCodeActive = false;
            }
            else
            {
                var jsonModel = Security.Decrypt(profile.Code);
                if (string.IsNullOrWhiteSpace(jsonModel))
                {
                    StaticVal.IsCodeActive = false;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<GenCodeModel>(jsonModel);
                    if (!StaticVal.profile.Email.Contains(model.Email) || model.Expired <= time)
                    {
                        StaticVal.IsCodeActive = false;
                    }
                    else
                    {
                        StaticVal.IsCodeActive = true;
                    }
                }
            }

            if (!StaticVal.IsCodeActive)
            {
                StaticVal.IsAccessMain = false;
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
                frmMain.Instance().BeginInvoke((MethodInvoker)delegate
                {
                    frmMain.Instance().Hide();
                    frmLogin.Instance().Show();
                });
            }
            StaticVal.IsExecCheckCodeActive = false;
        }
    }
}

using AnalyzeApp.API.Interface;
using Quartz;

namespace AnalyzeApp.API.Job
{
    [DisallowConcurrentExecution]
    public class ScheduleJobTelegram : IJob
    {
        private readonly IAnalyzeRepo _repo;
        public ScheduleJobTelegram(IAnalyzeRepo repo)
        {
            _repo = repo;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var isSend = false;
            var lData = await _repo.GetNotify(5);
            if (lData != null && lData.Any())
            {
                foreach (var item in lData)
                {
                    if (!(StaticVal.lNotify.Any(x => x.Phone.Equals(item.Phone) && x.Content.Equals(item.Content))))
                    {
                        isSend = true;
                        StaticVal.lNotify.Add(item);
                    }
                    else if (StaticVal.lNotify.Any(x => x.Phone.Equals(item.Phone) && x.Content.Equals(item.Content) && Math.Abs(item.TimeCreate - x.TimeCreate) < 60))
                    {
                        isSend = true;
                        StaticVal.lNotify.Add(item);
                    }
                    else
                    {
                        isSend = false;
                    }
                    if (isSend)
                    {
                        var result = await TeleClient.SendMessage(item.Phone, item.Content, item.IsService);
                    }

                    await _repo.DeleteNotify(item.TimeCreate);
                    Thread.Sleep(2000);
                }
            }
            if (StaticVal.lNotify.Count > 100)
                StaticVal.lNotify.RemoveRange(0, 20);
        }
    }
}

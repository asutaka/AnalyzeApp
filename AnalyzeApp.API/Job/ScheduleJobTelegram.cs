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
            var lData = await _repo.GetNotify(5);
            if(lData != null && lData.Any())
            {
                foreach (var item in lData)
                {
                    var result = await TeleClient.SendMessage(item.Phone, item.Content, item.IsService);
                    if ((enumTelegramSendMessage)result == enumTelegramSendMessage.Success)
                        await _repo.DeleteNotify(item.TimeCreate);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

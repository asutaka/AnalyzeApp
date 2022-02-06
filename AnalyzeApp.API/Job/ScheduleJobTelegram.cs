using AnalyzeApp.API.Interface;
using Quartz;

namespace AnalyzeApp.API.Job
{
    [DisallowConcurrentExecution]
    public class ScheduleJobTelegram : IJob
    {
        private readonly IAnalyzeService _service;
        public ScheduleJobTelegram(IAnalyzeService service)
        {
            _service = service;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var lData = await _service.GetNotify(5);
            if(lData != null && lData.Any())
            {
                foreach (var item in lData)
                {
                    var result = TeleClient.SendMessage(item.Phone, item.Content, item.IsService);
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("deo hieu kieu gi");
        }
    }
}

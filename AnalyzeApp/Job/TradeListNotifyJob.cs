using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeApp.Job
{
    [DisallowConcurrentExecution]
    public class TradeListNotifyJob : IJob
    {
        private readonly ProfileModel _profile = StaticVal.profile;
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var model = Config.TradeList;
                if (!_profile.IsNotify || !model.IsNotify || StaticVal.IsTradeListChange)
                    return;

                var lstTask = new List<Task>();
                var lstNotify = new List<string>();
                foreach (var item in model.lData)
                {
                    var task = Task.Run(() =>
                    {
                        var currentVal = (double)DataMng.GetCurrentVal(item.Coin);
                        var entityAbove = item.Config.Where(x => x.IsAbove && currentVal > (double)x.Value).OrderByDescending(x => x.Value).FirstOrDefault();
                        if (entityAbove != null)
                        {
                            var sendTime = new DateTimeOffset().ToUnixTimeSeconds();
                            var entityNotiTrade = StaticVal.lstNotiTrade.FirstOrDefault(x => x.Coin == item.Coin && x.Value == entityAbove.Value && x.IsAbove && (sendTime - x.SendTime < 120));
                            if(entityNotiTrade == null)
                            {
                                StaticVal.lstNotiTrade.Add(new SendNotifyModel { 
                                    Coin = item.Coin,
                                    SendTime = sendTime,
                                    IsAbove = true,
                                    Value = entityAbove.Value
                                });
                                APIService.Instance().SendMessage(new NotifyModel { Content = $"Giá {item.Coin} vượt lên trên {entityAbove.Value}", IsService = false });
                            }
                        }

                        var entityBelow = item.Config.Where(x => !x.IsAbove && currentVal < (double)x.Value).OrderBy(x => x.Value).FirstOrDefault();
                        if (entityBelow != null)
                        {
                            var sendTime = new DateTimeOffset().ToUnixTimeSeconds();
                            var entityNotiTrade = StaticVal.lstNotiTrade.FirstOrDefault(x => x.Coin == item.Coin && x.Value == entityBelow.Value && !x.IsAbove && (sendTime - x.SendTime < 120));
                            if (entityNotiTrade == null)
                            {
                                StaticVal.lstNotiTrade.Add(new SendNotifyModel
                                {
                                    Coin = item.Coin,
                                    SendTime = sendTime,
                                    IsAbove = false,
                                    Value = entityBelow.Value
                                });
                                APIService.Instance().SendMessage(new NotifyModel { Content = $"Giá {item.Coin} giảm xuống dưới {entityBelow.Value}", IsService = false });
                            }
                        }
                    });
                    lstTask.Add(task);
                }
                Task.WaitAll(lstTask.ToArray());
                if(StaticVal.lstNotiTrade.Count() > 500)
                {
                    StaticVal.lstNotiTrade.RemoveRange(0, 100);
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"TradeListJob:Execute: {ex.Message}");
            }
        }
    }
}

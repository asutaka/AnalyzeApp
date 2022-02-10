using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyzeApp.Common
{
    public class DataMng
    {
        public static List<CryptonDetailDataModel> GetCoin()
        {
            var cryptonModel = CommonMethod.DownloadJsonFile<CryptonDataModel>(ConstVal.COIN_LIST);
            var output = cryptonModel.Data.Where(x => x.S.Substring(x.S.Length - 4) == "USDT").OrderBy(x => x.S).ToList();
            return output;
        }
        public static JArray GetSource(string coin, enumInterval interval, long startTime = 0, long endTime = 0)
        {
            try
            {
                string strTime = string.Empty;
                if (startTime > 0)
                    strTime += $"&startTime={startTime}";
                if (endTime > 0)  
                    strTime += $"&endTime={endTime}";
                var url = $"{ConstVal.COIN_DETAIL}symbol={coin}&interval={interval.GetDisplayName()}{strTime}";
                var arrData = CommonMethod.DownloadJsonArray(url);
                return arrData;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"DataMng|GetSource:{ex.Message}");
                return null;
            }
        }
    }
}
//15m: 10 ngày
//1h: 1 tháng
//4h: 3 tháng
//1d: 2 năm
//1w: 10 năm
//1m: no limit

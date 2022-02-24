using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnalyzeApp.Common
{
    public static class CommonMethod
    {
        public static bool CheckForInternetConnection(int timeoutMs = 10000)
        {
            try
            {
                var url = "https://www.google.com.vn/";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch(Exception e)
            {
                NLogLogger.PublishException(e, $"CommonMethod|CheckForInternetConnection: { e.Message }");
                return false;
            }
        }

        public static bool CheckFileExist(string fileName, string folderName)
        {
            var folder = string.IsNullOrWhiteSpace(folderName) ? string.Empty : $"\\{folderName}";
            string path = $"{Directory.GetCurrentDirectory()}{folder}\\{fileName}";
            return File.Exists(path);
        }

        public static async Task<long> GetTimeAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    var response = await client.GetAsync(ConstVal.timeURL);
                    var result = response.Content.ReadAsStringAsync().Result;
                    var resultModel = JsonConvert.DeserializeObject<TimeModel>(result);
                    return resultModel.TimeStamp;
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CommonMethod|GetTimeAsync: { ex.Message }");
                return 0;
            }
        }

        public static T DownloadJsonFile<T>(string url)
        {
            if (WebRequest.Create(url) is HttpWebRequest webRequest)
            {
                webRequest.ContentType = "application/json";
                webRequest.UserAgent = "Nothing";

                using (var s = webRequest.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        var contributorsAsJson = sr.ReadToEnd();
                        var contributors = JsonConvert.DeserializeObject<T>(contributorsAsJson);
                        return (T)Convert.ChangeType(contributors, typeof(T));
                    }
                }
            }
            return (T)Convert.ChangeType(null, typeof(T));
        }

        public static JArray DownloadJsonArray(string url)
        {
            if (WebRequest.Create(url) is HttpWebRequest webRequest)
            {
                webRequest.ContentType = "application/json";
                webRequest.UserAgent = "Nothing";
                try
                {
                    using (var s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (var sr = new StreamReader(s))
                        {
                            var contributorsAsJson = sr.ReadToEnd();
                            var contributors = JArray.Parse(contributorsAsJson);
                            return contributors;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static int GetFlag(BasicSettingModel model)
        {
            var div = 1;
            if (model.TimeCalculate == (int)enumTimeZone.OneHour)
            {
                div = div * 4;
            }
            else if (model.TimeCalculate == (int)enumTimeZone.FourHour)
            {
                div = div * 4 * 4;
            }
            else if (model.TimeCalculate == (int)enumTimeZone.OneDay)
            {
                div = div * 4 * 4 * 6;
            }
            else if (model.TimeCalculate == (int)enumTimeZone.OneWeek)
            {
                div = div * 4 * 4 * 6 * 7;
            }
            else if (model.TimeCalculate == (int)enumTimeZone.OneMonth)
            {
                div = div * 4 * 4 * 6 * 7 * 4;
            }
            var result = 1800 * model.RealtimeInterval * div;
            return result;
        }
    }
}

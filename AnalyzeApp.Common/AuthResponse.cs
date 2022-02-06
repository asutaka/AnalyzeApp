using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AnalyzeApp.Common
{
    public class AuthResponse
    {
        private string access_token;
        public string Access_token
        {
            get
            {
                // Access token lasts an hour if its expired we get a new one.
                if (DateTime.Now.Subtract(created).Hours > 1)
                {
                    refresh();
                }
                return access_token;
            }
            set { access_token = value; }
        }
        public string refresh_token { get; set; }
        public string clientId { get; set; }
        public string secret { get; set; }
        public string expires_in { get; set; }
        public DateTime created { get; set; }


        public static AuthResponse get(string response)
        {
            AuthResponse result = JsonConvert.DeserializeObject<AuthResponse>(response);
            result.created = DateTime.Now;   // DateTime.Now.Add(new TimeSpan(-2, 0, 0)); //For testing force refresh.
            return result;
        }

        public void refresh()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                var postData = $"client_id={clientId}&client_secret={secret}&refresh_token={refresh_token}&grant_type=refresh_token";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var refreshResponse = get(responseString);
                this.access_token = refreshResponse.access_token;
                this.created = DateTime.Now;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AuthResponse|Refresh: {ex.Message}");
            }
        }

        public static AuthResponse Exchange(string authCode, string clientid, string secret, string redirectURI)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                var postData = $"code={authCode}&client_id={clientid}&client_secret={secret}&redirect_uri={redirectURI}&grant_type=authorization_code";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var x = get(responseString);

                x.clientId = clientid;
                x.secret = secret;

                return x;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AuthResponse|Exchange: {ex.Message}");
            }

            return null;
        }

        public static Uri GetAuthenticationURI(string clientId, string redirectUri)
        {
            try
            {
                if (string.IsNullOrEmpty(redirectUri))
                {
                    redirectUri = ConstVal.redirectURI;
                }
                string oauth = string.Format("https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code", clientId, redirectUri, ConstVal.scopes);
                return new Uri(oauth);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"AuthResponse|GetAutenticationURI: {ex.Message}");
            }
            return null;
        }
    }
}

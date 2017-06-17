using DnD.Companion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace DnD.Companion.Helper
{
    public class ApiHandler
    {
        private static ApiHandler _apiHandler { get; set; }
        public static ApiHandler Instance
        {
            get
            {
                if (_apiHandler == null)
                    _apiHandler = new ApiHandler();
                return _apiHandler;
            }
        }

        private string AccessToken { get; set; }

        public void Login(string username, string password)
        {
            try
            {                   
                User user = new User { Username = username, Password = password };
                string json = JsonConvert.SerializeObject(user);
                byte[] data = Encoding.UTF8.GetBytes(json);
                Uri uri = new Uri("http://10.0.2.2:5000/api/account/sign-in");
                var request = WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";
                //var request = new HttpWebRequest(new Uri("http://10.0.2.2:5000/api/account/sign-in")) { ContentType = @"application/json", Method = "POST" };
                //request.KeepAlive = true;
                //var request = new HttpWebRequest(new Uri("http://192.168.0.103:5000/api/account")) { ContentType = @"application/json", Method = "GET" };
                request.ContentLength = data.Length;
                using (var sw = request.GetRequestStream())
                {
                    sw.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    if ((int)response.StatusCode == 200)
                    {
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        var returnObject = JsonConvert.DeserializeObject<Token>(responseString);
                        if (returnObject != null)
                        {
                            this.AccessToken = returnObject.access_token;
                        }
                    }
                    else
                    {
                        throw new WebException("Could not login", WebExceptionStatus.UnknownError);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}

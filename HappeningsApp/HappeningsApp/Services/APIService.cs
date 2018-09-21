using HappeningsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HappeningsApp.Services
{
    public class APIService
    {
       // HttpClient client;
        public APIService()
        {
            //client = new HttpClient();
        }
        public async static Task<string> Post<T>(T model,string method)
        {
            //HttpResponseMessage message = new HttpResponseMessage();
            string json = string.Empty;
            string url = $"{Constants.CrawlAPI}{method}";
            using (var client = new HttpClient())
            {
               // var client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //client.BaseAddress = new Uri(Constants.CrawlAPI);
                
                json = JsonConvert.SerializeObject(model);
               var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
              var  message = await client.PostAsync(url, stringContent);
              var resultt = await message.Content.ReadAsStringAsync();

                return resultt;
          //  var resultt = await message.Content.ReadAsStringAsync();

            //if (message.StatusCode== HttpStatusCode.OK)
            //{
            //    var result = await message.Content.ReadAsStringAsync();
            //    var deserilize = JsonConvert.DeserializeObject<T>(result);
            //}
            //else
            //{
            //    var result = await message.Content.ReadAsStringAsync();
            //    var deserilize = JsonConvert.DeserializeObject<T>(result);
            //}

            //    return message;
            }

            
        }

        internal async static Task<HttpResponseMessage> GetToken(UserInfo user)
        {
            //string response = "";
            HttpResponseMessage res = new HttpResponseMessage();
            string url = "";
            url = Constants.CrawlAPI + "token";
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "password");
            dict.Add("password", user.Password);
            dict.Add("username", user.Username);
            using(var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                                                        ("application/json"));
                 res = await client.PostAsync(url, new FormUrlEncodedContent(dict));

            }

            return res;
        }



        internal async static Task<HttpResponseMessage> PostNew<T>(T model, string method)
        {
            //string response = "";
            string json = "";
            json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8);
            HttpResponseMessage res = new HttpResponseMessage();
            string url = "";
            url = Constants.CrawlAPI + method;
          
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                                                        ("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalStaticFields.Token);
                
                res = await client.PostAsync(url, content);

            }

            return res;
        }

        internal async static Task<HttpResponseMessage> Get( string method)
        {

            HttpResponseMessage res = new HttpResponseMessage();
            string url = "";
            url = Constants.CrawlAPI + method;

            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                                                        ("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalStaticFields.Token);

                res = await client.GetAsync(url);

            }

            return res;
        }

        internal async static Task<string> RegisterLocal(Registeration reg)
        {
            try
            {
                string json = "";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //client.BaseAddress = new Uri(Constants.CrawlAPI);
                string url = Constants.CrawlAPI + "api/account/register";
                json = JsonConvert.SerializeObject(reg);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var message = await client.PostAsync(url, stringContent).ConfigureAwait(true);
                var resultt = await message.Content.ReadAsStringAsync();

                //return resultt;



                //Registeration r = new Registeration();

                //r = reg;

                //return Task.FromResult(r);
                return resultt;
            }
            catch (Exception ex)
            {
                var log = ex;
                return String.Empty;
            }
     
        }
    }
}

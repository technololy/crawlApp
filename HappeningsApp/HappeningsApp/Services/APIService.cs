using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HappeningsApp.Services
{
    public class APIService
    {

        public async Task<HttpResponseMessage> Post<T>(T model,string method)
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
                return message;
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



    }
}

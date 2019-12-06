using HappeningsApp.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
        public async static Task<string> Post<T>(T model, string method)
        {
            //HttpResponseMessage message = new HttpResponseMessage();
            string json = string.Empty;
            string url = $"{Constants.CrawlAPI}{method}";
            using (var client = new HttpClient())
            {
                // var client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //client.BaseAddress = new Uri(Constants.CrawlAPI);
                //test
                json = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var message = await client.PostAsync(url, stringContent);
                var resultt = await message.Content.ReadAsStringAsync();
                //LogService.LogErrors($"Request json:\n{json}, Response json\n{resultt}");
                await LogService.LogErrorsNew(url: method, request: json, response: resultt, activity: "put Async");

                return resultt;

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
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri(Constants.CrawlAPI);
                //serialize your json using newtonsoft json serializer then add it to the StringContent
                //var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                //method address would be like api/callUber:SomePort for example
                res = await client.PostAsync("/token", new FormUrlEncodedContent(dict));
                string cont = await res.Content.ReadAsStringAsync();

                //LogService.LogErrors($"Endpoint is {client.BaseAddress.ToString()}/token. Response json\n{cont}");
                await LogService.LogErrorsNew(url: url, response: cont, activity: "Get Token");

            }

            return res;
        }

        internal async static Task<HttpResponseMessage> PostNew<T>(T model, string method)
        {
            //string response = "";
            string json = "";
            json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
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
                var resultt = await res.Content.ReadAsStringAsync();
                // LogService.LogErrors($"Endpoint is {url}. Request json:\n{json}, Response json\n{resultt}");
                await LogService.LogErrorsNew(url: url, request: json, response: resultt, activity: "POST Async");


            }

            return res;
        }

        internal async static Task<HttpResponseMessage> Put<T>(T model, string endpoint)
        {
            try
            {
                string json = "";
                json = JsonConvert.SerializeObject(model);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.CrawlAPI);
                    //serialize your json using newtonsoft json serializer then add it to the StringContent
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalStaticFields.Token);
                    //method address would be like api/callUber:SomePort for example
                    var result = await client.PutAsync(endpoint, content);
                    var response = await result.Content.ReadAsStringAsync();
                    try
                    {
                        var t = JsonConvert.DeserializeObject<T>(response);

                    }
                    catch (Exception e)
                    {
                        var log = e;


                    }
                    //LogService.LogErrors($"PUT url is {endpoint}, Request {json} and response:\n{response}");
                    await LogService.LogErrorsNew(url: endpoint, request: json, response: response, activity: "put Async");
                    return result;
                }
            }
            catch (Exception ex)
            {

                var log = ex;
                LogService.LogErrors(log.ToString());
                return new HttpResponseMessage();
            }
        }

        internal async static Task<HttpResponseMessage> LogClient<T>(T model, string method)
        {
            //string response = "";
            string json = "";
            json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF32, "application/json");
            HttpResponseMessage res = new HttpResponseMessage();
            string url = "";
            url = Constants.CrawlAPI + method;

            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                                                        ("application/json"));
                // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalStaticFields.Token);

                res = await client.PostAsync(url, content);
                var resultt = await res.Content.ReadAsStringAsync();
                // LogService.LogErrors($"Request json:\n{json}, Response json\n{resultt}");


            }

            return res;
        }

        internal async static Task<HttpResponseMessage> Get(string method, string actionPerformed = "")
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
                var content = await res.Content.ReadAsStringAsync();

                // LogService.LogErrors($"Request {url} and response:\n{content}");
                await LogService.LogErrorsNew(url: url, request: url, response: content, activity: actionPerformed + " GET Async");

                return res;


            }

        }




        internal async static Task<HttpResponseMessage> Delete(string method)
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

                res = await client.DeleteAsync(url);
                var content = await res.Content.ReadAsStringAsync();

                // LogService.LogErrors($"Request {url} and response:\n{content}");
                await LogService.LogErrorsNew(url: url, request: url, response: content, activity: "Delete Async");

                return res;


            }

        }

        internal async static Task<HttpResponseMessage> SendAsync<T>(T model, string method)
        {

            HttpResponseMessage res = new HttpResponseMessage();
            string json = "";
            json = JsonConvert.SerializeObject(model);
            string url = "";
            url = Constants.CrawlAPI + method;

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = new StringContent(json)

            };

            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                                                        ("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalStaticFields.Token);

                res = await client.SendAsync(request);
                var content = await res.Content.ReadAsStringAsync();

                // LogService.LogErrors($"Request {url} and response:\n{content}");
                await LogService.LogErrorsNew(url: url, request: url, response: content, activity: "Delete Async");

                return res;


            }

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
                await LogService.LogErrorsNew(url: url, request: json, response: resultt, activity: "REGISTER LOCAL Async");

                return resultt;
            }
            catch (Exception ex)
            {
                var log = ex;
                return String.Empty;
            }

        }

        internal async static Task<HttpResponseMessage> RegisterLocalNew(Registeration reg)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            try
            {
                string json = "";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //client.BaseAddress = new Uri(Constants.CrawlAPI);
                string url = Constants.CrawlAPI + "api/account/register";
                json = JsonConvert.SerializeObject(reg);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client2 = new HttpClient())
                {
                    client2.MaxResponseContentBufferSize = 256000;

                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    responseMessage = await client2.PostAsync(url, stringContent).ConfigureAwait(true);
                    var resultt = await responseMessage.Content.ReadAsStringAsync();
                    //LogService.LogErrors($"End point: {url}. Request json:\n{json}, Response json\n{resultt}");
                    await LogService.LogErrorsNew(url: url, request: json, response: resultt, activity: "REGISTERATION Async");

                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

                return new HttpResponseMessage();
            }

        }

        public async static Task<HttpResponseMessage> LogAsync(string json)
        {
            var user = (string.IsNullOrEmpty(GlobalStaticFields.Username) ? "NA" : GlobalStaticFields.Username);
            var jsonRequest = "{'user': '" + user + "','Error': '" + json + "'}}";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Constants.CrawlAPI);
                //serialize your json using newtonsoft json serializer then add it to the StringContent
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                //method address would be like api/callUber:SomePort for example
                //var result = await client.PostAsync("/api/Error", content);
                //string resultContent = await result.Content.ReadAsStringAsync();
                var result = new HttpResponseMessage();
                return result;
            }



        }

        public async static Task<HttpResponseMessage> LogNewAsync(Errors err)
        {
            //var user = (string.IsNullOrEmpty(GlobalStaticFields.Username) ? "NA" : GlobalStaticFields.Username);
            //var jsonRequest = "{'user': '" + user + "','Error': '" + json + "'}}";
            if (!CrossConnectivity.Current.IsConnected)
                return new HttpResponseMessage();
            var jsonRequest = JsonConvert.SerializeObject(err);
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Constants.CrawlAPI);
                //serialize your json using newtonsoft json serializer then add it to the StringContent
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                //method address would be like api/callUber:SomePort for example
                var result = await client.PostAsync("/api/Error", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                return result;
            }



        }
    }

}

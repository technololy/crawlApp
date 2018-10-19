using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class MessageServices
    {
        public MessageServices()
        {
        }


        public async Task<List<Pushresp>> GetPushMessages()
        {
            //string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
            var PushRepList = new List<Models.Pushresp>();
            string endpoint = "/api/PushNotification";
            //string endpoint = "GetByUserID?User_id=" + testuser;
            var response = await APIService.Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                content = Utility.ReplaceFirstOccurrence(content, "[", "");
                content = Utility.ReplaceLastOccurrence(content, "]", "");
                var model = JsonConvert.DeserializeObject<MyMessages>(content);
                
                //var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                if (model.Message.ToLower().Contains("success"))
                {
                    foreach (var item in model.Pushresp)
                    {
                      var msgg=  item;
                        PushRepList.Add(msgg);
                    }
                }

                return PushRepList;
            }

            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var empty = new List<Models.Pushresp>();
                return empty;
            }
        }
    }
}

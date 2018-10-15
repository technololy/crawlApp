using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class CollectionService
    {
        public async Task<ObservableCollection<Models.FavoriteModel>> GetUserFavs()
        {
            string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
            //string endpoint = "api/GetByUserID?User_id=" + GlobalStaticFields.Username;
            string endpoint = "GetByUserID?User_id=" + testuser;
            var response = await APIService.Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model1 = JsonConvert.DeserializeObject<Models.CollectionsModelResp>(content);
                var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                return model;
            }

            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var empty = new ObservableCollection<Models.FavoriteModel>();
                return empty;
            }
        }


        public async Task<Models.CollectionsModelResp> GetUserFavsNew()
        {
            //string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
            string endpoint = "/GetByUserID?User_id=" + GlobalStaticFields.Username;
            //string endpoint = "GetByUserID?User_id=" + testuser;
            var response = await APIService.Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<Models.CollectionsModelResp>(content);
                //var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                return model;
            }

            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var empty = new Models.CollectionsModelResp();
                return empty;
            }
        }

        internal async Task<bool> CreateNewCollection(string newCollectionName, string newCollectionNick)
        {
            var newCollection = new Collections
            {
                User_id=GlobalStaticFields.Username,
                Name = newCollectionName,
                NickName = newCollectionNick
            };
          var resp =await  APIService.PostNew<Collections>(newCollection, "api/Collections");
            if (resp.StatusCode== System.Net.HttpStatusCode.OK)
            {
                var cont =await resp.Content.ReadAsStringAsync();
                if (cont.Contains("Record Added Successfully"))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;

            }

        }

        internal async Task<ObservableCollection<CollectionsResp>> GetUserListCollection()
        {
            //string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
            string endpoint = "/GetByUserID?User_id=" + GlobalStaticFields.Username;
            //string endpoint = "GetByUserID?User_id=" + testuser;
            var response = await APIService.Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ObservableCollection<CollectionsResp>>(content);
                //var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                return model;
            }

            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var empty = new ObservableCollection<CollectionsResp>();
                return empty;
            }
        }
    }

 
}

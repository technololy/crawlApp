using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class CollectionService
    {
        public async Task<ObservableCollection<Models.FavoriteModel>> GetUserFavs()
        {
            string endpoint = "api/GetByUserID?User_id=" + GlobalStaticFields.Username;
            var response = await APIService.Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                return model;
            }

            else
            {
                var empty = new ObservableCollection<Models.FavoriteModel>();
                return empty;
            }
        }
    }

 
}

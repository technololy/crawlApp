using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class FavService
    {
        public FavService()
        {
        }


        public async Task<ObservableCollection<FavoriteModel>> GetFavs()
        {
            FavRootObject fav = new FavRootObject();
            ObservableCollection<FavoriteModel> listofFavs = new ObservableCollection<FavoriteModel>();
            ObservableCollection<FavRootObject> Favlist = new ObservableCollection<FavRootObject>();

            var respo = await APIService.Get("api/Favs");
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();

                fav = JsonConvert.DeserializeObject<FavRootObject>(content);
                if (fav.Message.ToLower().Contains("success"))
                {
                    listofFavs = fav.FavoriteList;
                }



            }
            else
            {
                var content = await respo.Content.ReadAsStringAsync();

            }
            return listofFavs;
        }



        public async Task<ObservableCollection<FavoriteModel>> GetFavsTest()
        {
            FavRootObject fav = new FavRootObject();
            ObservableCollection<FavoriteModel> listofFavs = new ObservableCollection<FavoriteModel>()
            {
                new FavoriteModel{Name="Clubs i party", Details="Chilling when i need to cool down"},
                new FavoriteModel{Name="events i like", Details="events to flourish"},
                new FavoriteModel{Name="outdoors i like", Details="outing is the best thing ever"},
                new FavoriteModel{Name="books i love", Details="lose my self in books of wisdom"},
                new FavoriteModel{Name="Museums that curate", Details="art is life. if you know you know"},
            };

            ObservableCollection<FavRootObject> Favlist = new ObservableCollection<FavRootObject>();

     
            return listofFavs;
        }
    }
}

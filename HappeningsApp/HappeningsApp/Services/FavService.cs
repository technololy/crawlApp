using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                else
                {

                }



            }
            else
            {
                var content = await respo.Content.ReadAsStringAsync();

            }
            return listofFavs;
        }


        public async Task<ObservableCollection<Favourite>> GetFavorites()
        {
            FavoritesModelAPIResponse fav = new FavoritesModelAPIResponse();
            ObservableCollection<Favourite> listofFavs = new ObservableCollection<Favourite>();
            ObservableCollection<FavRootObject> Favlist = new ObservableCollection<FavRootObject>();

            var respo = await APIService.Get("GetFavouritesbyUserId?UserId="+GlobalStaticFields.Username);
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();

                listofFavs = JsonConvert.DeserializeObject<ObservableCollection<Favourite>>(content);
                if (listofFavs.Any())
                {
                   return listofFavs;
                }
                else
                {
                    listofFavs = null;
                }



            }
            else
            {
                var content = await respo.Content.ReadAsStringAsync();

            }
            return listofFavs;
        }

        public async Task<bool> SaveFavorites(Favourite favourite)
        {
            FavoritesModelAPIResponse fav = new FavoritesModelAPIResponse();
            ObservableCollection<Favourite> listofFavs = new ObservableCollection<Favourite>();
            ObservableCollection<FavRootObject> Favlist = new ObservableCollection<FavRootObject>();

            var respo = await APIService.PostNew(favourite, "api/UserFavourites/");
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();


                if (content.ToLower().Contains("success"))
                {
                    return true;
                }
                else
                {
                    return false;

                }



            }
            else
            {
                return false;


            }
            return false;
        }

        internal async Task<bool> DeleteFavorite(Favourite favourite)
        {
            FavoritesModelAPIResponse fav = new FavoritesModelAPIResponse();
            ObservableCollection<Favourite> listofFavs = new ObservableCollection<Favourite>();
            ObservableCollection<FavRootObject> Favlist = new ObservableCollection<FavRootObject>();

            var respo = await APIService.Delete("api/UserFavourites?id="+favourite.Id);
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;// will remove this when niyi corrects it

                var content = await respo.Content.ReadAsStringAsync();


                if (content.ToLower().Contains("success"))
                {
                    return true;
                }
                else
                {
                    return false;

                }



            }
            else
            {
                return false;


            }
            
        }
    }
}

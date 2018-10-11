using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;

namespace HappeningsApp.ViewModels
{


    public class FavViewModel
    {
        public ObservableCollection<Models.FavoriteModel> MyFavs { get; set; }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public CollectionsModelResp Collectionz { get; set; }
        public bool IsEnabled { get; set; }
        public FavViewModel()
        {
        }

        public ObservableCollection<FavoriteModel> GetMyFavs(string username)
        {
            MyFavs = new ObservableCollection<Models.FavoriteModel>();
            CollectionService cs = new CollectionService();

            return MyFavs;
        }

        public async Task<CollectionsModelResp> GetFavs()
        {
            FavService fs = new FavService();
            CollectionService cs = new CollectionService();
       
            Collectionz = await cs.GetUserFavsNew();


            GlobalStaticFields.AllCollections = Collectionz;
            return Collectionz;
        }


    }
}

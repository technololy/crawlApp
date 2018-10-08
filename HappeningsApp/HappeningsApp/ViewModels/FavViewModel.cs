using System;
using System.Collections.ObjectModel;
using HappeningsApp.Services;

namespace HappeningsApp.ViewModels
{


    public class FavViewModel
    {
        public ObservableCollection<Models.FavoriteModel> MyFavs { get; set; }

        public FavViewModel()
        {
        }

        public ObservableCollection<Models.FavoriteModel> GetMyFavs(string username)
        {
            MyFavs = new ObservableCollection<Models.FavoriteModel>();
            CollectionService cs = new CollectionService();

            return MyFavs;
        }

    }
}

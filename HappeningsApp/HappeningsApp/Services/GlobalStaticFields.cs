using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;

namespace HappeningsApp.Services
{
    public static class GlobalStaticFields
    {
      

        public static string Token
        {
            get;
            set;
        }

        public static string Username
        {
            get;
            set;
        }

        public static ObservableCollection<FavoriteModel> FavList
        {
            get;
            set;
        }
        public static ObservableCollection<Deals> DealsRepo { get; set; }
        public static ObservableCollection<Category> CategoriesFromAPI { get;  set; }
        public static ObservableCollection<FavoriteModel> Favs { get;  set; }
        public static IntroPageViewModel IntroModel { get;  set; }
        public static ObservableCollection<Deals> dealsfromAPI { get;  set; }
        //public static List<> AllService { get; internal set; }
    }
}

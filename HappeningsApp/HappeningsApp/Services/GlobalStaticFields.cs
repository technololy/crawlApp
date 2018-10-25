using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using MvvmHelpers;

namespace HappeningsApp.Services
{
    public static class GlobalStaticFields
    {
        public static ObservableCollection<Grouping<string, Deals>> thisweekGrouping;

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
        public static ObservableCollection<GetAll2.Deal> GetAll { get;  set; }
        public static ObservableCollection<Category> CategoriesFromAPI { get;  set; }
        public static ObservableCollection<FavoriteModel> Favs { get;  set; }
        public static IntroPageViewModel IntroModel { get;  set; }
        public static ObservableCollection<Deals> dealsfromAPI { get;  set; }
        public static CollectionsModelResp AllCollections { get;  set; }
        public static ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouping { get; set; }
        public static ObservableCollection<CollectionsResp> CollectionList { get; set; }
        //public static List<> AllService { get; internal set; }
    }
}

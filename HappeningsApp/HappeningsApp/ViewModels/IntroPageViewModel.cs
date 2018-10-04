using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace HappeningsApp.ViewModels
{
    public class IntroPageViewModel//: //INotifyPropertyChanged
    {
        public ObservableCollection<ImageItems> dealsItems { get; set; }
        public ObservableCollection<ImageItems> nearbyItems { get; set; }
        public ObservableCollection<ImageItems> category { get; set; }
        public ObservableCollection<Deals> dealsfromAPI { get; set; }
        public ObservableCollection<Category> CategfromAPI { get; set; }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public ObservableCollection<Collections> collections { get; set; }
        public IntroPageViewModel(string test)
        {
            MockImageList m = new MockImageList();
            DealsService dealsService = new DealsService();
            nearbyItems = m.NearBy();
            dealsItems = m.GetDeals();
            category = m.Category();
        }
        
        public IntroPageViewModel()
        {
            
            GetDeals();
            GetCategories();
            GetTestDeals();
            GetTestCollections();
            GetFavs();
        }

        public async Task<ObservableCollection<FavoriteModel>> GetFavs()
        {
            FavService fs = new FavService();
           Favs= await fs.GetFavsTest();

            //GlobalStaticFields.AllService.Add(Favs);
            GlobalStaticFields.Favs = Favs;
            return Favs;
        }

        public void GetTestCollections()
        {
            MockDataStore m = new MockDataStore();
            collections = m.GetCollections();
        }

        public void GetTestDeals()
        {
            MockImageList m = new MockImageList();

            dealsItems = m.GetDeals();
            nearbyItems = m.NearBy();
            category = m.Category();
        }

        public async Task<ObservableCollection<Deals>> GetDeals()
        {
            DealsService ds = new DealsService();

            dealsfromAPI = await ds.GetDeals();
            GlobalStaticFields.dealsfromAPI = dealsfromAPI;
            return dealsfromAPI;

        }


        private async Task GetCategories()
        {
           CategoriesService ds = new CategoriesService();

            CategfromAPI = await ds.GetCategories();
            GlobalStaticFields.CategoriesFromAPI = CategfromAPI;


        }
    }
}

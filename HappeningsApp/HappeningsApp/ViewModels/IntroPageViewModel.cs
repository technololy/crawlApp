using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.ViewModels
{
    public class IntroPageViewModel
    {
        public ObservableCollection<ImageItems> dealsItems { get; set; }
        public ObservableCollection<ImageItems> nearbyItems { get; set; }
        public ObservableCollection<ImageItems> category { get; set; }
        public ObservableCollection<Deals> dealsfromAPI { get; set; }
        public ObservableCollection<Category> CategfromAPI { get; set; }
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
        }

        private void GetTestCollections()
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

        private async void GetDeals()
        {
            DealsService ds = new DealsService();

            dealsfromAPI = await ds.GetDeals();

        }


        private async void GetCategories()
        {
           HappeningsApp.Services.CategoriesService ds = new HappeningsApp.Services.CategoriesService();

            CategfromAPI = await ds.GetCategories();

        }
    }
}

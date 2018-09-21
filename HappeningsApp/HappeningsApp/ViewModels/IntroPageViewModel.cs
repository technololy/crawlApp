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
        //public IntroPageViewModel()
        //{
        //    MockImageList m = new MockImageList();
        //    DealsService dealsService = new DealsService();
        //    nearbyItems = m.NearBy();
        //    dealsItems = m.GetDeals();
        //    category = m.Category();
        //}
        
        public IntroPageViewModel()
        {
            
            GetDeals();

        }

        private async void GetDeals()
        {
            DealsService ds = new DealsService();

            dealsfromAPI = await ds.GetDeals();

        }
    }
}

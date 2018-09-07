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
        public IntroPageViewModel()
        {
            MockImageList m = new MockImageList();
            nearbyItems = m.NearBy();
            dealsItems = m.GetDeals();
            category = m.Category();
        }

    }
}

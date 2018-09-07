using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.ViewModels
{
    public class DealsViewModel
    {
        public ObservableCollection<ImageItems> dealsItems { get; set; }

        public DealsViewModel()
        {
            MockImageList m = new MockImageList();
            dealsItems = m.GetDeals();
        }

    }
}

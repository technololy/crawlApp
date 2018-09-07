using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.ViewModels
{
    public class NearByViewModel
    {
        public ObservableCollection<ImageItems> nearbyItems { get; set; }
        public NearByViewModel()
        {
            MockImageList m = new MockImageList();
           nearbyItems= m.NearBy();
        }
    }
}

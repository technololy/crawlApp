using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.ViewModels
{
    public class TodaysViewModel
    {
        public ObservableCollection<ImageItems> TodaysItem { get; set; }
        public TodaysViewModel()
        {
            MockImageList m = new MockImageList();
            TodaysItem = m.MockTodayStore();
        }


    }
}

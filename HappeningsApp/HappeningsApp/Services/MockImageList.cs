using HappeningsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.Services
{
    public class MockImageList
    {
        ObservableCollection<ImageItems> imageItems;

        public ObservableCollection<ImageItems> MockCategoryStore()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
                new ImageItems{Name="Outdoor Sights",Image="lagoscathedral.jpg"},
                new ImageItems{Name="Date Spots",Image="nationaltheater.jpg"},
                new ImageItems{Name="Night Life",Image="quilox.jpg"},
                new ImageItems{Name="Sporting/Cruise",Image="boatcruise.jpg"},
            };

            foreach (var item in imageList)
            {
                imageItems.Add(item);

            }

            return imageItems;
        }

        internal ObservableCollection<ImageItems> MockTodayStore()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
           
                new ImageItems{Name="Fun view and night party",Image="EkoCourts.jpg"},
                new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="OnShore/OffShore Deals",Image="lagosoilrig.jpg"},
               
           
                     new ImageItems{Name="Happeing at Lagos Cathedral",Image="lagoscathedral.jpg"},
               
                new ImageItems{Name="QUilox Season 7",Image="quilox.jpg"},
             
                new ImageItems{Name="Check out exciting homes",Image="onethousandandfour.jpg"},
            };

            foreach (var item in imageList)
            {
                
                imageItems.Add(item);

            }

            return imageItems;
        }
    }
}

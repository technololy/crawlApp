using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using Xamarin.Forms;

namespace HappeningsApp.Test
{
    public partial class TestPage : ContentPage
    {
        Models.Deals dealz;
        NewDealsModel.Deal AllNewDeals;
        NewCategoryDetailModel.Deal AllCategoryListing;
        private Activity selected2;
        public string MapsAddress { get; set; }
        public IEnumerable<string> OpeningDaysVertical { get; set; }
        public IEnumerable<OpeningDaysList> OpeningDaysGenericList { get; set; }
        public string hostOrEventName { get; set; }
        public TestPage()
        {
            InitializeComponent();
            var json = "{\"type\":\"Hosts\",\"Id\":\"1\",\"User_Id\":\"\",\"Name\":\"Craft Gourmet by Lou Baker\",\"Details\":\"Craft Gourmet by Lou Baker is one of Victoria Islands finest restaurants. Perfect for Brunches and Fine Dining. With an open kitchen and delicious meals, this is definitely one of our top pics\",\"Coordinates\":\"\",\"CategoryId\":\"0\",\"Category\":\"Fine Dinning ,Cafe & Bistro,Brunch Spots\",\"Owner_Id\":\"1\",\"Owner\":\"Craft Gourmet by Lou Baker\",\"Owner_Location\":\"14, Idowu Martins, Victoria Island, Lagos.\",\"Status\":\"Active\",\"OpeningHours\":\"\",\"Expiration_Date\":\"0001-01-01T00:00:00\",\"ImagePath\":\"assorted2.jpg\",\"Pictures\":[{\"Picture\":\"assorted2.jpg\"},{\"Picture\":\"assorted2.jpg\"},{\"Picture\":\"assorted2.jpg\"},{\"Picture\":\"assorted2.jpg\"}],\"DaysOpen\":[{\"Day\":\"Monday\",\"Time\":\"Closed\"},{\"Day\":\"Tuesday\",\"Time\":\"09:00-21:30\"},{\"Day\":\"Wednesday\",\"Time\":\"09:00-21:30\"},{\"Day\":\"Thursday\",\"Time\":\"09:00-21:30\"},{\"Day\":\"Friday\",\"Time\":\"09:00-21:30\"},{\"Day\":\"Saturday\",\"Time\":\"09:00-21:30\"},{\"Day\":\"Sunday\",\"Time\":\"12:00-21:30\"}],\"Hostopeninghours\":{\"Days\":\"\",\"OpeningTime\":\"\",\"ClosingTime\":\"\"},\"HostCategories\":[{\"Category\":\"Fine Dinning \"}],\"Created\":\"2019-02-18T12:32:44\",\"Modified\":\"2019-02-20T12:13:45\"}";
            var serialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Deals>(json);
            dealz = serialized;
            //dealz = new Models.Deals() 
            //{
                 
            //};
            //dealz = myDeals;
            MapsAddress = dealz.Owner_Location;
            hostOrEventName = dealz.Name;
            Carousel.IsVisible = true;
            SinglePicture.IsVisible = false;
            //if (dealz?.Pictures?.Count > 0)
            //{
            //    Carousel.IsVisible = true;
            //    SinglePicture.IsVisible = false;
            //    //Carousel.ItemsSource = dealz.Pictures;
            //}
            //else
            //{
            //    Carousel.IsVisible = false;
            //    SinglePicture.IsVisible = true;


            //}
            BindingContext = dealz;
        }
    }
}

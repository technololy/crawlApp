using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acr.UserDialogs;
using Geocoding;
using Geocoding.Google;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class DetailPage : ContentPage
    {
        Models.Deals dealz;
        NewDealsModel.Deal AllNewDeals;
        NewCategoryDetailModel.Deal AllCategoryListing;
        private Activity selected2;
        public string MapsAddress { get; set; }
        public IEnumerable<string> OpeningDaysVertical { get; set; }
        public IEnumerable<OpeningDaysList> OpeningDaysGenericList { get; set; }
        public string hostOrEventName { get; set; }
        public string previousPageTitle { get; set; }
        int Count = 0;
        short Counter = 0;
        int SlidePosition = 0;


        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (await DisplayAlert("Info", "Wanna add to a collection?", "Yes", "No"))
            {
                await DisplayAlert("", "Done", "OK");
            }
        }

        public DetailPage()
        {
            InitializeComponent();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = "#182C61";
        }
        //from dealslist
        //this is old and will be discarded after stability
        public DetailPage(Deals myDeals)
        {
            InitializeComponent();
            dealz = new Models.Deals();
            dealz = myDeals;
            MapsAddress = myDeals.Owner_Location;
            hostOrEventName = myDeals.Name;
            previousPageTitle = myDeals.Category;
            if (dealz?.Pictures?.Count > 0)
            {
                Carousel.IsVisible = true;
                SinglePicture.IsVisible = false;
                //Carousel.ItemsSource = dealz.Pictures;
                AutoSlide(dealz?.Pictures?.Count);
            }
            else
            {
                Carousel.IsVisible = false;
                SinglePicture.IsVisible = true;


            }
            BindingContext = myDeals;

        }

        private void AutoSlide(int? pictureCount)
        {
            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                SlidePosition++;
                if (SlidePosition == pictureCount) SlidePosition = 0;
                cv.Position = SlidePosition;
                return true;
            });
        }

        public DetailPage(NewDealsModel.Deal AllDeals)
        {
            InitializeComponent();

            AllNewDeals = new NewDealsModel.Deal();

            AllNewDeals = AllDeals;
            MapsAddress = AllNewDeals.Owner_Location;
            hostOrEventName = AllNewDeals.Name;
            previousPageTitle = AllNewDeals.Category;
            //DaysOpenListView.ItemsSource = AllNewDeals.DaysOpen;
            // var returnedList = ReturnDaysAsListAndSetListViewItemSource(AllDeals.OpeningHours);
            if (dealz?.Pictures?.Count > 0)
            {
                Carousel.IsVisible = true;
                SinglePicture.IsVisible = false;
                //Carousel.ItemsSource = dealz.Pictures;
                AutoSlide(dealz?.Pictures?.Count);
            }
            else
            {
                Carousel.IsVisible = false;
                SinglePicture.IsVisible = true;


            }
            BindingContext = AllNewDeals;

        }


        public DetailPage(NewCategoryDetailModel.Deal AllCategory)
        {
            InitializeComponent();
            OwnerAddress.IsVisible = false;
            OwnerName.IsVisible = false;
            AllCategoryListing = new NewCategoryDetailModel.Deal();

            AllCategoryListing = AllCategory;
            MapsAddress = AllCategoryListing.Owner_Location;
            hostOrEventName = AllCategoryListing.Name;
            previousPageTitle = AllCategory.Category;
            //var returnedList = ReturnDaysAsListAndSetListViewItemSource(AllCategory.OpeningHours);
            if (AllCategoryListing?.Pictures?.Count > 0)
            {
                Carousel.IsVisible = true;
                SinglePicture.IsVisible = false;
                //Carousel.ItemsSource = dealz.Pictures;
                AutoSlide(dealz?.Pictures?.Count);
            }
            else
            {
                Carousel.IsVisible = false;
                SinglePicture.IsVisible = true;


            }
            BindingContext = AllCategoryListing;

        }


        public DetailPage(GetAll2.Deal myDeals)
        {
            InitializeComponent();
            MapsAddress = myDeals.Owner_Location;
            hostOrEventName = myDeals.Name;
            BindingContext = myDeals;



        }
        //from favorites
        public DetailPage(Favourite myDeals)
        {
            InitializeComponent();
            var CopyFavIntoDeals = new GetAll2.Deal()
            {
                ImagePath = myDeals.ImageURL,
                Owner_Location = myDeals.Address,
                Details = myDeals.Description
            };
            BindingContext = CopyFavIntoDeals;

        }



        public DetailPage(Activity selected2)
        {
            InitializeComponent();

            this.selected2 = selected2;
            BindingContext = selected2;
            MapsAddress = selected2.Owner_Location;
            hostOrEventName = selected2.Name;


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var MapsAddressPlus = AddPlusToMapAddress(MapsAddress);
                var uri = new Uri($"http://maps.google.com/maps?daddr={MapsAddressPlus}&directionsmode=transit");
                Device.OpenUri(uri);
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }
        }

        private string AddPlusToMapAddress(string mapsAddress)
        {
            return mapsAddress = mapsAddress.Replace(" ", "+");
        }

        async void Uber_tapped(object sender, System.EventArgs e)
        {
            var MapsAddressPlus = AddPlusToMapAddress(MapsAddress);
            var encodedAddress = HttpUtility.UrlEncode(MapsAddress);
            string deepLink = "";
            LatLong ll = new LatLong();
            Uber uber = new Uber();

            string map = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    map = "Apple";
                    break;
                case Device.Android:
                    map = "Android";
                    break;


                default:
                    map = "Not android/IOS";
                    break;
            }

            using (UserDialogs.Instance.Loading("Loading...."))
            {
                try
                {
                    var coordinates = await ll.CalcLongLat(MapsAddress);

                    if (!string.IsNullOrEmpty(coordinates.Latitude.ToString()) &&
                    (!string.IsNullOrEmpty(coordinates.Longitude.ToString())))
                    {
                        deepLink = uber.GetUberDeepLink(coordinates.Longitude,
                        coordinates.Latitude, encodedAddress);
                        Device.BeginInvokeOnMainThread(() => Device.OpenUri(new Uri(deepLink)));


                    }
                    else
                    {
                        await DisplayAlert("Lat/Long Issue", "Can not get the latitude/longitude of the selected location", "OK");
                    }


                }


                catch (FeatureNotSupportedException fnsEx)
                {
                    await DisplayAlert("Faild", fnsEx.Message, "OK");
                }
                catch (PermissionException pEx)
                {
                    await DisplayAlert("Faild", pEx.Message, "OK");
                }


                catch (Exception ex)
                {

                    var log = ex;

                    try
                    {
                        var msg = "Navigating to uber. " +
                         "Please take note of the destination address";
                        if (Device.RuntimePlatform == Device.iOS)
                        {

                            var fakeClick = true;//i was pressed for time so i just hardcoded this. wil corrct later
                            if (fakeClick)
                            {
                                using (UserDialogs.Instance.Loading(""))
                                {
                                    IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyBeq5wC6XqvGG73TGPDzqJkx - WFwG6uqx4" };
                                    IEnumerable<Address> addresses = await geocoder.GeocodeAsync(MapsAddress);
                                    //Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
                                    if (addresses != null)
                                    {
                                        var lat = addresses.First().Coordinates.Latitude;
                                        var longitude = addresses.First().Coordinates.Longitude;
                                        deepLink = uber.GetUberDeepLink(longitude, lat, MapsAddress, hostOrEventName);
                                        // deepLink = await ll.ReturnUberDeepLinkNonPrecise(MapsAddress);
                                        Device.BeginInvokeOnMainThread(() => Device.OpenUri(new Uri(deepLink)));
                                    }
                                    else
                                    {
                                        await DisplayAlert("", "Can not get longitude and latitude. Manually search uber", "OK");

                                    }


                                }
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        await DisplayAlert("", "Error occured", "OK");
                        var logg = exx;
                    }


                }
            }

        }

        void Go_Back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

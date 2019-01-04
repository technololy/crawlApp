using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Acr.UserDialogs;
using Geocoding;
using Geocoding.Google;

namespace HappeningsApp.Views.AppViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        Models.Deals dealz;
        private Activity selected2;
        public string MapsAddress
        {
            get;
            set;
        }
        public string hostOrEventName { get; set; }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (await DisplayAlert("Info","Wanna add to a collection?","Yes","No"))
            {
               await DisplayAlert("", "Done","OK");
            }
        }

		public DetailPage ()
		{
			InitializeComponent ();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = "#182C61";
        }

        public DetailPage(HappeningsApp.Models.Deals myDeals)
        {
            InitializeComponent();
            dealz = new Models.Deals();
            dealz = myDeals;
            MapsAddress = myDeals.Owner_Location;
            BindingContext = myDeals;
      
        }

        public DetailPage(HappeningsApp.Models.GetAll2.Deal myDeals)
        {
            InitializeComponent();
          
            BindingContext = myDeals;
            MapsAddress = myDeals.Owner_Location;
         
        }

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

            using (Acr.UserDialogs.UserDialogs.Instance.Loading($"Searching {map} maps"))
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
                        

                            if (await DisplayAlert("", msg, "Ok. Got It", "No, Not interested"))
                            {
                                using (UserDialogs.Instance.Loading(""))
                                {
                                    IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyBeq5wC6XqvGG73TGPDzqJkx - WFwG6uqx4" };
                                    IEnumerable<Address> addresses = await geocoder.GeocodeAsync(MapsAddress);
                                    //Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
                                
                                    var lat = addresses.First().Coordinates.Latitude;
                                    var longitude = addresses.First().Coordinates.Longitude;
                                    deepLink = uber.GetUberDeepLink(longitude,lat, MapsAddress);
                                    // deepLink = await ll.ReturnUberDeepLinkNonPrecise(MapsAddress);
                                    Device.BeginInvokeOnMainThread(() => Device.OpenUri(new Uri(deepLink)));

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
    }
}
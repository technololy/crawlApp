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


            try
            {
              var coordinates= await ll.CalcLongLat(MapsAddress);
            
                //var loc = await Geocoding.GetLocationsAsync(MapsAddress).ConfigureAwait(false);
                //var point = loc?.FirstOrDefault();
                //if (point!=null)
                //{
                //    var longitude = point.Longitude;
                //    var latitude = point.Latitude;
                //    var uberLaunch333 = "https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]="
                //    +latitude+"&dropoff[longitude]="+longitude+"&dropoff[nickname]=" 
                //        + MapsAddress + "&dropoff" +
                //"[formatted_address]=" + encodedAddress + "&link_text=View%20team%20roster&partner_deeplink" +
                //"=partner%3A%2F%2Fteam%2F9383\n";

                //Device.OpenUri(new Uri(uberLaunch333));


                //}
                if (!string.IsNullOrEmpty(coordinates.Latitude.ToString()) && 
                (!string.IsNullOrEmpty(coordinates.Longitude.ToString())))
                {
                    deepLink =  uber.GetUberDeepLink(coordinates.Longitude, 
                    coordinates.Latitude, encodedAddress);
                    Device.BeginInvokeOnMainThread(() => Device.OpenUri(new Uri(deepLink)));


                }
                else
                {
                  await  DisplayAlert("Lat/Long Issue", "Can not get the latitude/longitude of the selected location", "OK");
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
             //   var fakelongitude = "0";
             //   var fakelatitude = "0";
                var log = ex;
                //   var uberLaunch333 = "https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]="
                //    + fakelatitude + "&dropoff[longitude]=" + fakelongitude + "&dropoff[nickname]="
                //        + MapsAddress + "&dropoff" +
                //"[formatted_address]=" + encodedAddress + "&link_text=View%20team%20roster&partner_deeplink" +
                //"=partner%3A%2F%2Fteam%2F9383\n";
                if (Device.RuntimePlatform == Device.iOS)
                {
                    using (UserDialogs.Instance.Loading("Apple Maps can not " +
                    	"find that exact address so will use parts of the address instead"))
                    {
                        deepLink = await ll.ReturnUberDeepLinkNonPrecise(MapsAddress);
                        Device.BeginInvokeOnMainThread(() => Device.OpenUri(new Uri(deepLink)));

                    }
                }

                //await DisplayAlert("Lat/Long Error", "Can not find the longitude and latitude of the chosen address " +
                	//"from google maps", "OK");

            }
        }
    }
}
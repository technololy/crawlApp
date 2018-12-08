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

            try
            {
              

                var MapsAddressPlus = AddPlusToMapAddress(MapsAddress);
                var encodedAddress = HttpUtility.UrlEncode(MapsAddress);
                var loc = await Geocoding.GetLocationsAsync(MapsAddress);
                var point = loc?.FirstOrDefault();
                if (point!=null)
                {
                    var longitude = point.Longitude;
                    var latitude = point.Latitude;
                    var uberLaunch333 = "https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]="
                    +latitude+"&dropoff[longitude]="+longitude+"&dropoff[nickname]=" 
                        + encodedAddress + "&dropoff" +
                "[formatted_address]=" + encodedAddress + "&link_text=View%20team%20roster&partner_deeplink" +
                "=partner%3A%2F%2Fteam%2F9383\n";
                    Device.OpenUri(new Uri(uberLaunch333));


                }
                //var uberLaunch = $"https://m.uber.com/ul/?action=setPickup&pickup[nickname]={encodedAddress}&" +
                //	"pickup[formatted_address]=my_location&dropoff[nickname]={encodedAddress}&dropoff" +
                //	"[formatted_address]={encodedAddress}";

                //var uberLaunch2 = "https://m.uber.com/ul/?action=setPickup&pickup[nickname]=lagos&" +
                //	"pickup[formatted_address]=my_location&dropoff[latitude]=37.802374&dropoff[longitude]" +
                //	"=-122.405818&dropoff[nickname]=Coit%20Tower&dropoff[formatted_address]=" +
                //	"1%20Telegraph%20Hill%20Blvd%2C%20San%20Francisco%2C%20CA%2094133";
                ////Device.OpenUri(new Uri("uber://?addr=" + MapsAddressPlus));

                //var uberLaunch3 = $"https://m.uber.com/ul/?action=setPickup&pickup[latitude]=37.775818&pickup" +
                //	"[longitude]=-122.418028&pickup[nickname]=UberHQ&pickup[formatted_address]=" +
                //	"1455%20Market%20St%2C%20San%20Francisco%2C%20CA%2094103&dropoff[latitude]=" +
                //	"37.802374&dropoff[longitude]=-122.405818&dropoff[nickname]=" +
                //	"Coit%20Tower&dropoff[formatted_address]=" +
                //	"1%20Telegraph%20Hill%20Blvd%2C%20San%20Francisco%2C%20CA%2094133&product_id=" +
                //	"a1111c8c-c720-46c3-8534-2fcdd730040d&link_text=View%20team%20roster&partner_deeplink" +
                //	"=partner%3A%2F%2Fteam%2F9383\n";

                //var uberLaunch33 = "https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]=" +
                //  "6.448936&dropoff[longitude]=3.3936575&dropoff[nickname]="+encodedAddress+"&dropoff" +
                //  "[formatted_address]="+encodedAddress+"&link_text=View%20team%20roster&partner_deeplink" +
                //  "=partner%3A%2F%2Fteam%2F9383\n";

                //Device.OpenUri(new Uri(uberLaunch33));
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}
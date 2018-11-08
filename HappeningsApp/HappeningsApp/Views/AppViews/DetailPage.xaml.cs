using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppLanding : ContentPage
	{
        IntroPageViewModel introPageViewMod;

		public AppLanding ()
		{
			InitializeComponent ();
            introPageViewMod = new IntroPageViewModel();

            Deals_Tapped(this, null);
          
		}

        private void Deals_Tapped(object sender, EventArgs e)
        {
            var page =new DealsList();
            //nearBy = new NearByViewModel();
            BindingContext = introPageViewMod;
            page.Content.BackgroundColor = Color.FromHex("#000015");
            PlaceHolder.Content = page.Content;
            lblDeals.TextColor = Color.Magenta;
            lblThisWeek.TextColor = Color.White;
            lblCategories.TextColor = Color.White;
            lblCollections.TextColor = Color.White;


        }

        protected override void OnAppearing()
        {

            base.OnAppearing(); 
        }
        private void ThisWeek_Tapped(object sender, EventArgs e)
        {
            var page = new Home();
            PlaceHolder.Content = page.Content;
            lblDeals.TextColor = Color.White;
            lblThisWeek.TextColor = Color.Magenta;
            lblCategories.TextColor = Color.White;
            lblCollections.TextColor = Color.White;

        }

        private void Categories_Tapped(object sender, EventArgs e)
        {
            lblDeals.TextColor = Color.White;
            lblThisWeek.TextColor = Color.White;
            lblCategories.TextColor = Color.Magenta;
            lblCollections.TextColor = Color.White;
        }

        private void Collections_Tapped(object sender, EventArgs e)
        {
            var page = new Collections();
            page.Content.BackgroundColor = Color.FromHex("#000015");

            PlaceHolder.Content = page.Content;
            lblDeals.TextColor = Color.White;
            lblThisWeek.TextColor = Color.White;
            lblCategories.TextColor = Color.White;
            lblCollections.TextColor = Color.Magenta;

        }
    }
}
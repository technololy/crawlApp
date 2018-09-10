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
            Deals_Tapped(this, null);
            introPageViewMod = new IntroPageViewModel();
            //nearBy = new NearByViewModel();
            BindingContext = introPageViewMod;
		}

        private void Deals_Tapped(object sender, EventArgs e)
        {
            var page =new Deals();
            page.Content.BackgroundColor = Color.FromHex("#000015");
            PlaceHolder.Content = page.Content;


        }

        private void ThisWeek_Tapped(object sender, EventArgs e)
        {
            var page = new Home();
            PlaceHolder.Content = page.Content;

        }

        private void Categories_Tapped(object sender, EventArgs e)
        {

        }

        private void Collections_Tapped(object sender, EventArgs e)
        {
            var page = new Collections();
            PlaceHolder.Content = page.Content;

        }
    }
}
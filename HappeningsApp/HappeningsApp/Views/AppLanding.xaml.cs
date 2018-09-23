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

            page.Content.BackgroundColor = Color.FromHex("#000015");
            PlaceHolder.Content = page.Content;
            //lblDeals.TextColor = Color.Magenta;
            //lblThisWeek.TextColor = Color.White;
            //lblCategories.TextColor = Color.White;
            //lblCollections.TextColor = Color.White;
            bxVwDeals.BackgroundColor = Color.FromHex("#FF00A1");
            bxVwCat.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;

            BindingContext = introPageViewMod;

        }

     
        private void ThisWeek_Tapped(object sender, EventArgs e)
        {
            var page = new Deals();
            PlaceHolder.Content = page.Content;
            bxVwthisWeek.BackgroundColor = Color.FromHex("#FF00A1");
            bxVwCat.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwDeals.BackgroundColor = Color.Black;
            //lblDeals.TextColor = Color.White;
            //lblThisWeek.TextColor = Color.Magenta;
            //lblCategories.TextColor = Color.White;
            //lblCollections.TextColor = Color.White;
            BindingContext = introPageViewMod;
        }

        private void Categories_Tapped(object sender, EventArgs e)
        {
            var page = new CategoriesPage();
            PlaceHolder.Content = page.Content;
            //lblDeals.TextColor = Color.White;
            //lblThisWeek.TextColor = Color.White;
            //lblCategories.TextColor = Color.Magenta;
            //lblCollections.TextColor = Color.White;
            bxVwCat.BackgroundColor = Color.FromHex("#FF00A1");
            bxVwDeals.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;
            BindingContext = introPageViewMod;

        }

        private void Collections_Tapped(object sender, EventArgs e)
        {
            var page = new Collections();
            page.Content.BackgroundColor = Color.FromHex("#000015");

            PlaceHolder.Content = page.Content;
            bxVwCol.BackgroundColor = Color.FromHex("#FF00A1");
            bxVwCat.BackgroundColor = Color.Black;
            bxVwDeals.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;
            //lblDeals.TextColor = Color.White;
            //lblThisWeek.TextColor = Color.White;
            //lblCategories.TextColor = Color.White;
            //lblCollections.TextColor = Color.Magenta;

        }
    }
}
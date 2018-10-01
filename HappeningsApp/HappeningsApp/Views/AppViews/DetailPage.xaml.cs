using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.AppViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage ()
		{
			InitializeComponent ();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = "#182C61";
        }

        public DetailPage(HappeningsApp.Models.Deals myDeals)
        {
            InitializeComponent();
            BindingContext = myDeals;
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = "#182C61";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
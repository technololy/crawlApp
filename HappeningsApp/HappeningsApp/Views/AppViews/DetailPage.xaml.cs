using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.AppViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        Models.Deals dealz;
        private Activity selected2;

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
            BindingContext = myDeals;
      
        }

        public DetailPage(HappeningsApp.Models.GetAll2.Deal myDeals)
        {
            InitializeComponent();
          
            BindingContext = myDeals;
         
        }
        public DetailPage(Activity selected2)
        {
            InitializeComponent();

            this.selected2 = selected2;
            BindingContext = selected2;

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
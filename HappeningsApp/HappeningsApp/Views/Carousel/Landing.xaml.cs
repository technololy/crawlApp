using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Carousel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Landing : ContentPage
	{
        CategoryViewModel categoryViewModel;

        public Landing ()
		{
			InitializeComponent ();
            BindingContext = categoryViewModel = new CategoryViewModel();
            
            
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new MainPage());

            }
            catch (Exception ex)
            {
                string log = ex.ToString();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Collections());
        }
    }
}
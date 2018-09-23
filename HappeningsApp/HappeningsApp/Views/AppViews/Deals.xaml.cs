using HappeningsApp.ViewModels;
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
	public partial class Deals : ContentPage
	{
        //TodaysViewModel todaysViewModel;
        //DealsViewModel _DealsViewModel;
        //NearByViewModel nearBy;
        IntroPageViewModel introPageViewMod;
        public Deals ()
		{
			InitializeComponent ();
            //_DealsViewModel = new DealsViewModel();
            //introPageViewMod = new IntroPageViewModel();
            //nearBy = new NearByViewModel();
            BindingContext = introPageViewMod;

		}

        private void DealsView_SelectedItemChanged(object sender, EventArgs e)
        {

            if (DealsView.SelectedItem==null)
            {
                return;
            }
            //var select = introPageViewMod.dealsItems.ElementAt(DealsView.)

            Application.Current.MainPage.Navigation.PushAsync(new DetailPage());
           // Navigation.PushAsync(new DetailPage());
        }
    }
}
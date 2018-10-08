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
	public partial class ThisWeek : ContentPage
	{
		public ThisWeek ()
		{
			InitializeComponent ();
            var days = new List<string>() { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun", "All" };
            segment.Children = days;
        }

        private void TapPlus_Tapped(object sender, EventArgs e)
        {

        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Deals());
        }

        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as HappeningsApp.Models.Deals;
            if (selected != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }
        void Handle_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            //ItemSelectedText.Text = $"{e.SelectedItem}";
        }

    }
}
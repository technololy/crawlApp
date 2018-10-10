using HappeningsApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
       public List<string> days;
		public ThisWeek ()
		{
			InitializeComponent ();
            
             days = new List<string>() { "MON", "TUE", "WED", "THUR", "FRI", "SAT", "SUN", "ALL" };
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
            try
            {            
                var select = e.SelectedItem as string;
                string fulldayName = GetFullDay(select);
                var listViewDatax = dealsListview.ItemsSource;          
                var dealsList = GlobalStaticFields.dealsfromAPI.Where(d=>d.Expiration_Date.
                DayOfWeek.ToString().ToLower()==fulldayName.ToLower()).FirstOrDefault();
                dealsListview.ScrollTo(dealsList, ScrollToPosition.Start, true);
            
            }
            catch (Exception ex)
            {

                var log = ex;
            }

        }

        private string GetFullDay(string select)
        {
            string day = "";
            switch (select)
            {
                case "MON":
                    day = "Monday";
                    break;
                case "TUE":
                    day = "Tuesday";
                    break;
                case "WED":
                    day = "Wednesday";
                    break;
                case "THUR":
                    day = "Thursday";
                    break;
                case "FRI":
                    day = "Friday";
                    break;
                case "SAT":
                    day = "Saturday";
                    break;
                case "SUN":
                    day = "Sunday";
                    break;
                case "ALL":
                    day = "All";
                    break;
                
                default:
                    break;

            }
            return day;

        }
    }
}
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
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
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryThingGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();

        IntroPageViewModel ivm;

        public ThisWeek ()
		{
			InitializeComponent ();
            RefreshListView();
            ivm = new IntroPageViewModel();
             days = new List<string>() { "ALL", "MON", "TUE", "WED", "THUR", "FRI", "SAT", "SUN" };
            segment.Children = days;
        }

        private void TapPlus_Tapped(object sender, EventArgs e)
        {

        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            //Navigation.PushModalAsync(new Deals());
        }

        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as NewDealsModel.Deal;
            if (selected != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                //var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                //Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }
        void Handle_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {            
                var select = e.SelectedItem as string;
                string fulldayName = GetFullDay(select);
                var listViewDatax = dealsListview.ItemsSource;          
                var dealsList = GlobalStaticFields.GetEvery.Where(d=>d.Expiration_Date.
                DayOfWeek.ToString().ToLower()==fulldayName.ToLower()).FirstOrDefault();
                dealsListview.ScrollTo(dealsList, ScrollToPosition.MakeVisible, true);
            
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
            }

        }


        private void RefreshListView()
        {
            dealsListview.RefreshCommand = new Command(async() =>

            {
                 ivm = new IntroPageViewModel();
                ivm.GetDeals();
                ivm.GetCategories();
                ivm.GetAll();
                await Task.Delay(3000);

                var groupByDate = GroupListByDate();
                //GlobalStaticFields.GetAllGrouping = groupByDate;
                BindingContext = groupByDate;
                await Task.Delay(3000);
                dealsListview.IsRefreshing = false;
            });
        }
        private ObservableCollection<Grouping<string, NewDealsModel.Deal>> GroupListByDate()
        {
            try
            {
                if (ivm?.GetEvery==null)
                {
                    return GetEveryThingGrouped;
                }
                var grp = from h in ivm?.GetEvery
                          orderby h?.Expiration_Date
                          group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                          select new Grouping<string, NewDealsModel.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
                GetEveryThingGrouped.Clear();
                foreach (var g in grp)
                {
                    GetEveryThingGrouped.Add(g);
                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
                MyToast.DisplayToast( Color.Red,"Slight error occured parsing response");
            }
         
            return GetEveryThingGrouped;
        }

        private ObservableCollection<Grouping<string, GetAll2.Deal>> GroupListByDateOriginal()
        {
            try
            {
                if (ivm?.GgetAll == null)
                {
                    return GetAllGrouped;
                }
                var grp = from h in ivm?.GgetAll
                          orderby h?.Expiration_Date
                          group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                          select new Grouping<string, GetAll2.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
                GetAllGrouped.Clear();
                foreach (var g in grp)
                {
                    GetAllGrouped.Add(g);
                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
                MyToast.DisplayToast(Color.Red, "Slight error occured parsing response");
            }

            return GetAllGrouped;
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
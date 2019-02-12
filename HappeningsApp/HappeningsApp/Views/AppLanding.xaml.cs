using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using HappeningsApp.Views.Search;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        IntroPageViewModel ivm;
        public ObservableCollection<Grouping<string, Models.Deals>> GroupedDeals = new ObservableCollection<Grouping<string, Models.Deals>>();
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryThingGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();


        public AppLanding ()
		{
			InitializeComponent();
            try
            {
                ivm = new IntroPageViewModel();
                ivm.GetDeals();
                ivm.GetCategories();
                ivm.GetAll();
                ivm.GetAllSearchFromNewModel();
                ivm.GetAllThisWeek();
                Deals_Tapped(this, null);
                ShowSurVeyOne();

            }
            catch (Exception ex)
            {
                var log = ex;
            }
          
          
		}

        void Search_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SearchPage(),true);
        }
    

   

        private async Task ShowSurVeyOne()
        {
          await Task.Delay(30000);
           await NowShowOne();
        }

        private async Task NowShowOne()
        {
            try
            {
                if (!Application.Current.Properties.ContainsKey("DidSurveySubmitOk"))
                {
                    await Navigation.PushModalAsync(new Survey.SurveyOne(), true);

                }

                else
                {
                    if (Convert.ToBoolean(Application.Current.Properties["DidSurveySubmitOk"]))
                    {
                        //dont show survey
                    }
                    else
                    {
                        //show survey
                        await LogService.LogErrorsNew(activity: "User was presented survey one screen");

                        await Navigation.PushModalAsync(new Survey.SurveyOne(), true);
                    }
                }
            
            }
            catch (Exception ex)
            {
                var log = ex;
                Application.Current.Properties["SurveyOne"] = false;
                Application.Current.Properties["SurveyTwo"] = false;
                Application.Current.Properties["DidSurveySubmitOk"] = false;
            }
        }

  

        private async void Deals_Tapped(object sender, EventArgs e)
        {
        
            try
            {
                var page = new DealsList();

                page.Content.BackgroundColor = Color.FromHex("#000015");
                PlaceHolder.Content = page.Content;
                //lblDeals.TextColor = Color.Magenta;
                //lblThisWeek.TextColor = Color.White;
                //lblCategories.TextColor = Color.White;
                //lblCollections.TextColor = Color.White;
                bxVwDeals.BackgroundColor = Color.FromHex("#3498db");

                bxVwCat.BackgroundColor = Color.Black;
                bxVwCol.BackgroundColor = Color.Black;
                bxVwthisWeek.BackgroundColor = Color.Black;
                ivm.AllDeals = GlobalStaticFields.AllDeals;
                BindingContext = ivm;
                await LogService.LogErrorsNew(activity: "User clicked on Deals Tab");
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());

            }

        }


        private async void ThisWeek_Tapped(object sender, EventArgs e)
        {
         try
            {
                var page = new ThisWeek();
                PlaceHolder.Content = page.Content;
                bxVwthisWeek.BackgroundColor = Color.FromHex("#3498db");
                bxVwCat.BackgroundColor = Color.Black;
                bxVwCol.BackgroundColor = Color.Black;
                bxVwDeals.BackgroundColor = Color.Black;

                var groupEveryByDate = GroupListByDate();
                GlobalStaticFields.GetEveryGrouping = groupEveryByDate;
                BindingContext = groupEveryByDate;
                await LogService.LogErrorsNew(activity: "User clicked on This Week Tab");
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());

            }

        }
        private ObservableCollection<Grouping<string, NewDealsModel.Deal>> GroupListByDate()
        {
            try
            {
                if (ivm?.GetEvery == null)
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
                MyToast.DisplayToast(Color.Red, "Slight error occured parsing response");
            }

            return GetEveryThingGrouped;
        }

        private ObservableCollection<Grouping<string, NewDealsModel.Deal>> GroupListByDatexx()
        {
            var todaysDate = DateTime.Now;
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            var AfterAWeek = startOfWeek.AddDays(7);
            var sevenDaysAfter = DateTime.Now.AddDays(7);
            var grp = from h in ivm?.GetEvery
                      where h.Expiration_Date >= startOfWeek && h?.Expiration_Date.Date <= AfterAWeek

                      orderby h?.Expiration_Date
                    group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                    select new Grouping<string, NewDealsModel.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
            GetEveryGrouped.Clear();
            foreach (var g in grp)
            {
                GetEveryGrouped.Add(g);
            }
            return GetEveryGrouped;
        }

        private ObservableCollection<Grouping<string, GetAll2.Deal>> GroupListByDateOriginal()
        {
            var todaysDate = DateTime.Now;
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            var AfterAWeek = startOfWeek.AddDays(7);
            var sevenDaysAfter = DateTime.Now.AddDays(7);
            var grp = from h in ivm?.GgetAll
                      where h?.Expiration_Date.Date >= startOfWeek && h?.Expiration_Date.Date <= AfterAWeek

                      orderby h?.Expiration_Date
                      group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                      select new Grouping<string, GetAll2.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
            GetAllGrouped.Clear();
            foreach (var g in grp)
            {
                GetAllGrouped.Add(g);
            }
            return GetAllGrouped;
        }

        //old. retained for some magic done within it
        private ObservableCollection<Grouping<string, Models.Deals>> GroupListByDateBkUp()
        {
            var todaysDate = DateTime.Now;
            var sevenDaysAfter = DateTime.Now.AddDays(7);
            var grp = from h in ivm.DealsfromAPI
                      where h.Expiration_Date.Date>=todaysDate && h.Expiration_Date.Date<=sevenDaysAfter 
                      orderby h.Expiration_Date
                      group h by h.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                                   
                      select new Grouping<string, Models.Deals>(ThisWeeksGroup.Key, ThisWeeksGroup);

            foreach (var g in grp)
            {
                GroupedDeals.Add(g);
            }
            return GroupedDeals;
        }
        private async void Categories_Tapped(object sender, EventArgs e)
        {
            try
            {
                var page = new CategoriesPage();
                page.Content.BackgroundColor = Color.FromHex("#000015");

                PlaceHolder.Content = page.Content;

                bxVwCat.BackgroundColor = Color.FromHex("#3498db");
                bxVwDeals.BackgroundColor = Color.Black;
                bxVwCol.BackgroundColor = Color.Black;
                bxVwthisWeek.BackgroundColor = Color.Black;
                ivm.CategfromAPI = GlobalStaticFields.CategoriesFromAPI;
                BindingContext = ivm;
                await LogService.LogErrorsNew(activity: "User clicked on Categories Tab");
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }
           


        }

        private async void Collections_Tapped(object sender, EventArgs e)
        {
         try
            {
                var page = new Collections();
                page.Content.BackgroundColor = Color.FromHex("#000015");

                PlaceHolder.Content = page.Content;
                bxVwCol.BackgroundColor = Color.FromHex("#3498db");
                bxVwCat.BackgroundColor = Color.Black;
                bxVwDeals.BackgroundColor = Color.Black;
                bxVwthisWeek.BackgroundColor = Color.Black;
                BindingContext = ivm;
                await LogService.LogErrorsNew(activity: "User clicked on Collections Tab");
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());

            }


        }

        protected  override  bool OnBackButtonPressed()
        {
            return true;
        }

        
       async void settings_Tapped(object sender, System.EventArgs e)
        {
            //await settingsImage.ScaleTo(2, 500, Easing.SinInOut);
            //await settingsImage.ScaleTo(1, 500, Easing.SinInOut);
            await settingsImage.RotateTo(360, 300, Easing.SinInOut);
            await settingsImage.RotateTo(0, 300, Easing.SinInOut);
            
            //await LogService.LogErrorsNew(activity: "User clicked on Settings");

            await Navigation.PushAsync(new Settings.SettingsPage(),true);
        }
    }
}
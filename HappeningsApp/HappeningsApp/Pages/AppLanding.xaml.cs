using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using HappeningsApp.Views.Search;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using HappeningsApp.Views;
using HappeningsApp.Pages.Home;

namespace HappeningsApp.Pages
{
    public partial class AppLanding : ContentPage
    {
        IntroPageViewModel ivm;
        MyCreatedCollectionViewModel mcvm;
        public ObservableCollection<Grouping<string, Models.Deals>> GroupedDeals = new ObservableCollection<Grouping<string, Models.Deals>>();
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryThingGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();
        public string PageLandingTitle { get; set; }

        public AppLanding()
        {
            InitializeComponent();
            try
            {
                ivm = new IntroPageViewModel();
                mcvm = new MyCreatedCollectionViewModel();
                ivm.GetDeals();
                ivm.GetCategories();
                ivm.GetAll();
                ivm.GetAllSearchFromNewModel();
                ivm.GetAllThisWeek();
                ivm.Initializefavourites();
                mcvm.InitializeFavListNewUI();
                ThisWeek_Tapped(this, null);
                ShowSurVeyOne();
                this.BindingContext = this;
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }

        private async void ThisWeek_Tapped(object sender, EventArgs e)
        {
            try
            {
                var page = new Pages.Home.ThisWeek();
                PlaceHolder.Content = null;

                PlaceHolder.Content = page.Content;
                //bxVwthisWeek.BackgroundColor = Color.FromHex("#3498db");
                //bxVwCat.BackgroundColor = Color.Black;
                //bxVwCol.BackgroundColor = Color.Black;
                //bxVwDeals.BackgroundColor = Color.Black;
                imgThisWeek.Source = ImageSource.FromFile("ic_small_calendar_blue");

                imgCat.Source = ImageSource.FromFile("ic_sort_with_three_lines_white");
                imgProf.Source = ImageSource.FromFile("ic_user_white");
                imgFav.Source = ImageSource.FromFile("ic_favorite_border");
                lblPageLandingTitle.Text = "This Week";
                lblCategories.TextColor = Color.White;
                lblProfiles.TextColor = Color.White;
                lblThisWeek.TextColor = Color.FromHex("#3498db");
                lblFavorites.TextColor = Color.White;

              
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
                var grp = from h in ivm?.GetThisWeek
                          orderby h?.type
                          group h by h?.type into ThisWeeksGroup
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



        void Search_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SearchPage(), true);
        }




        private async Task ShowSurVeyOne()
        {
            await Task.Delay(10000);
            if (!string.IsNullOrEmpty(GlobalStaticFields.Username))
            {
                await NowShowOne();
            }
          
        }

        private async Task NowShowOne()
        {
            try
            {
                if (!Application.Current.Properties.ContainsKey("DidSurveySubmitOk"))
                {
                    await Navigation.PushModalAsync(new Pages.ProfileData.QuestionsAboutYou(), true);

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

                        await Navigation.PushModalAsync(new ProfileData.QuestionsAboutYou(), true);
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
        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        async void notif_Tapped(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new Notification.Notifications());

        }

        private async void Categories_Tapped(object sender, EventArgs e)
        {
            try
            {
              

                var page = new Categories();
                page.Content.BackgroundColor = Color.FromHex("#000015");
                PlaceHolder.Content = null;

                PlaceHolder.Content = page.Content;
                lblPageLandingTitle.Text = "Categories";
                lblCategories.TextColor = Color.FromHex("#3498db");
                lblFavorites.TextColor = Color.White;
                lblThisWeek.TextColor = Color.White;
                lblProfiles.TextColor = Color.White;

                imgThisWeek.Source = ImageSource.FromFile("ic_small_calendar_white");
                imgCat.Source = ImageSource.FromFile("ic_three_lines_blue");
                imgProf.Source = ImageSource.FromFile("ic_user_white");
                imgFav.Source = ImageSource.FromFile("ic_favorite_border");
                //bxVwCat.BackgroundColor = Color.FromHex("#3498db");
                //bxVwDeals.BackgroundColor = Color.Black;
                //bxVwCol.BackgroundColor = Color.Black;
                //bxVwthisWeek.BackgroundColor = Color.Black;
                ivm.CategfromAPI = GlobalStaticFields.CategoriesFromAPI;
                BindingContext = ivm;
               // await LogService.LogErrorsNew(activity: "User clicked on Categories Tab");
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }



        }

        private async void Profiles_Tapped(object sender, System.EventArgs e)
        {
            lblPageLandingTitle.Text = "Profiles";
            lblCategories.TextColor = Color.White;
            lblFavorites.TextColor = Color.White;
            lblThisWeek.TextColor = Color.White;
            lblProfiles.TextColor = Color.FromHex("#3498db");

            imgThisWeek.Source = ImageSource.FromFile("ic_small_calendar_white");
            imgFav.Source = ImageSource.FromFile("ic_favorite_border");
            imgProf.Source = ImageSource.FromFile("ic_user");
            imgCat.Source = ImageSource.FromFile("ic_sort_with_three_lines_white");
            //await Navigation.PushAsync(new Home.Profile());
            var page = new Profile();
            page.Content.BackgroundColor = Color.FromHex("#000015");
            
            PlaceHolder.Content = null;
            PlaceHolder.Content = page.Content;

        }

       async void Favorites_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                var page = new Favorites();
                //page.Content.BackgroundColor = Color.FromHex("#000015");
                PlaceHolder.Content = null;
                BindingContext = GlobalStaticFields.FavoriteList;

                PlaceHolder.Content = page.Content;
                lblPageLandingTitle.Text = "Favorites";

                lblCategories.TextColor = Color.White;
                lblProfiles.TextColor = Color.White;
                lblThisWeek.TextColor = Color.White;
                lblFavorites.TextColor = Color.FromHex("#3498db");

                imgThisWeek.Source = ImageSource.FromFile("ic_small_calendar_white");
                imgCat.Source = ImageSource.FromFile("ic_sort_with_three_lines_white");
                imgProf.Source = ImageSource.FromFile("ic_user_white");
                imgFav.Source = ImageSource.FromFile("ic_like");
            }
            catch (Exception ex)
            {
                var log = ex;
               await LogService.LogErrorsNew(error:ex.Message.ToString());
               await LogService.LogErrorsNew(error:"Testing logging error at fav tapped");
               await DisplayAlert("Error", "Unexpected error occured. The tech Team has been alerted", "Ok");
            }
        }
    }
}

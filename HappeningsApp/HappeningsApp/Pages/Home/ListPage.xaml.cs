using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;
using HappeningsApp.Pages.Home;
using HappeningsApp.ViewModels;

namespace HappeningsApp.Pages.Home
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }


        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as NewCategoryDetailModel.Deal;
            if (selected != null)
            {
                Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                //var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;

                // Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }

        public ListPage(Category cat)
        {
            InitializeComponent();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;

            try
            {

                Title = cat.CategoryName;
                
                GetCategoryByID(cat);


            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

            }

        }


        private async Task GetCategoryByID(Category cat)
        {

            DealsService ds = new DealsService();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {


                var detail = await ds.GetAllByCategoryID2(cat.CategoryID);
                if (detail?.Count > 0)
                {
                    ObservableCollection<NewCategoryDetailModel.Deal> deals = new ObservableCollection<NewCategoryDetailModel.Deal>(detail);

                    this.BindingContext = deals;
                 
                }
                else
                {
                    await DisplayAlert("", "No Content", "Back");
                    await Navigation.PopAsync(true);
                    return;
                }
            }


        }

        private async void TapPlus_Tapped(object sender, EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                try
                {
                    var img = sender as Image;
                    var item = img.BindingContext as NewCategoryDetailModel.Deal;
                    //var item2 = img.BindingContext as Category;
                    //Application.Current.MainPage.Navigation.PushAsync(new Favorites(item), true);
                    FavService fs = new FavService();
                    Favourite favourite = new Favourite()
                    {
                        Address = item.Owner_Location,
                        Description = item.Details,
                        ImageURL = item.ImagePath,
                        Name = item.Name,
                        UserId = GlobalStaticFields.Username,
                        Id = Convert.ToInt32(item.Id)
                    };

                    var result = await fs.SaveFavorites(favourite);
                    if (result)
                    {
                        RefreshFavourites();

                        await DisplayAlert("", "Great!! Just added " + item.Name + " to your list of favorites", "Ok");

                    }
                    else
                    {
                        await DisplayAlert("", "Sorry!! Could not add " + item.Name + " to your list of favorites right now.", "Ok");

                    }
                }
                catch (Exception ex)
                {
                    var log = ex;
                    await DisplayAlert("", "Sorry. Error occured", "Ok");
                    await LogService.LogErrorsNew(error: ex.Message.ToString());

                }
            }
                
        }

        private void RefreshFavourites()
        {
            IntroPageViewModel ivm = new IntroPageViewModel();
            ivm.Initializefavourites();
        }
    }
}

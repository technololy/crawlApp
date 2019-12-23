using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using HappeningsApp.Views.Favourites;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class Favorites : ContentPage
    {
        MyCreatedCollectionViewModel mcvm = new MyCreatedCollectionViewModel();
        public CollectionsResp Collectz
        {
            get;
            set;
        }
        public Favorites()
        {
            InitializeComponent();
            //if (GlobalStaticFields.CollectionList !=null)
            //{

            //    BindingContext = GlobalStaticFields.CollectionList;
            //}
            //else
            //{
                mcvm = new MyCreatedCollectionViewModel();
                mcvm.InitializeFavListNewUI();
                mcvm.PageTitle = "Favorites";
                BindingContext = mcvm;
            //}
        }


        public Favorites(NewCategoryDetailModel.Deal deals)
        {
            InitializeComponent();

            mcvm = new MyCreatedCollectionViewModel
            {
                IsEnabled = true,
                CurrentlySelectedFavorite = deals
            };
            mcvm.GetListCollection();

            BindingContext = mcvm;


        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try
            {
                if (e.Item == null)
                {
                    return;
                }
                var selectedItem = e.Item as CollectionsResp;
                Navigation.PushAsync(new MyDetailedFavorites(selectedItem));

                //Deselect Item
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var secondaryPage = new AddNewFavourite();
            // Subscribe to the event when things are updated
            secondaryPage.AddOperationCompeleted += SecondaryPage_AddOperationCompeleted;
            Navigation.PushModalAsync(new AddNewFavourite());
        }

        void SecondaryPage_AddOperationCompeleted(object sender, EventArgs e)
        {
            mcvm.GetListCollection();

            // Unsubscribe to the event to prevent memory leak
            (sender as AddNewFavourite).AddOperationCompeleted -= SecondaryPage_AddOperationCompeleted;
            // Do something after change

        }


        private void AddToFav_Tapped(object sender, EventArgs e)
        {
            // fvvm.AddNewTest();//this works. went tru hell getting it to work via viewmodel, never knowing this would wwork but o think it worked cos of command parameter added
        }
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(""))
            {
                try
                {
                    var s = (TappedEventArgs)e;
                    var castedObj = s.Parameter as Favourite;
                    FavService fs = new FavService();

                    bool r = await fs.DeleteFavorite(castedObj).ConfigureAwait(false);
                    if (r)
                    {
                        IntroPageViewModel ivm = new IntroPageViewModel();
                        ivm.Initializefavourites();
                    }
                }
                catch (Exception ex)
                {
                    var log = ex;
                    LogService.LogErrors(ex.ToString());
                }
            }


        }
        void Handle_ScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
        {


        }

        protected async override void OnAppearing()
        {
            //MyCreatedCollectionViewModel mcvmm = new MyCreatedCollectionViewModel();
            //await mcvm.GetListCollection();

            //if (GlobalStaticFields.CollectionList == null || GlobalStaticFields.CollectionList.Count == 0)
            //    await mcvm.GetListCollection();
            //else
            //{
            //    mcvm.CollectionsList?.Clear();
            //    foreach (var item in GlobalStaticFields.CollectionList)
            //    {
            //        mcvm.CollectionsList.Add(item);
            //    }
            //}

            //this.BindingContext = mcvm;
            base.OnAppearing();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(""))
            {
                try
                {
                    var s = (TappedEventArgs)e;
                    if (!(s.Parameter is Favourite castedObj))
                    {
                        return;
                    }
                    // mcvm = new MyCreatedCollectionViewModel();

                    var r = await mcvm.DeleteSavedFavorites(castedObj,
                                                            Collectz.Id.ToString()).ConfigureAwait(false);
                    if (r)
                    {
                        Collectz.Details.Remove(castedObj);
                        mcvm.UserFavs = null;
                        mcvm.UserFavs = Collectz;

                        Device.BeginInvokeOnMainThread(() =>
                                                      MyFavList.ItemsSource = null

                                                      );
                        Device.BeginInvokeOnMainThread(() => MyFavList.ItemsSource = mcvm.UserFavs.Details);
                        //Device.BeginInvokeOnMainThread(()=>this.BindingContext = mcvm.UserFavs);
                        // Collectz.Details.Remove(castedObj);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Info", "Not successful", "OK"));
                    }
                }
                catch (Exception ex)
                {
                    var log = ex;
                    LogService.LogErrors(ex.ToString());
                    //await DisplayAlert("Info", "Error", "OK");

                }
            }
        }
    }
}

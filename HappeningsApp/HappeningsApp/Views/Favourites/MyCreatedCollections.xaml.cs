using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using Xamarin.Forms;

namespace HappeningsApp.Views.Favourites
{
    public partial class MyCreatedCollections : ContentPage
    {
        MyCreatedCollectionViewModel mcvm;
        public MyCreatedCollections()
        {
            InitializeComponent();
            mcvm = new MyCreatedCollectionViewModel();
            //BindingContext = new MyCreatedCollectionViewModel();//this was the original that worked
            //im saving it cos i suffered to understand it
            //now lemme try this new one below
            mcvm.GetListCollection();
            BindingContext = mcvm ;
        }

        public MyCreatedCollections(Deals deals)
        {
            InitializeComponent();

            mcvm = new MyCreatedCollectionViewModel
            {
                IsEnabled = true,
                CurrentlySelectedFav = deals
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
            secondaryPage.AddOperationCompeleted+= SecondaryPage_AddOperationCompeleted;
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
            using(UserDialogs.Instance.Loading(""))
            {
                try
                {
                    var s = (TappedEventArgs)e;
                    var castedObj = s.Parameter as CollectionsResp;
                   // mcvm = new MyCreatedCollectionViewModel();
                    var r = await mcvm.DeleteCollection(castedObj).ConfigureAwait(false);
                    if (r)
                    {
                        mcvm.CollectionsList.Remove(castedObj);
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
            await mcvm.GetListCollection();

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
    }
}

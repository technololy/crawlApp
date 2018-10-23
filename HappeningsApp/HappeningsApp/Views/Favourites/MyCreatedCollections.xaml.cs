using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using HappeningsApp.Models;
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
                    IsEnabled = true
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
            Navigation.PushModalAsync(new AddNewFavourite());
        }

        private void AddToFav_Tapped(object sender, EventArgs e)
        {
            // fvvm.AddNewTest();//this works. went tru hell getting it to work via viewmodel, never knowing this would wwork but o think it worked cos of command parameter added
        }
        private void Delete_Tapped(object sender, EventArgs e)
        {
            var s = (TappedEventArgs)e;
            var castedObj = s.Parameter as CollectionsResp;
        }
        void Handle_ScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
        {


        }
    }
}

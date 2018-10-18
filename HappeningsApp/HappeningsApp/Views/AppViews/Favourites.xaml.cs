using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class Favourites : ContentPage
    {
       public FavViewModel fvvm { get; set; } = new FavViewModel();
        Deals Dealss = new Deals();

        public  Favourites()//from collections
        {
            InitializeComponent();
            GetFavList();
        }

        private async Task GetFavList()
        {          
            await fvvm.GetListCollection();
            //MyFavList.ItemsSource = fvvm.CollectionsList;//newly added to test if it would work
               BindingContext = fvvm;
        }

        public Favourites(string fav)
        {
            InitializeComponent();
            GetFavList();
        }

        public Favourites(Deals deals)// from categories
        {
            InitializeComponent();
            fvvm.IsEnabled = true;
            Dealss = deals;
            fvvm.CurrentlySelectedFav = Dealss;
            GetFavList();

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
        void Handle_ScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
        {


        }
        private void Button_Clicked(object sender, EventArgs e)
        {
          Navigation.PushModalAsync(new Views.Favourites.AddNewFavourite());
        }

        private void AddToFav_Tapped(object sender, EventArgs e)
        {
           // fvvm.AddNewTest();//this works. went tru hell getting it to work via viewmodel, never knowing this would wwork but o think it worked cos of command parameter added
        }

        protected async override void OnAppearing()
        {
            if (GlobalStaticFields.CollectionList == null || GlobalStaticFields.CollectionList.Count == 0)
                await GetFavList();
            else
            {
                fvvm.CollectionsList.Clear();
                foreach (var item in GlobalStaticFields.CollectionList)
                {
                    fvvm.CollectionsList.Add(item);
                }
            }
            base.OnAppearing();
        }
    }
}

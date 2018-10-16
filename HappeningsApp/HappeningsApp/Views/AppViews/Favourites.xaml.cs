using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class Favourites : ContentPage
    {
       public FavViewModel fvvm { get; set; } = new FavViewModel();
        

        public  Favourites()//from collections
        {
            InitializeComponent();
           // favViewModel = new FavViewModel();
            //BindingContext = GlobalStaticFields.AllCollections;
     
            GetFavList();
        }

        

        private async Task GetFavList()
        {
            //favViewModel = new FavViewModel();
            //await fvvm.GetFavs();
            await fvvm.GetListCollection();
            MyFavList.ItemsSource = fvvm.CollectionsList;
               BindingContext = fvvm;


        }

        public Favourites(string fav)
        {
            InitializeComponent();
            GetFavList();
        }

        public Favourites(Models.Deals deals)// from categories
        {
            InitializeComponent();
            fvvm.IsEnabled = true;
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
                var selectedItem = e.Item as Models.FavoriteModel;
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
            fvvm.AddNewTest();
        }
    }
}

using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class Favourites : ContentPage
    {
        public FavViewModel fvvm;


        public Favourites()//from collections
        {
            InitializeComponent();
            fvvm = new FavViewModel();
            BindingContext = fvvm;
            // favViewModel = new FavViewModel();
            //BindingContext = GlobalStaticFields.AllCollections;

           // GetFavList();
        }



        private async Task GetFavList()
        {
            //favViewModel = new FavViewModel();
            //await fvvm.GetFavs();
            await fvvm.GetListCollection();
            //MyFavList.ItemsSource = fvvm.CollectionsList;
            //   BindingContext = fvvm;


        }

        public Favourites(string fav)
        {
            InitializeComponent();

        }

        public Favourites(Models.Deals deals)// from categories
        {
            InitializeComponent();
            fvvm.IsEnabled = true;
            //  GetFavList();

        }

        private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
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

        private void Handle_ScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
        {


        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Views.Favourites.AddNewFavourite());
        }

        private void AddToFav_Tapped(object sender, EventArgs e)
        {

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

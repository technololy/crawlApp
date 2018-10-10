using System;
using System.Collections.Generic;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class Favourites : ContentPage
    {
        FavViewModel favViewModel;
        void Handle_ScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
        {
            string p = "g";

        }

        public  Favourites()
        {
            InitializeComponent();
            favViewModel = new FavViewModel();
            BindingContext = GlobalStaticFields.AllCollections;
           //BindingContext = GlobalStaticFields.IntroModel.Favs != null&& GlobalStaticFields.IntroModel.Favs?.Count > 0 ?GlobalStaticFields.IntroModel.Favs :  new FavService().GetFavsTest().Result;
            GetFavList();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {

            try
            {
                Button button = (Button)sender;
                StackLayout layout = (StackLayout)button.Parent;
                Label label = (Label)layout.Children[0];    
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());

            }


            var select = MyFavList.SelectedItem;
        }

        private void GetFavList()
        {
            if ( GlobalStaticFields.Favs?.Count>0)
            {
                BindingContext = GlobalStaticFields.AllCollections;

            }

        }

        public Favourites(string fav)
        {
            InitializeComponent();
            GetFavList();
        }

        public Favourites(Models.Deals deals)
        {
            InitializeComponent();
            
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
    }
}

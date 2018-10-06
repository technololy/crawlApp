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
            BindingContext = GlobalStaticFields.Favs;
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

        }

        public Favourites(string fav)
        {
            InitializeComponent();
            GetFavList();
        }
    }
}

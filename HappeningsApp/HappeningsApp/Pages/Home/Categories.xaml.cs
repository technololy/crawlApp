using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class Categories : ContentPage
    {
        public Categories()
        {
            InitializeComponent();
            RefreshCategoryListView();

        }

        private void RefreshCategoryListView()
        {
            ListCategories.RefreshCommand = new Command(() =>
            {
                IntroPageViewModel ivmm = new IntroPageViewModel();
                ivmm.GetCategories();
                ivmm.GetDeals();
                ivmm.GetAll();
                this.BindingContext = ivmm;
                ListCategories.IsRefreshing = false;
            });
        }

       async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (ListCategories.SelectedItem == null)
            {
                return;
            }
            var cat = ListCategories.SelectedItem as Category;
            try
            {
                //adding this line because original code does not navigate using pushasync. only navigates using application.current.mainpage.navigation
                //i need navigation.pushasync cos i want to use the custom navigation. if i use application.current.mainpage, i dont get to use custom navgation
                //i need 



                await Navigation.PushAsync(new ListPage(cat));

            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.Search;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class Categories : ContentPage
    {
        IntroPageViewModel _ivm = new IntroPageViewModel();
        public Categories()
        {
            InitializeComponent();
            _ivm.PageTitle = "Categories";
            RefreshCategoryListView();
            //_ivm = new IntroPageViewModel();
           // _ivm.InitializeFromAPI();
            BindingContext = _ivm;


        }

        public Categories(IntroPageViewModel ivm)
        {
            InitializeComponent();
            RefreshCategoryListView();
            BindingContext = ivm;

        }

        protected override async void OnAppearing()
        {
            await _ivm.InitializeFromAPI();

            base.OnAppearing();
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


               // await Application.Current.MainPage.Navigation.PushAsync(new CategoryListing(), true);
                await Application.Current.MainPage.Navigation.PushAsync(new ListPage(cat),true);
                ((ListView)sender).SelectedItem = null;

            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }



        void Search_Tapped(object sender, System.EventArgs e)
        {

            Navigation.PushAsync(new SearchPage(), true);
        }

        void Profiles_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Profile());

        }


    }
}

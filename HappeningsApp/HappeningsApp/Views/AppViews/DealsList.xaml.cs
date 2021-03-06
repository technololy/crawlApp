﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class DealsList : ContentPage
    {

        public ObservableCollection<Activity> Activity;


        private Category cat;

        public DealsList()
        {
            InitializeComponent();
            RefreshListView();

        }

        //private void RefreshListView()
        //{
        //    dealsListview.RefreshCommand = new Command(()=>

        //    {
        //        IntroPageViewModel vm = new IntroPageViewModel();
        //        vm.GetDeals();
        //        this.BindingContext = vm;
        //        dealsListview.IsRefreshing = false;
        //    });
        //}

        private void RefreshListView()
        {
            dealsListview.RefreshCommand = new Command(() =>

            {
                IntroPageViewModel vm = new IntroPageViewModel();
                vm.GetDeals();
                vm.GetCategories();
                vm.GetAll();
                this.BindingContext = vm;
                dealsListview.IsRefreshing = false;
            });
        }

        public DealsList(Category cat)
        {
            InitializeComponent();
            try
            {
                this.cat = cat;

                GetCategoryByID(this.cat);


            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

            }

        }

        private async Task GetCategoryByID(Category cat)
        {

            var a = 1;
            //Activity = new ObservableCollection<Activity>();
            //Services.DealsService ds = new Services.DealsService();
            //using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            //{

            //    //Activity = await ds.GetAllByCategoryID(cat.CategoryID);
            //    var detail = await ds.GetAllByCategoryID2(cat.CategoryID);
            //    if (detail?.Count > 0)
            //    {
            //        ObservableCollection<NewDealsModel.Deal> deals = new ObservableCollection<NewDealsModel.Deal>(detail);

            //        this.BindingContext = deals;
            //        //this.dealsListview.ItemsSource = resp;
            //    }
            //    else
            //    {
            //        await DisplayAlert("", "No Content", "Back");
            //        await Application.Current.MainPage.Navigation.PopAsync(true);
            //        return;
            //    }
            //}


        }

        private async Task GetCategoryByIDOriginal(Category cat)
        {
            Activity = new ObservableCollection<Activity>();
            Services.DealsService ds = new Services.DealsService();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {

                //Activity = await ds.GetAllByCategoryID(cat.CategoryID);
                var detail = await ds.GetAllByCategoryID2Original(cat.CategoryID);
                if (detail?.Count > 0)
                {
                    ObservableCollection<Deals> deals = new ObservableCollection<Deals>(detail);

                    this.BindingContext = deals;
                    //this.dealsListview.ItemsSource = resp;
                }
                else
                {
                    await DisplayAlert("", "No Content", "Back");
                    await Application.Current.MainPage.Navigation.PopAsync(true);
                    return;
                }
            }


        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            //Navigation.PushModalAsync(new Deals());
        }

        private void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            if (dealsListview.SelectedItem is NewDealsModel.Deal selected)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));
                //Application.Current.MainPage.Navigation.PushAsync(new test.MyPage(selected));
            

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));
                //Navigation.PushAsync(new DetailPage(selected2));

            }

        }



        private void TapPlus_Tapped(object sender, EventArgs e)
        {
            try
            {
                var img = sender as Image;
                var item = img.BindingContext as NewDealsModel.Deal;
                //var item2 = img.BindingContext as Category;
                Navigation.PushAsync(new Favourites(item), true);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}


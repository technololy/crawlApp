using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;
using HappeningsApp.Pages.Home;

namespace HappeningsApp.Pages.Home
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }


        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as NewCategoryDetailModel.Deal;
            if (selected != null)
            {
                //CustomNavigationPage(new DetailPage(selected));
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));
               //Application.Current.MainPage.Navigation.PushAsync(new AllInnerDetail(selected),true);

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;

                 Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }

        public ListPage(Category cat)
        {
            InitializeComponent();
            try
            {

                Title = cat.CategoryName;
                
                GetCategoryByID(cat);


            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

            }

        }


        private async Task GetCategoryByID(Category cat)
        {

            DealsService ds = new DealsService();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {


                var detail = await ds.GetAllByCategoryID2(cat.CategoryID);
                if (detail?.Count > 0)
                {
                    ObservableCollection<NewCategoryDetailModel.Deal> deals = new ObservableCollection<NewCategoryDetailModel.Deal>(detail);

                    this.BindingContext = deals;
                 
                }
                else
                {
                    await DisplayAlert("", "No Content", "Ok");
                    //await Navigation.PopAsync(true);
                    return;
                }
            }


        }

        private void TapPlus_Tapped(object sender, EventArgs e)
        {
            try
            {
                var img = sender as Image;
                var item = img.BindingContext as NewCategoryDetailModel.Deal;
                //var item2 = img.BindingContext as Category;
                Application.Current.MainPage.Navigation.PushAsync(new Favorites(item), true);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}

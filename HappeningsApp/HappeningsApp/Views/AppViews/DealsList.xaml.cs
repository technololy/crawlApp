using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class DealsList : ContentPage
    {
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Deals());
        }

        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem==null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as HappeningsApp.Models.Deals;
            if (selected!=null)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }

        //IntroPageViewModel introPageViewMod;
        private Category cat;

        public DealsList()
        {
            InitializeComponent();
            //introPageViewMod = new IntroPageViewModel();
            //nearBy = new NearByViewModel();
            //BindingContext = introPageViewMod;

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
            }
          
        }

        private async void GetCategoryByID(Category cat)
        {
            Services.DealsService ds = new Services.DealsService();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                
                var resp = await ds.GetAllByCategoryID(cat.CategoryID);
                var rr = resp;
                if (resp?.Count > 0)
                {
                    this.BindingContext = resp;
                    //this.dealsListview.ItemsSource = resp;
                }
                else
                {
                   await DisplayAlert("", "No Content", "Ok");
                   await Application.Current.MainPage.Navigation.PopAsync(true);
                    return;
                }
            }
        }
    }
}

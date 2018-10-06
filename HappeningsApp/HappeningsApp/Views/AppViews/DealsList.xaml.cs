using System;
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
                LogService.LogErrors(log.ToString());

            }

        }

        private async void GetCategoryByID(Category cat)
        {
            Activity = new ObservableCollection<Activity>();
            Services.DealsService ds = new Services.DealsService();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {

                Activity = await ds.GetAllByCategoryID(cat.CategoryID);
            }
                
                if (Activity?.Count > 0)
                {
                    this.BindingContext = Activity;
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


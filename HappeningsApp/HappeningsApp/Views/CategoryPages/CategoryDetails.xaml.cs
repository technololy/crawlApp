using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.Views.AppViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.CategoryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryDetails : ContentPage
	{
		public CategoryDetails ()
		{
			InitializeComponent ();
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
                // Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));
                Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                //Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));
                Navigation.PushAsync(new DetailPage(selected2));

            }

        }

        public CategoryDetails(Category cat)
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
                    //this.dealsListview.ItemsSource = resp;
                }
                else
                {
                    await DisplayAlert("", "No Content", "Go Back");
                    await Application.Current.MainPage.Navigation.PopAsync(true);
                    return;
                }
            }


        }


        private async Task GetCategoryByIDOriginal(Category cat)
        {
            
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

        private void TapPlus_Tapped(object sender, EventArgs e)
        {
            try
            {
                var img = sender as Image;
                var item = img.BindingContext as NewCategoryDetailModel.Deal;
                //var item2 = img.BindingContext as Category;
               Application.Current.MainPage.Navigation.PushAsync(new Favourites.MyCreatedCollections(item), true);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesPage : ContentPage
	{
        CategoryViewModel categoryViewModel;
        public CategoriesPage ()
		{
			InitializeComponent ();
            RefreshCategoryListView();
            //BindingContext = categoryViewModel = new CategoryViewModel();

        }

        private void RefreshCategoryListView()
        {
            ListCategories.RefreshCommand = new Command(()=>
            {
                IntroPageViewModel ivmm = new IntroPageViewModel();
                ivmm.GetCategories();
                this.BindingContext = ivmm.CategfromAPI;
            });
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (ListCategories.SelectedItem == null)
            {
                return;
            }
            var cat = ListCategories.SelectedItem as Category;
            var category = ListCategories.SelectedItem as Models.Deals;
            Application.Current.MainPage.Navigation.PushAsync(new CategoryPages.CategoryDetails(cat));

        }
    }
}
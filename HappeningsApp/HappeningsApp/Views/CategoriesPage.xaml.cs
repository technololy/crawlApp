﻿using HappeningsApp.Models;
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
            //BindingContext = categoryViewModel = new CategoryViewModel();

        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
           if (ListCategories.SelectedItem==null)
            {
                return;
            }
            var cat = ListCategories.SelectedItem as Category;
            Application.Current.MainPage.Navigation.PushAsync(new HappeningsApp.Views.AppViews.DealsList(cat));
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (ListCategories.SelectedItem == null)
            {
                return;
            }
            var cat = ListCategories.SelectedItem as Category;
            Application.Current.MainPage.Navigation.PushAsync(new HappeningsApp.Views.AppViews.DealsList(cat));

        }
    }
}
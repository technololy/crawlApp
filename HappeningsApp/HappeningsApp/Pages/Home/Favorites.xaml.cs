using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class Favorites : ContentPage
    {
        MyCreatedCollectionViewModel mcvm;
        public Favorites()
        {
            InitializeComponent();
        }


        public Favorites(NewCategoryDetailModel.Deal deals)
        {
            InitializeComponent();

            mcvm = new MyCreatedCollectionViewModel
            {
                IsEnabled = true,
                CurrentlySelectedFavorite = deals
            };
            mcvm.GetListCollection();

            BindingContext = mcvm;


        }
    }
}

using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class MyDetailedFavorites : ContentPage
    {
        public MyDetailedFavorites(CollectionsResp favs)
        {
            InitializeComponent();
            this.BindingContext = favs;
            this.Title = "Favorites in " + favs.Name;

        }

        public MyDetailedFavorites()
        {
            InitializeComponent();
           

        }
    }
}

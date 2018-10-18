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
            if (favs.Details.Count==0)
            {
                favs.Name = "Nothing here yet";
            }
            this.BindingContext = favs;
            this.Title = "Saved in " + favs.Name;

        }

        public MyDetailedFavorites()
        {
            InitializeComponent();
           

        }
    }
}

using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class MyDetailedFavorites : ContentPage
    {
        public MyDetailedFavorites(CollectionsResp favs)
        {
            InitializeComponent();
            try
            {
                if (favs.Details.Count == 1 && string.IsNullOrEmpty(favs.Details[0].Name))
                {
                    favs.Details[0].Name = "Nothing here yet";
                }
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
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

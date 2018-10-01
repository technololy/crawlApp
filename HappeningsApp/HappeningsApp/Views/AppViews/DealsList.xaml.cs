using System;
using System.Collections.Generic;
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
           Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));

        }

        IntroPageViewModel introPageViewMod;
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
            this.cat = cat;
        }
    }
}

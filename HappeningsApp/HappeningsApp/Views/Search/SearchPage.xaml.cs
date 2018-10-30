using System;
using System.Linq;
using System.Collections.Generic;
using HappeningsApp.Services;
using HappeningsApp.Views.AppViews;
using Xamarin.Forms;
using HappeningsApp.Models;

namespace HappeningsApp.Views.Search
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = GlobalStaticFields.GetAll;
            LogService.LogErrorsNew(activity: "User landed on Search Page");

        }

        void Search_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
          
            var keyword = e.NewTextValue.ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dealsListview.IsVisible = false;

                dealsListview.ItemsSource = GlobalStaticFields.GetAll;
                return;
            }
            dealsListview.IsVisible = keyword.Length > 0 ? true : false;

            dealsListview.ItemsSource = GlobalStaticFields.GetAll.Where(c => c.Name.ToLower().Contains(keyword) ||
                                                                        c.Owner_Location.ToLower().Contains(keyword) ||
                                                                        c.Details.ToLower().Contains(keyword)
                                                                       );
        }

        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as HappeningsApp.Models.GetAll2.Deal;
            if (selected != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected));

            }
            else
            {
                var selected2 = dealsListview.SelectedItem as HappeningsApp.Models.Activity;
                Application.Current.MainPage.Navigation.PushAsync(new DetailPage(selected2));

            }

        }

        private void TapPlus_Tapped(object sender, EventArgs e)
        {
            try
            {

                var args = (TappedEventArgs)e;
                var m = args.Parameter as GetAll2.Deal;
                Deals md = new Deals();

                md.Name = m?.Name;
                md.Category = m?.Category;
                md.CategoryId = m?.CategoryId;
                md.Created = m.Created;
                md.Details = m?.Details;
                md.Expiration_Date = m.Expiration_Date;
                md.Id = Convert.ToInt32(m.Id);
                md.ImagePath = m?.ImagePath;
                md.Owner_Location = m?.Owner_Location;
                md.Owner = m?.Owner;
                md.Modified = m.Modified;
                 md.Owner_Id = Convert.ToInt32(m?.Owner_Id);
                md.type = m?.type;
                md.User_Id = m?.User_Id?.ToString();

                
                Navigation.PushAsync(new AppViews.Favourites(md), true);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}

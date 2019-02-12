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
            BindingContext = GlobalStaticFields.GetEvery;
            LogService.LogErrorsNew(activity: "User landed on Search Page");

        }

        void Search_TextChangedOld(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
          
            var keyword = e.NewTextValue.ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dealsListview.IsVisible = false;
                if (GlobalStaticFields.GetEvery!=null && GlobalStaticFields.AllDeals!=null
                
                    && GlobalStaticFields.CategoriesFromAPI!=null)
                {
                    dealsListview.ItemsSource = GlobalStaticFields.GetEvery;
                    categoryListView.ItemsSource = GlobalStaticFields.CategoriesFromAPI;
                    EveryListview.ItemsSource = GlobalStaticFields.AllDeals;

                }
                return;
            }
            dealsListview.IsVisible = keyword.Length > 0 ? true : false;
            categoryListView.IsVisible =  false;
            EveryListview.IsVisible =false;

            var every = GlobalStaticFields.GetEvery.Where(c => c.Name.ToLower().Contains(keyword) ||
                                                                        c.Owner_Location.ToLower().Contains(keyword) ||
                                                                        c.Details.ToLower().Contains(keyword)
                                                                       );
            categoryListView.ItemsSource = GlobalStaticFields.CategoriesFromAPI.Where(c => c.CategoryName.ToLower().Contains(keyword) ||
                                                                  c.Description.ToLower().Contains(keyword)
                                                                 );
           var allDeals = GlobalStaticFields.AllDeals.Where(c => c.Name.ToLower().Contains(keyword) ||
                                                                  c.Owner_Location.ToLower().Contains(keyword) ||
                                                                  c.Details.ToLower().Contains(keyword)
                                                                 );

            List<NewDealsModel.Deal> deals = new List<NewDealsModel.Deal>();

            //var everyToList = every.ToList();
            //var dealsToList = allDeals.ToList();
            foreach (var item in every)
            {
                deals.Add(item);
            }
            foreach (var item in allDeals)
            {
                deals.Add(item);
            }

            dealsListview.ItemsSource = deals;
            
            //EveryListview.ItemsSource = allDeals;

        }



        void Search_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

            var keyword = e.NewTextValue.ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dealsListview.IsVisible = false;
                if (GlobalStaticFields.GetAllSearch != null)
                {

                    dealsListview.ItemsSource = GlobalStaticFields.GetAllSearch;
           
                }
                return;
            }
            dealsListview.IsVisible = keyword.Length > 0 ? true : false;
        

            var everySearch = GlobalStaticFields.GetAllSearch.Where(c => c.Name.ToLower().Contains(keyword) ||
                                                                        c.Owner_Location.ToLower().Contains(keyword) ||
                                                                        c.Details.ToLower().Contains(keyword)
                                                                       );
       

            dealsListview.ItemsSource = everySearch;

         

        }


        void dealsListview_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (dealsListview.SelectedItem == null)
            {
                return;
            }
            var selected = dealsListview.SelectedItem as NewDealsModel.Deal;
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
                var m = args.Parameter as NewDealsModel.Deal;
                NewDealsModel.Deal md = new NewDealsModel.Deal();

                md.Name = m?.Name;
                md.Category = m?.Category;
                //md.CategoryId = m?.CategoryId;
                md.Created = m.Created;
                md.Details = m?.Details;
                md.Expiration_Date = m.Expiration_Date;
                md.Id = Convert.ToInt32(m.Id);
                md.ImagePath = m?.ImagePath;
                md.Owner_Location = m?.Owner_Location;
                md.Owner = m?.Owner;
                md.Modified = m.Modified;
                 md.Owner_Id = Convert.ToInt32(m?.Owner_Id);
                //md.type = m?.type;
                //md.User_Id = m?.User_Id?.ToString();

                
                Navigation.PushAsync(new AppViews.Favourites(md), true);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }
    }
}

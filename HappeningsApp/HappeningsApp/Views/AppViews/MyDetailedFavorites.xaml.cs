﻿using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.AppViews
{
    public partial class MyDetailedFavorites : ContentPage
    {
        MyCreatedCollectionViewModel mcvm;

        public CollectionsResp Collectz
        {
            get;
            set;
        }
        public MyDetailedFavorites(CollectionsResp favs)
        {
            InitializeComponent();
            mcvm = new MyCreatedCollectionViewModel();
            mcvm.UserFavs = favs;
            
            try
            {
                Collectz = favs;
                if (favs.Details.Count == 1 && string.IsNullOrEmpty(favs.Details[0].Name))
                {
                    favs.Details[0].Name = "Nothing here yet";
                }
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }
            //this.BindingContext = favs;
            this.BindingContext = mcvm.UserFavs;
            //this.BindingContext = null;
            this.Title = "Saved in " + favs.Name;

        }

        public MyDetailedFavorites()
        {
            InitializeComponent();
           
        }

       async void Delete_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(""))
            {
                try
                {
                    var s = (TappedEventArgs)e;
                    if (!(s.Parameter is Favourite castedObj))
                    {
                        return;
                    }
                    // mcvm = new MyCreatedCollectionViewModel();

                    var r = await mcvm.DeleteSavedFavorites(castedObj,
                                                            Collectz.Id.ToString()).ConfigureAwait(false);
                    if (r)
                    {
                       Collectz.Details.Remove(castedObj);
                        mcvm.UserFavs = null;
                        mcvm.UserFavs = Collectz;
                       
                        Device.BeginInvokeOnMainThread(()=>
                                                      MyFavList.ItemsSource = null                    
                                                     
                                                      );
                        Device.BeginInvokeOnMainThread(() => MyFavList.ItemsSource = mcvm.UserFavs.Details);
                        //Device.BeginInvokeOnMainThread(()=>this.BindingContext = mcvm.UserFavs);
                        // Collectz.Details.Remove(castedObj);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async()=> await  DisplayAlert("Info", "Not successful", "OK"));
                    }
                }
                catch (Exception ex)
                {
                    var log = ex;
                    LogService.LogErrors(ex.ToString());
                    //await DisplayAlert("Info", "Error", "OK");

                }
            }
        }
    }
}

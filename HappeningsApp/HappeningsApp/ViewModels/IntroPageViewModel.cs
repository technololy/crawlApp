﻿using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace HappeningsApp.ViewModels
{
    public class IntroPageViewModel//: //INotifyPropertyChanged
    {
      
        public ObservableCollection<Deals> dealsfromAPI { get; set; }
        public ObservableCollection<Category> CategfromAPI { get; set; }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public ObservableCollection<Collections> collections { get; set; }
     
        
        public IntroPageViewModel()
        {
            
            GetDeals();
            GetCategories();
           
        }
        

    

        public async Task<ObservableCollection<Deals>> GetDeals()
        {
            DealsService ds = new DealsService();

            dealsfromAPI = await ds.GetDeals();
            GlobalStaticFields.dealsfromAPI = dealsfromAPI;
            return dealsfromAPI;

        }


        private async Task GetCategories()
        {
           CategoriesService ds = new CategoriesService();

            CategfromAPI = await ds.GetCategories();
            GlobalStaticFields.CategoriesFromAPI = CategfromAPI;


        }
    }
}

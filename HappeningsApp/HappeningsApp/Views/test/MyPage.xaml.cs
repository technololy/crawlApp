using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HappeningsApp.Models;
using HappeningsApp.Services;
using MvvmHelpers;
using Xamarin.Forms;

namespace HappeningsApp.Views.test
{
    public partial class MyPage : ContentPage
    {
        public ObservableCollection<Grouping<string, Models.Deals>> GroupedDeals = new ObservableCollection<Grouping<string, Models.Deals>>();
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
        NewDealsModel.Deal AllNewDeals;

        public MyPage()
        {
            InitializeComponent();




            MockImageList mds = new MockImageList();
            var mock = mds.GetDealsTest();

            var mock1 = GroupListByDate(mock);
            BindingContext = mock1;
        }

        public MyPage(NewDealsModel.Deal AllDeals)
        {
            InitializeComponent();

            AllNewDeals = new NewDealsModel.Deal();

            AllNewDeals = AllDeals;
            //MapsAddress = AllNewDeals.Owner_Location;
            //hostOrEventName = AllNewDeals.Name;
            DaysOpenListView.ItemsSource = AllNewDeals.DaysOpen;
            // var returnedList = ReturnDaysAsListAndSetListViewItemSource(AllDeals.OpeningHours);
        
            BindingContext = AllNewDeals;

        }

        private ObservableCollection<Grouping<string, Deals>> GroupListByDate(ObservableCollection<Deals> deals)
        {
            var grp = from h in deals
                      orderby h?.Expiration_Date
                      group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                      select new Grouping<string, Deals>(ThisWeeksGroup.Key, ThisWeeksGroup);
            GroupedDeals.Clear();
            foreach (var g in grp)
            {
                GroupedDeals.Add(g);
            }
            return GroupedDeals;
        }
    }
}

using HappeningsApp.Models;
using HappeningsApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HappeningsApp.ViewModels
{
    public class IntroPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Deals> _dealsfromAPI;
        private ObservableCollection<Category> _categfromAPI;

        public bool IsEnabled { get; set; } = true;
        public ObservableCollection<Deals> DealsfromAPI
        {
            get => _dealsfromAPI;
            set
            {
                if (_dealsfromAPI != value)
                {
                    _dealsfromAPI = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Category> CategfromAPI 
        { 
            get => _categfromAPI; 
            set
            {
                if (_categfromAPI != value )
                {
                     _categfromAPI = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public ObservableCollection<Collections> Coollections { get; set; }
        public ObservableCollection<GetAll2.Deal> GgetAll { get; set; }

        public IntroPageViewModel()
        {

            //GetDeals();
            //GetCategories();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public async Task<ObservableCollection<Deals>> GetDeals()
        {
            DealsService ds = new DealsService();

            DealsfromAPI = await ds.GetDeals();
            GlobalStaticFields.dealsfromAPI = DealsfromAPI;
            return DealsfromAPI;

        }


        public async Task GetCategories()
        {
            CategoriesService ds = new CategoriesService();

            CategfromAPI = await ds.GetCategories();
            GlobalStaticFields.CategoriesFromAPI = CategfromAPI;


        }

        public async Task GetAll()
        {
            GetAllService ds = new GetAllService();

            var all = await ds.GetAll2();
            GgetAll = new ObservableCollection<GetAll2.Deal>(all);
            GlobalStaticFields.GetAll = GgetAll;

        }


        //private ObservableCollection<Grouping<string, GetAll2.Deal>> GroupListByDate()
        //{
        //    var grp = from h in ivm?.GgetAll
        //              orderby h?.Expiration_Date
        //              group h by h?.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
        //              select new Grouping<string, GetAll2.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
        //    GetAllGrouped.Clear();
        //    foreach (var g in grp)
        //    {
        //        GetAllGrouped.Add(g);
        //    }
        //    return GetAllGrouped;
        //}
    }
}

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
using System.Linq;

namespace HappeningsApp.ViewModels
{
    public class IntroPageViewModel: INotifyPropertyChanged //: BaseViewModel
    {
        private ObservableCollection<Deals> _dealsfromAPI;
        private ObservableCollection<NewDealsModel.Deal> _allDeals;
        private ObservableCollection<Category> _categfromAPI;
        private ObservableCollection<NewDealsModel.Deal> _getAllSearch;
        private ObservableCollection<NewDealsModel.Deal> _getThisWeek;
        MyCreatedCollectionViewModel mcvm;
        public ObservableCollection<Grouping<string, Models.Deals>> GroupedDeals = new ObservableCollection<Grouping<string, Models.Deals>>();
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();
        public ObservableCollection<Grouping<string, NewDealsModel.Deal>> GetEveryThingGrouped = new ObservableCollection<Grouping<string, NewDealsModel.Deal>>();
        public string PageTitle { get; set; }
        public bool IsEnabled { get; set; } = true;
        public ObservableCollection<Deals> DealsfromAPI
        {
            get => _dealsfromAPI;
            set
            {
                if (_dealsfromAPI != value)
                {
                    //SetProperty(ref _dealsfromAPI, value);
                    _dealsfromAPI = value;
                    OnPropertyChanged(nameof(DealsfromAPI));
                }
            }
        }

        public ObservableCollection<NewDealsModel.Deal> AllDeals
        {
            get => _allDeals;
            set
            {
                if (_allDeals != value)
                {
                    _allDeals = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Category> CategfromAPI
        {
            get => _categfromAPI;
            set
            {
                if (_categfromAPI != value)
                {
                   // SetProperty(ref _categfromAPI, value);
                    _categfromAPI = value;
                    OnPropertyChanged(nameof(CategfromAPI));
                }
            }
        }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public ObservableCollection<Collections> Coollections { get; set; }
        public ObservableCollection<GetAll2.Deal> GgetAll { get; set; }
        public ObservableCollection<NewDealsModel.Deal> GetEvery { get; set; }
        public ObservableCollection<Favourite> InitializedFav { get; set; }
        public ObservableCollection<NewDealsModel.Deal> GetThisWeek 
        { 
            get => _getThisWeek;
            set
                {
                if (_getThisWeek!=value)
                {
                    //SetProperty(ref _getThisWeek, value);

                    _getThisWeek = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<NewDealsModel.Deal> GetAllSearch
        {
            get => _getAllSearch;
            set
            {
                if (_getAllSearch != value)
                {
                    //SetProperty(ref _getAllSearch, value);

                    _getAllSearch = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public async Task<ObservableCollection<NewDealsModel.Deal>> GetDeals()
        {
            DealsService ds = new DealsService();

            AllDeals = await ds.GetDeals();
            GlobalStaticFields.AllDeals = AllDeals;
            return AllDeals;

        }
        public async Task<ObservableCollection<Deals>> GetDealsOriginal()
        {
            DealsService ds = new DealsService();

            DealsfromAPI = await ds.GetDealsOriginal();
            GlobalStaticFields.dealsfromAPI = DealsfromAPI;
            return DealsfromAPI;

        }


        public async Task InitializeFromAPI()
        {
            mcvm = new MyCreatedCollectionViewModel();

            await GetCategories();
            await GetDeals();

            await GetAll();
            await GetAllSearchFromNewModel();
            await GetAllThisWeek();
            await Initializefavourites();
            GroupListByDate();
           await mcvm.InitializeFavListNewUI();

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
            GetEvery = new ObservableCollection<NewDealsModel.Deal>(all);
            GlobalStaticFields.GetEvery = GetEvery;

        }


        public async Task GetAllSearchFromNewModel()
        {
            GetAllService ds = new GetAllService();

            var search = await ds.GetAllSearch();
            GetAllSearch = new ObservableCollection<NewDealsModel.Deal>(search);
            GlobalStaticFields.GetAllSearch = GetAllSearch;

        }


        public async Task GetAllOriginal()
        {
            GetAllService ds = new GetAllService();

            var all = await ds.GetAll2Original();
            GgetAll = new ObservableCollection<GetAll2.Deal>(all);
            GlobalStaticFields.GetAll = GgetAll;

        }

        public async Task GetAllThisWeek()
        {
            GetAllService ds = new GetAllService();

            var week = await ds.GetThisWeek();
            GetThisWeek = new ObservableCollection<NewDealsModel.Deal>(week);
            GlobalStaticFields.GetAllThisWeek = GetThisWeek;


        }

        public async Task Initializefavourites()
        {
            FavService ds = new FavService();

            var fav = await ds.GetFavorites();
            InitializedFav = new ObservableCollection<Favourite>(fav);
            GlobalStaticFields.FavoriteList = InitializedFav;


        }

        private ObservableCollection<Grouping<string, NewDealsModel.Deal>> GroupListByDate()
        {
            try
            {
                if (GetEvery == null)
                {
                    return GetEveryThingGrouped;
                }
                var grp = from h in GetThisWeek
                          orderby h?.type
                          group h by h?.type into ThisWeeksGroup
                          select new Grouping<string, NewDealsModel.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);
                GetEveryThingGrouped.Clear();
                foreach (var g in grp)
                {
                    GetEveryThingGrouped.Add(g);
                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
                MyToast.DisplayToast(Xamarin.Forms.Color.Red, "Slight error occured parsing response");
            }

            return GetEveryThingGrouped;
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

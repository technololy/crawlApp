using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;

namespace HappeningsApp.ViewModels
{


    public class FavViewModel : INotifyPropertyChanged
    {
        private CollectionsModelResp _collectionz;

        public Deals CurrentlySelectedFav { get; set; }
        public NewDealsModel.Deal CurrentlySelectedFavorite { get; set; }

        private ObservableCollection<CollectionsResp> _collectionsList = new ObservableCollection<CollectionsResp>();

        public ObservableCollection<Models.FavoriteModel> MyFavs { get; set; }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public bool WasNewFavAddedOk { get; set; } = false;
        public CollectionsModelResp Collectionz
        {
            get => _collectionz;
            set
            {
                if (_collectionz != value)
                {
                    _collectionz = value;
                    OnPropertyChanged(nameof(Collectionz));
                }

            }
        }

        public ObservableCollection<CollectionsResp> CollectionsList
        {
            //get
            //{
            //    return _collectionsList;
            //}
            //set
            //{
            //    if (_collectionsList != value)
            //    {
            //        _collectionsList = value;
            //        OnPropertyChanged(nameof(CollectionsList));

            //    }
            //}
            get;set;
        } = new ObservableCollection<CollectionsResp>();

        public ObservableCollection<Favourite> FavList { get; set; }

        public bool IsEnabled { get; set; }
        public Command GetFavCommand { get; set; }
        public Command AddNewCollectionCommand { get; set; }
        public string NewCollectionName
        {
            get;
            set;
        }

        public string NewCollectionNick
        {
            get;
            set;
        }
        public FavViewModel()
        {
            GetFavCommand = new Command(GetSelectedFav);
            //AddNewCollectionCommand = new Command(AddNewCollection);
        }

        internal void AddNewTest()
        {
            CollectionsList.Add(new CollectionsResp{Name=Guid.NewGuid().ToString(),NickName=Guid.NewGuid().ToString()});
        }

        public async Task<bool> AddNewCollection()
        {
            WasNewFavAddedOk = false;

            using (UserDialogs.Instance.Loading(""))
            {
                CollectionService cs = new CollectionService();
                bool result = await cs.CreateNewCollection(NewCollectionName, NewCollectionNick);
                if (result)
                {
                    //refresh list

                    UserDialogs.Instance.Toast(MyToast.DisplayToast(Color.Green,"Successful..."));
                    //await GetFavs();
                    return WasNewFavAddedOk = true;

                }
                else
                {
                    UserDialogs.Instance.Toast(MyToast.DisplayToast(Color.OrangeRed, "Not Successful..."));
                    return WasNewFavAddedOk = false;

            
                }
            }
        
        }

        private  void GetSelectedFav(object obj)
        {
            CollectionsResp ctl = obj as CollectionsResp;
            UpdateCollections(ctl);
           
        }

        private async void UpdateCollections(CollectionsResp ctl)
        {
            try
            {
                using (UserDialogs.Instance.Loading("Adding to "+ctl.Name))
                {
                    var cs = this.CurrentlySelectedFavorite;
                    ctl.Details.Add(
                    new Favourite { Name = CurrentlySelectedFavorite.Name, 
                        Address = CurrentlySelectedFavorite.Owner_Location, 
                        Description = CurrentlySelectedFavorite.Details, 
                        Id = CurrentlySelectedFavorite.Id });
                    CollectionService cserv = new CollectionService();
                    var isCollAdded = await cserv.UpdateCollectionWithDetails(ctl);
                    if (isCollAdded)
                    {
                        UserDialogs.Instance.Alert("Great!! Added to " + ctl.Name, "Info", "OK");
                    }

                    else
                    {
                        UserDialogs.Instance.Alert("Unsuccessfull", "Error", "OK");

                    }
                }
             
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }
         
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<FavoriteModel> GetMyFavs(string username)
        {
            MyFavs = new ObservableCollection<Models.FavoriteModel>();
            CollectionService cs = new CollectionService();

            return MyFavs;
        }

        public async Task<CollectionsModelResp> GetFavs()
        {
            FavService fs = new FavService();
            CollectionService cs = new CollectionService();

            Collectionz = await cs.GetUserFavsNew();


            GlobalStaticFields.AllCollections = Collectionz;
            return Collectionz;
        }



        public async Task<ObservableCollection<CollectionsResp>> GetListCollection()
        {
            FavService fs = new FavService();
            CollectionService cs = new CollectionService();

            //var fav = await cs.GetUserFavsNew();
            CollectionsList =await cs.GetUserListCollection();

            //CollectionsList = fav.Collections;
            //if (fav.Collections?.Count>0)
            //{
               
            //    ObservableCollection<CollectionsResp> ff  = new ObservableCollection<CollectionsResp>(fav.Collections);
            //    foreach (var item in ff)
            //    {
            //        CollectionsList.Add(item);

            //    }
            //   //OnPropertyChanged(nameof(CollectionsList));
               
            //}

            GlobalStaticFields.CollectionList = CollectionsList;
            return CollectionsList;
        }

        public async Task InitializeFavListNewUI()
        {
           

            FavService fs = new FavService();


            var myCollectn = await fs.GetFavorites();
            if (myCollectn.Any())
            {
                FavList = new ObservableCollection<Favourite>(myCollectn);
              
            }
            else
            {
              
            }



            GlobalStaticFields.FavoriteList = FavList;

        }
    }
}

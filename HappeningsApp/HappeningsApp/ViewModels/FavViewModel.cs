using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<CollectionsResp> _collectionsList;

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
            get => _collectionsList;
            set 
            {
                if(_collectionsList != value)
                {
                    _collectionsList = value;
                    OnPropertyChanged();

                }
            }
        }
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
                    await GetFavs();
                    return WasNewFavAddedOk = true;

                }
                else
                {
                    UserDialogs.Instance.Toast(MyToast.DisplayToast(Color.OrangeRed, "Not Successful..."));
                    return WasNewFavAddedOk = false;

            
                }
            }
        
        }

        private void GetSelectedFav(object obj)
        {
            CollectionsResp ctl = obj as CollectionsResp;
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

            CollectionsList = await cs.GetUserListCollection();


            GlobalStaticFields.AllCollections = Collectionz;
            return CollectionsList;
        }


    }
}

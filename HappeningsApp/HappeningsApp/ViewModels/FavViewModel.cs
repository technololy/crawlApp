using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;
using Xamarin.Forms;

namespace HappeningsApp.ViewModels
{


    public class FavViewModel : INotifyPropertyChanged
    {
        private CollectionsModelResp _collectionz;

        public ObservableCollection<Models.FavoriteModel> MyFavs { get; set; }
        public ObservableCollection<FavoriteModel> Favs { get; set; }
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
        public bool IsEnabled { get; set; }
        public Command GetFavCommand { get; set; }


        public FavViewModel()
        {
            GetFavCommand = new Command(GetSelectedFav);
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


    }
}

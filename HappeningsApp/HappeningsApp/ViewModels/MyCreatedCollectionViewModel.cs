using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services;

namespace HappeningsApp.ViewModels
{
    public class MyCreatedCollectionViewModel: INotifyPropertyChanged
    {
        public bool IsEnabled { get; set; }

        private ObservableCollection<CollectionsResp> _CollectionsList;

        public ObservableCollection<CollectionsResp> CollectionsList
        {
            get => _CollectionsList;
            set
            {
                if (_CollectionsList != value)
                {
                    _CollectionsList = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MyCreatedCollectionViewModel()
        {
           // GetListCollection();
        }

        public async Task GetListCollection()
        {
          
            CollectionService cs = new CollectionService();

          
            var myCollectn = await cs.GetUserListCollection();
            if (myCollectn.Any())
            {
                CollectionsList = new ObservableCollection<CollectionsResp>(myCollectn);
            }


            GlobalStaticFields.CollectionList = CollectionsList;
            
        }

    }
}

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
    public class MyCreatedCollectionViewModel : INotifyPropertyChanged
    {
        public bool IsEnabled { get; set; }
        public bool WasNewFavAddedOk { get; set; } = false;
        public NewCategoryDetailModel.Deal CurrentlySelectedFavorite { get; set; }
        public NewDealsModel.Deal CurrentlySelectedFavoriteDeal { get; set; }
        bool fake = true;

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
        public Deals CurrentlySelectedFav { get; set; }
        public Command GetFavCommand { get; set; }
        public bool ActivityRunning
        {
            get => _activityRunning;
            set
            {
                if (_activityRunning != value)
                {
                    _activityRunning = value;
                    OnPropertyChanged();

                }
            }
        }

        public bool ShowTips
        {
            get => _showTips;
            set

            {
                if (_showTips != value)
                {
                    _showTips = value;
                    OnPropertyChanged();

                }

            }
        }



        private ObservableCollection<CollectionsResp> _CollectionsList;
        private bool _activityRunning;

        public ObservableCollection<CollectionsResp> CollectionsList
        {
            get => _CollectionsList;
            set
            {
                if (_CollectionsList != value)
                {
                    _CollectionsList = value;

                    ActivityRunning = false;
                    ShowTips = true;
                    OnPropertyChanged();
                }
            }
        }

        private CollectionsResp _userFavs;
        private bool _showTips;

        public CollectionsResp UserFavs
        {
            get => _userFavs;
            set
            {
                if (_userFavs != value)
                {
                    _userFavs = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //private void GetSelectedFav(object obj)
        //{
        //    CollectionsResp ctl = obj as CollectionsResp;
        //    UpdateCollections(ctl);

        //}

        public MyCreatedCollectionViewModel()
        {
            // GetListCollection();
            GetFavCommand = new Command(GetSelectedFav);

        }
        private void GetSelectedFav(object obj)
        {
            CollectionsResp ctl = obj as CollectionsResp;
            UpdateCollections(ctl);

        }

        public async Task<bool> DeleteCollection(CollectionsResp resp)
        {
            CollectionService cserv = new CollectionService();
            var result = await cserv.DeleteCollection(resp).ConfigureAwait(false);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private async Task UpdateCollections(CollectionsResp ctl)
        {
            try
            {
                //if (await UserDialogs.Instance.ConfirmAsync("Confirm", $"Are you sure you want to add{CurrentlySelectedFav.Name} to {ctl.Name}? ", "Yes", "No"))
                if(fake)
                {
                    using (UserDialogs.Instance.Loading("Adding to " + ctl.Name))
                    {
                        var cs = this.CurrentlySelectedFavorite;
                        ctl.Details.Add(new Favourite
                        {
                            Name = CurrentlySelectedFavorite.Name,
                            Address = CurrentlySelectedFavorite.Owner_Location,
                            Description = CurrentlySelectedFavorite.Details,
                            //Id = CurrentlySelectedFavorite.Id, 
                            ImageURL = CurrentlySelectedFavorite.ImagePath
                        });
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

              

            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }

        }

        public async Task UpdateFavoriteList(CollectionsResp ctl)
        {
            try
            {
                //if (await UserDialogs.Instance.ConfirmAsync("Confirm", $"Are you sure you want to add{CurrentlySelectedFav.Name} to {ctl.Name}? ", "Yes", "No"))
                if (fake)
                {
                    using (UserDialogs.Instance.Loading("Adding to favorites"))
                    {
                        var cs = this.CurrentlySelectedFavorite;
                        ctl.Details.Add(new Favourite
                        {
                            Name = CurrentlySelectedFavorite.Name,
                            Address = CurrentlySelectedFavorite.Owner_Location,
                            Description = CurrentlySelectedFavorite.Details,
                            //Id = CurrentlySelectedFavorite.Id, 
                            ImageURL = CurrentlySelectedFavorite.ImagePath
                        });
                        CollectionService cserv = new CollectionService();
                        var isCollAdded = await cserv.UpdateCollectionWithDetails(ctl);
                        if (isCollAdded)
                        {
                            UserDialogs.Instance.Alert("Great!! Added "+ CurrentlySelectedFavorite.Name+ " to favorites", "Info", "OK");
                        }

                        else
                        {
                            UserDialogs.Instance.Alert("Unsuccessfull", "Error", "OK");

                        }
                    }
                }



            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }

        }
        public async Task GetListCollection()
        {
            ActivityRunning = true;

            CollectionService cs = new CollectionService();


            var myCollectn = await cs.GetUserListCollection();
            if (myCollectn.Any())
            {
                CollectionsList = new ObservableCollection<CollectionsResp>(myCollectn);
                ShowTips = false;
            }
            else
            {
                ShowTips = true;
            }
            ActivityRunning = false;


            GlobalStaticFields.CollectionList = CollectionsList;

        }

        public async Task InitializeFavListNewUI()
        {
            ActivityRunning = true;

            CollectionService cs = new CollectionService();


            var myCollectn = await cs.GetUserListCollection();
            if (myCollectn.Any())
            {
                CollectionsList = new ObservableCollection<CollectionsResp>(myCollectn);
                ShowTips = false;
            }
            else
            {
                ShowTips = true;
            }
            ActivityRunning = false;


            GlobalStaticFields.CollectionList = CollectionsList;

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

                    UserDialogs.Instance.Toast(MyToast.DisplayToast(Color.Green, "Successful..."));
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



        public async Task<bool> DeleteSavedFavorites(Favourite ctl, string categoryID)
        {
            bool response = false;
            try
            {
                //if (await UserDialogs.Instance.ConfirmAsync("Confirm","Are you sure","Yes","No"))
                if(fake)
                {
                    using (UserDialogs.Instance.Loading("Deleting...."))
                    {
                        // var cs = this.CurrentlySelectedFav;
                        // ctl.Add(new Favourite { Name = CurrentlySelectedFav.Name, Address = CurrentlySelectedFav.Owner_Location, Description = CurrentlySelectedFav.Details, Id = CurrentlySelectedFav.Id, ImageURL = CurrentlySelectedFav.ImagePath });
                        CollectionService cserv = new CollectionService();
                        var isDeleted = await cserv.DeleteCollectionWithDetails(ctl, categoryID);
                        response = isDeleted;
                    }
                    //return response;
                }

            }


            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
            }

            return response;
        }


    }
}

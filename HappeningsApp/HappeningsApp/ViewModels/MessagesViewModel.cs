using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.Services;

namespace HappeningsApp.ViewModels
{
    public class MessagesViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Pushresp> myMessages { get;set;}
        private ObservableCollection<Pushresp> _Notif;

        public ObservableCollection<Pushresp> Notif
        {
            get => _Notif;
            set
            {
                if (_Notif != value)
                {
                    _Notif = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MessagesViewModel()
        {
            GetMessages();
        }

        private async Task GetMessages()
        {
            using (UserDialogs.Instance.Loading())
            {
                MessageServices msgServ = new MessageServices();
                var msg = await msgServ.GetPushMessages();
                if (msg?.Count > 0)
                {

                    //item.Pushrespx;
                    myMessages = new ObservableCollection<Pushresp>(msg);
                    Notif = new ObservableCollection<Pushresp>(msg);




                }
            }
             
         

        }
    }
}

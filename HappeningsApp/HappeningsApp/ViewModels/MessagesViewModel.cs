using System;
using System.Collections.Generic;
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
        public string PageTitle { get; set; }

        public bool IsImageEnabled { get; set; }
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


    //        ObservableCollection<Pushresp> pushRep = new ObservableCollection<Pushresp>() 
    //        {

    //        new Pushresp {Message="Image files can be added to each application project and referenced " +
    //        	"from Xamarin.Forms shared code. This method of distributing images is required when images " +
    //        	"are platform-specific, such as when using different resolutions on different platforms, " +
    //        	"or slightly different designs", Picture="",Status="Hello01 Subject"} ,
    //        new Pushresp {Message="Test02 messages Image files can be added to each application project and " +
    //        	"referenced from Xamarin.Forms shared code. This method of distributing images is required " +
    //        	"when images are platform-specific, " +
    //        	"such as when using different resolutions on different platforms, or slightly different designs", 
    //Picture="",Status="Hello02 Subject" } ,
            //new Pushresp {Message="Test03 messages Image files can be added to each" +
            //	" application project and referenced from Xamarin.Forms shared code. This method of " +
            //	"distributing images is required when images are " +
            //	"platform-specific, such as when using different resolutions on different platforms," +
            //	" or slightly different designs", Picture="",Status="Hello03 Subject" } ,
            //};

            //myMessages = pushRep;
            //Notif = pushRep;

            //return;
            using (UserDialogs.Instance.Loading())
            {
                MessageServices msgServ = new MessageServices();
                var msg = await msgServ.GetPushMessages();
                if (msg?.Count > 0)
                {

                    //item.Pushrespx;
                    myMessages = new ObservableCollection<Pushresp>(msg);
                    Notif = new ObservableCollection<Pushresp>(msg);
                    foreach (var item in Notif)
                    {
                        if (string.IsNullOrEmpty(item?.Picture))
                        {
                            item.IsImageEnabled = false;
                        }
                        else
                        {
                            item.IsImageEnabled = true;
                        }
                        
                    }



                }
            }
             
         

        }
    }
}

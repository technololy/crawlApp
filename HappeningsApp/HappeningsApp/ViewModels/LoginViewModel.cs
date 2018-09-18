using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services.FaceBook;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace HappeningsApp.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        public LoginViewModel()
        {
        }
        public UserInfo User
        {
            get;
            set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private FaceBookProfile _facebookProfile;

        public FaceBookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            FaceBookService faceBookService = new FaceBookService();
            //var facebookServices = customerRestService.FacebookServices();

            FacebookProfile = await faceBookService.GetFacebookProfileAsync(accessToken);
            if ((FacebookProfile!=null))
            {
                var customerInfo = new UserInfo()
                {
                    EmailAddress = FacebookProfile.Email,
                    Firstname = FacebookProfile.FirstName,
                    Lastname = FacebookProfile.LastName,
                    Gender = FacebookProfile.Gender

                };
             


            }
            else
            {
                // .Alert("Facebook authentication did not pass", "Error", "ok");
                UserDialogs.Instance.Alert("Facebook authentication did not pass", "Error", "ok");
                return;
            }
        }
    }
}

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
            User = new UserInfo();
        }
        public UserInfo User
        {
            get;
            set;
        }
        public bool IsSuccess
        {
            get;
            set;
        }
        public string accessToken { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        internal void Register()
        {
            using (UserDialogs.Instance.Loading("Registration you.."))
            {
                var reg = new Registeration()
                {
                    Email=User.EmailAddress,
                    Password=User.Password,
                    ConfirmPassword=User.ConfirmPin,
                    UserName=User.Username
                };
            }
        }

        internal void ExtractAccessToken(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {

                string[] sep = { "access_token=", "&expires_in=" };
                var urlSplit = url.Split(sep, StringSplitOptions.None);

                accessToken = urlSplit[1].ToString();

            }
            else
            {

                accessToken = string.Empty;
            }
        }

        internal async Task CallProviderLoginAPI()
        {
            await Task.Delay(5000);
        }

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

        internal async Task CallFaceBookGraphAPI(string accessToken)
        {
            IsSuccess = false;
            FaceBookService faceBookService = new FaceBookService();
            //var facebookServices = customerRestService.FacebookServices();

            FacebookProfile = await faceBookService.GetFacebookProfileAsync(accessToken);
            if (FacebookProfile != null)
                IsSuccess = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void SetFacebookUserProfileAsync()
        {
          
            if ((FacebookProfile!=null))
            {
                User = new UserInfo()
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

            }
        }
    }
}

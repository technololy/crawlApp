using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HappeningsApp.Models;
using HappeningsApp.Services.FaceBook;
using Xamarin.Forms;
using Acr.UserDialogs;
using HappeningsApp.Services;
using HappeningsApp.Services.LoginSignUp;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Xamarin.Auth;
using System.Diagnostics;
using HappeningsApp.OAuth;
using System.Linq;

namespace HappeningsApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        Account account;
        AccountStore store;
        public INavigation nav { get; set; }
        public Command OnGoogleButtonCommand { get; set; }
        public LoginViewModel()  
            //public LoginViewModel(INavigation _nav)
        {
            //this.nav = _nav;
            User = new UserInfo();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            //OnGoogleButtonCommand = new Command(OnGoogleButtonClick);
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

        internal bool IsRegisterationDetailsValid()
        {
            User.Firstname = User.Username = User.EmailAddress;
            if (string.IsNullOrEmpty(User.EmailAddress) || string.IsNullOrEmpty(User.Firstname) || string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.ConfirmPin))
            {
                RegisterationError = "Please make sure you enter each and every field";
                return false;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(User.EmailAddress.Trim());
            if (!match.Success)
            {
                RegisterationError = "Email is invalid";
                return false;
            }
            if (User.Password.Trim() != User.ConfirmPin.Trim())
            {
                RegisterationError = "Passwords do not match";
                return false;
            }

            return true;

        }

        public string accessToken { get; set; }

        internal async Task<bool> GetTokenFromAPI()
        {
            
            IsSuccess = false;
            var tk = await APIService.GetToken(User).ConfigureAwait(false);
            if (tk.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var res = await tk.Content.ReadAsStringAsync();
                var cont = JsonConvert.DeserializeObject<TokenResponse>(res);
                GlobalStaticFields.Token = cont.access_token;
                IsSuccess = true;
                
                return IsSuccess;

            }
            else
            {
                var res =await tk.Content.ReadAsStringAsync();
                RegisterationError = res;
                return IsSuccess;
            }
        }

        internal async Task<bool> ChangePassword()
        {
            var chgPaswd = new ChangePassword()
            {
                OldPassword=User.Password,
                NewPassword=User.NewPassword,
                ConfirmPassword=User.ConfirmNewPassword
            };
            var resp =await LoginSignUp.ChangePassword(chgPaswd);
            return resp;
        }

        public void PersistUserDetails()
        {
            Application.Current.Properties["IsUserLoggedOn"] = true;
            Application.Current.Properties["username"] = User.Username;
            Application.Current.Properties["password"] = User.Password;
            Application.Current.SavePropertiesAsync();


        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal async Task<bool> Register()
        {
            IsSuccess = false;
            using (UserDialogs.Instance.Loading(".."))
            {
                var reg = new Registeration()
                {
                    Email = User.EmailAddress,
                    Password = User.Password,
                    ConfirmPassword = User.ConfirmPin,
                    UserName = User.Username
                };

                //APIService api = new APIService();
                //var a = await APIService.Post<Registeration>(reg, "api/Account/register");
                //var aa = await LoginSignUp.RegisterLocal(reg);
                var regaa = await LoginSignUp.RegisterLoco(reg);
                //if (a.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    var result =await a.Content.ReadAsStringAsync();
                //}
                //else
                //{
                //    var result = await a.Content.ReadAsStringAsync();
                
                if (regaa.Message != null)
                {
                    if (regaa.Message.ToLower().Contains("success"))
                    {
                        IsSuccess = true;

                    }
                    else
                    {
                        IsSuccess = false;
                        RegisterationError = regaa.Message;
                        return IsSuccess;
                    }
                }

                //if (aa == null)
                //{
                //    UserDialogs.Instance.Alert("Info", "Error", "OK"); 
                //    return IsSuccess;

                //}
                //if (aa.ContainsKey(false))
                //{
                //    UserDialogs.Instance.Alert("Info", "Error during registration", "OK");
                //    return IsSuccess;

                //}
                //else if(aa.ContainsKey(true))
                //{
                //    //UserDialogs.Instance.Alert("Success", "Registration successful", "OK");
                //    IsSuccess = true;
                //    return IsSuccess;

                //}
                //}
                return IsSuccess;
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

        public string RegisterationError { get; set; }

        public async Task CallFaceBookGraphAPI(string accessToken)
        {
            IsSuccess = false;
            FaceBookService faceBookService = new FaceBookService();
            //var facebookServices = customerRestService.FacebookServices();

            FacebookProfile = await faceBookService.GetFacebookProfileAsync(accessToken);
            if (FacebookProfile != null)
                IsSuccess = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async void SetFacebookUserProfileAsync()
        {
            await Task.Delay(1);
            if ((FacebookProfile != null))
            {
                User = new UserInfo()
                {
                    EmailAddress = FacebookProfile.Email,
                    Firstname = FacebookProfile.FirstName,
                    Lastname = FacebookProfile.LastName,
                    Password = "Qwe123!",
                    ConfirmPin = "Qwe123!",
                    Username = FacebookProfile.Email

                };


            }
            else
            {
                // .Alert("Facebook authentication did not pass", "Error", "ok");
                UserDialogs.Instance.Alert("Facebook authentication did not pass", "Error", "ok");

            }
        }



       public bool OnGoogleButtonClick()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Xamarin.Forms.Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;
            
            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
            return true;
        }

        public bool OnGoogleButtonClickShared()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Xamarin.Forms.Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompletedShared;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
            return true;
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }

                await store.SaveAsync(account = e.Account, Constants.AppName);
                //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
                MyToast t = new MyToast();
                UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green,"Successful google login"));
                User.Username = user.Email;
                User.Password = user.Email;
            
            }
            else
            {
                MyToast t = new MyToast();
                UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
            }
        }

        async void OnAuthCompletedShared(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompletedShared;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }

                await store.SaveAsync(account = e.Account, Constants.AppName);
                //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
                MyToast t = new MyToast();
                UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful google login"));
                User.Username = user.Email;
                User.Password = user.Email;

            }
            else
            {
                MyToast t = new MyToast();
                UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }


    }
}

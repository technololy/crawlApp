using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.OAuth;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Auth;
using Xamarin.Forms;

namespace HappeningsApp.Pages
{
    public partial class SignInUp : ContentPage
    {
        Account account;
        AccountStore store;
        public string accessToken { get; set; }
        public string SocialMedia { get; set; }
        LoginViewModel lvm = new LoginViewModel();
        public SignInUp()
        {
            InitializeComponent();
            store = AccountStore.Create();
            try
            {
                account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }



        async void Logon_Tapped(object sender, System.EventArgs e)
        {
           await Navigation.PushAsync(new Pages.Onboarding.LoginEmail());

        }



        private void OAuth_Clicked(object sender, EventArgs e)
        {
            try
            {

                var socialMediaButton = (Button)sender;
                var socialMediaName = socialMediaButton.Text;
                string clientId = null;
                string redirectUri = null;
                string authorizeURL = null;
                string accessTokenURL = null;
                OAuth2Authenticator authenticator_ = null;

                SocialMedia = socialMediaName.ToLower().Contains("google") ? "google" : "facebook";


                switch (SocialMedia)
                {
                    case "facebook":
                        authenticator_ = new OAuth2Authenticator

                        (
                            clientId: Constants.fbClientID,
                            scope: "email",
                            authorizeUrl: new Uri(Constants.FacebookOAuthURL),
                            redirectUrl: new Uri(Constants.redirectURI)

                            )


                        ;
                        break;

                    case "google":

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


                        authorizeURL = Constants.AuthorizeUrl;
                        accessTokenURL = Constants.AccessTokenUrl;

                        authenticator_ = new OAuth2Authenticator(
                      clientId,
                      null,
                      Constants.Scope,
                      new Uri(Constants.AuthorizeUrl),
                      new Uri(redirectUri),
                      new Uri(Constants.AccessTokenUrl),
                      null,
                      true);
                        break;
                    default:
                        break;
                }




                authenticator_.Completed += OnAuthCompleted;
                authenticator_.Error += OnAuthError;

                AuthenticationState.Authenticator = authenticator_;

                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(authenticator_);
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
            }

        }


        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {


            try
            {
                using (UserDialogs.Instance.Loading(""))
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
                        OAuth2Request oAuth2Request = null;
                        string returnedJson = null;
                        // If the user is authenticated, request their basic user data from Google
                        // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                        switch (SocialMedia)
                        {
                            case "facebook":
                                oAuth2Request = new OAuth2Request("GET", new Uri(Constants.graphAPI), null, e.Account);
                                break;

                            case "google":
                                oAuth2Request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);

                                break;
                            default:
                                break;
                        }
                        //var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                        var response = await oAuth2Request.GetResponseAsync();
                        if (response != null)
                        {
                            // Deserialize the data and store it in the account store
                            // The users email address will be used to identify data in SimpleDB
                            string userJson = await response.GetResponseTextAsync();
                            switch (SocialMedia)
                            {
                                case "google":
                                    try
                                    {
                                        user = JsonConvert.DeserializeObject<User>(userJson);
                                        lvm.User.Username = user.Email;
                                        lvm.User.Password = user.Email;
                                        lvm.User.EmailAddress = user.Email;
                                        lvm.User.ConfirmPin = user.Email;
                                    }
                                    catch (Exception ex)
                                    {
                                        var log = ex;
                                    }
                                    break;

                                case "facebook":
                                    try
                                    {
                                        var fbuser = JsonConvert.DeserializeObject<FaceBookProfile>(userJson);

                                        //var fbuser = JsonValue.Parse(response.GetResponseText());
                                        lvm.User.Username = fbuser.Email;
                                        lvm.User.Password = fbuser.Email;
                                        lvm.User.EmailAddress = fbuser.Email;
                                        lvm.User.ConfirmPin = fbuser.Email;
                                    }
                                    catch (Exception ex)
                                    {
                                        var log = ex;
                                    }

                                    break;
                                default:
                                    break;
                            }

                        }

                        if (account != null)
                        {
                            store.Delete(account, Constants.AppName);
                        }

                        await store.SaveAsync(account = e.Account, Constants.AppName);
                        //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
                        MyToast t = new MyToast();
                        UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful login"));
                        GlobalStaticFields.Username = lvm.User.Username;
                        var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

                        if (tk)
                        {
                            lvm.PersistUserDetails();
                            //Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                            Device.BeginInvokeOnMainThread
                             (
                          async  () => await Application.Current.MainPage.Navigation.PushAsync(new AppLanding())
                            );
                        }
                        else
                        {
                            var reg = await lvm.Register().ConfigureAwait(false);

                            if (reg)
                            {
                                //lvm.PersistUserDetails();
                                Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                                //Device.BeginInvokeOnMainThread
                                //  (
                                //async () => await Application.Current.MainPage.Navigation.PushAsync(new AppLanding())
                                //);

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread
                                      (() =>
                                       UserDialogs.Instance.Toast(t.ShowMyToast(Color.OrangeRed, $"Unsuccessful. {lvm.RegisterationError} ")));

                            }
                        }

                    }
                    else
                    {
                        MyToast t = new MyToast();
                        UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, $"Unsuccessful {SocialMedia} login"));
                    }


                }
            }
            catch (Exception ex)
            {
                var log = ex;
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

        void SignUp_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Onboarding.SignUpEmail());
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
        DisplayAlert("hello", "hi", "ok");
        }
    }
}

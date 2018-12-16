using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.OAuth;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.LoginSignUp;
using HappeningsApp.Views.Settings;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Auth;

//using UIKit;
using Xamarin.Forms;

namespace HappeningsApp.Views
{
    public partial class LoggedOn : ContentPage
    {
        Account account;
        AccountStore store;
        public string accessToken { get; set; }
        public string SocialMedia { get; set; }
        LoginViewModel lvm = new LoginViewModel();
        async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var activity = "FaceBook Login";
            try
            {
                var url = e.Url;
                using (UserDialogs.Instance.Loading("Please wait..."))
                {
                    lvm.ExtractAccessToken(url);
                    if (!string.IsNullOrEmpty(lvm.accessToken))
                    {
                        await lvm.CallFaceBookGraphAPI(lvm.accessToken);
                        if (lvm.IsSuccess)
                        {
                            using (UserDialogs.Instance.Loading("Facebook Authenticated.Now logging on.."))
                            {
                                lvm.SetFacebookUserProfileAsync();
                                var lg = await lvm.GetTokenFromAPI();
                                if (lg)
                                {
                                    await LogService.LogErrorsNew(error:$"facebook API ok=>login(token) ok " +
                                                                  " successful for email "+lvm?.User.EmailAddress,activity:activity+" successful facebook login, then called token ");
                                    lvm.PersistUserDetails();
                                  await  Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                                }

                                else
                                {
                                    await  LogService.LogErrorsNew(error:$"facebook API ok=>login(token) fail: email {lvm?.User?.EmailAddress}.",activity:activity+" login failed. will go ahead and reregister user ");

                                    var res = await lvm.Register();
                                    if (res)
                                    {
                                        await LogService.LogErrorsNew(error: "initial login failed. " +
                                                              "reg done,Ok.=>loggin in.. " +
                                                                      "for email" + lvm?.User?.EmailAddress, activity: activity + "  ");

                                        var tkResponse = await lvm.GetTokenFromAPI();

                                        //navigate to sign in user
                                        if (tkResponse)
                                        {
                                           await LogService.LogErrorsNew(error:$"..login failed=>" +
                                                           "reg ok => token fired & its ok"+
                                                                         "for email" + lvm?.User?.EmailAddress,activity:activity 
                                                                         +": After fb ok, login(token) failed, reg done, reg ok, login(token) ok ");
                                            lvm.PersistUserDetails();

                                            //Navigation.PopModalAsync(true);
                                            Application.Current.MainPage.Navigation.PushAsync(new AppLanding(), true);



                                        }
                                        else
                                        {
                                            await LogService.LogErrorsNew(error:$"token ok" +
                                                                          "for email" + lvm?.User?.EmailAddress,activity:activity + "  Facebook APi Successful." +
                                                              "immediate login failed." +
                                                        "reg was done after & was successful. " +
                                                              "token was called & its successful");

                                            if (await DisplayAlert("Sorry", "Sign in not successful at this time", "OK","Understood"))
                                                Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);
                                            else
                                                Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);


                                        }

                                    }
                                    else
                                    {
                                        await LogService.LogErrorsNew(error: $"registeration failed" +
                                                                         "for email" + lvm?.User?.EmailAddress, activity: activity + "  Facebook APi Successful. reg was done after & failed.");
                                        if (await DisplayAlert("Error", lvm.RegisterationError, "OK", "Back to login"))
                                            Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);
                                        else
                                            Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);


                                    }

                                }


                                //await lvm.CallProviderLoginAPI();
                            }


                        }
                        else
                        {
                            await LogService.LogErrorsNew(error: $"facebook graph api failed" +
                                                                       "for email" + lvm?.User?.EmailAddress, activity: activity + "  Facebook graph APi failed");

                            if (await DisplayAlert("Sorry", "Facebook GraphAPI call was not " +
                                                   "successful at this time.", "OK", "Understood"))
                              await  Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);
                            else
                              await  Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);

                        }
                    }
                    else
                    {
                        await LogService.LogErrorsNew(error: $"access token null" +
                                                                       "for email" + lvm?.User?.EmailAddress, activity: activity + "  get access token APi failed");

                        await DisplayAlert("Info", "Facebook access token empty at this time. " +
                                           "You don't have to do anything", "OK", "Noted");
                         

                    }
                }


            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
                if (await DisplayAlert("Sorry", "Facebook login had error at this time", "OK", "Understood"))
                    Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);
                else
                    Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);

            }
        }



        async void Logon_Tapped(object sender, System.EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Hi there", "It appears you have no internet access", "OK");
                return;
            }
            using (UserDialogs.Instance.Loading(""))
            {
                GlobalStaticFields.Username = lvm.User.Username;
                //lvm.User.Password = "Qwe123!";
                var resp = await lvm.GetTokenFromAPI();
                if (resp)
                {
                    // await Task.Run(() => (GlobalStaticFields.IntroModel = new IntroPageViewModel()));

                    lvm.PersistUserDetails();
                    await Navigation.PushAsync(new AppLanding());

                }
                else
                {
                    await DisplayAlert("Error", "Error logging in at this time. Please try again", "OK");
                }
            }
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }

        void Handle_Completed_1(object sender, System.EventArgs e)
        {
            Logon_Tapped(this, new EventArgs());
        }

        void Facebook_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                using (UserDialogs.Instance.Loading("Connecting to FaceBook.."))
                {

                    var apiRequest = $"{Constants.FacebookOAuthURL}?client_id={Constants.fbClientID}&display=popup&response_type=token&redirect_uri={Constants.redirectURI}&scope=email";
                    var wbView = new WebView()

                    {
                        Source = apiRequest,
                        HeightRequest = 1

                    };

                    wbView.Navigated += WbView_Navigated;
                    Content = wbView;

                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
                DisplayAlert("Error", "Error occured doing facebook", "OK");

            }


        }

        public LoggedOn()
        {
            InitializeComponent();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            #region commentedOut
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#FFFFFF");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Magenta;
            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;
            //var p = Application.Current.MainPage.Navigation.NavigationStack.Last().ToString();
            //var f = Application.Current.MainPage.Navigation.NavigationStack.Last(); 
            #endregion
            this.BindingContext = lvm;

        }

        public LoggedOn(string message )
        {
            InitializeComponent();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            var mytoast = MyToast.DisplayToast(Color.Blue, message);

            this.BindingContext = lvm;

        }

        private void SignUpTap_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginSignUp.SignUp(), true);
        }

        private void Dismissed_tapped(object sender, EventArgs e)
        {
            //Navigation.PopAsync(true);
           Navigation.PushAsync(new LoginOrSignUp());

        }

        private async void googleSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string clientId = null;
                string redirectUri = null;

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        clientId = Constants.iOSClientId;
                        redirectUri = Constants.iOSRedirectUrl;
                        break;

                    case Device.Android:
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
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
            }

        }


        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
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
                    lvm.User.Username = user.Email;
                    lvm.User.Password = user.Email;
                    lvm.User.EmailAddress = user.Email;
                    lvm.User.ConfirmPin = user.Email;
                    var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

                    if (tk)
                    {
                        Navigation.PushAsync(new AppLanding());
                    }
                    else
                    {
                        var reg = await lvm.Register().ConfigureAwait(false);

                        if (reg)
                        {
                            Device.BeginInvokeOnMainThread
                                  (
                                async () => Navigation.PushAsync(new AppLanding())
                                     );

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
                    UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
                }
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPassword1(),true);
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

        async void OnAuthCompletedNew(object sender, AuthenticatorCompletedEventArgs e)
        {


            try
            {
                using (UserDialogs.Instance.Loading(""))
                {
                    var authenticator = sender as OAuth2Authenticator;
                    if (authenticator != null)
                    {
                        authenticator.Completed -= OnAuthCompletedNew;
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

                        var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

                        if (tk)
                        {
                            Navigation.PushAsync(new AppLanding());
                        }
                        else
                        {
                            var reg = await lvm.Register().ConfigureAwait(false);

                            if (reg)
                            {
                                Device.BeginInvokeOnMainThread
                                      (
                                    async () => Navigation.PushAsync(new AppLanding())
                                         );

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


    }
}

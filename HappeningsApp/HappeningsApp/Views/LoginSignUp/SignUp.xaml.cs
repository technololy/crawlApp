﻿using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.OAuth;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.LoginSignUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        LoginViewModel lvm;
        Account account;
        AccountStore store;
        public bool HasPageBeenAnimated { get; set; } = false;
        public SignUp ()
		{
			InitializeComponent ();
            if (!HasPageBeenAnimated)
            {
                HasPageBeenAnimated = true;
                 AnimateThisPage();

            }
            lvm = new LoginViewModel();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            this.BindingContext = lvm;
           
		}
        #region ManualWorkingMethof
        //async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        //{
        //    var activity = "FaceBook Login";
        //    try
        //    {
        //        var url = e.Url;
        //        using (UserDialogs.Instance.Loading("Please wait..."))
        //        {
        //            lvm.ExtractAccessToken(url);
        //            if (!string.IsNullOrEmpty(lvm.accessToken))
        //            {
        //                await lvm.CallFaceBookGraphAPI(lvm.accessToken);
        //                if (lvm.IsSuccess)
        //                {
        //                    using (UserDialogs.Instance.Loading("Facebook Authenticated.Now logging on.."))
        //                    {
        //                        lvm.SetFacebookUserProfileAsync();
        //                        var lg = await lvm.GetTokenFromAPI();
        //                        if (lg)
        //                        {
        //                            await LogService.LogErrorsNew(error: $"facebook API ok=>login(token) ok " +
        //                                                          " successful for email " + lvm?.User.EmailAddress, activity: activity + " successful facebook login, then called token ");
        //                            lvm.PersistUserDetails();
        //                            await Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
        //                        }

        //                        else
        //                        {
        //                            await LogService.LogErrorsNew(error: $"facebook API ok=>login(token) fail: email {lvm?.User?.EmailAddress}.", activity: activity + " login failed. will go ahead and reregister user ");

        //                            var res = await lvm.Register();
        //                            if (res)
        //                            {
        //                                await LogService.LogErrorsNew(error: "initial login failed. " +
        //                                                      "reg done,Ok.=>loggin in.. " +
        //                                                              "for email" + lvm?.User?.EmailAddress, activity: activity + "  ");

        //                                var tkResponse = await lvm.GetTokenFromAPI();

        //                                //navigate to sign in user
        //                                if (tkResponse)
        //                                {
        //                                    await LogService.LogErrorsNew(error: $"..login failed=>" +
        //                                                    "reg ok => token fired & its ok" +
        //                                                                  "for email" + lvm?.User?.EmailAddress, activity: activity
        //                                                                  + ": After fb ok, login(token) failed, reg done, reg ok, login(token) ok ");
        //                                    lvm.PersistUserDetails();

        //                                    //Navigation.PopModalAsync(true);
        //                                    Application.Current.MainPage.Navigation.PushAsync(new AppLanding(), true);



        //                                }
        //                                else
        //                                {
        //                                    await LogService.LogErrorsNew(error: $"token ok" +
        //                                                                  "for email" + lvm?.User?.EmailAddress, activity: activity + "  Facebook APi Successful." +
        //                                                      "immediate login failed." +
        //                                                "reg was done after & was successful. " +
        //                                                      "token was called & its successful");

        //                                    if (await DisplayAlert("Sorry", "Sign in not successful at this time", "OK", "Understood"))
        //                                        Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);
        //                                    else
        //                                        Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);


        //                                }

        //                            }
        //                            else
        //                            {
        //                                await LogService.LogErrorsNew(error: $"registeration failed" +
        //                                                                 "for email" + lvm?.User?.EmailAddress, activity: activity + "  Facebook APi Successful. reg was done after & failed.");
        //                                if (await DisplayAlert("Error", lvm.RegisterationError, "OK", "Back to login"))
        //                                    Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);
        //                                else
        //                                    Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);


        //                            }

        //                        }


        //                        //await lvm.CallProviderLoginAPI();
        //                    }


        //                }
        //                else
        //                {
        //                    await LogService.LogErrorsNew(error: $"facebook graph api failed" +
        //                                                               "for email" + lvm?.User?.EmailAddress, activity: activity + "  Facebook graph APi failed");

        //                    if (await DisplayAlert("Sorry", "facebook graph api failed" +
        //                                           " at this time", "OK", "Understood"))
        //                        await Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);
        //                    else
        //                        await Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);

        //                }
        //            }
        //            else
        //            {
        //                await LogService.LogErrorsNew(error: $"access token null" +
        //                                                               "for email" + lvm?.User?.EmailAddress, activity: activity + "  get access token APi failed");

        //                if (await DisplayAlert("Sorry", "Fscebook access token null at this time", "OK", "Try again"))
        //                    await Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);
        //                else
        //                    await Application.Current.MainPage.Navigation.PushAsync(new LoggedOn(), true);

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        var log = ex;
        //        LogService.LogErrors(log.ToString());
        //        await DisplayAlert("Sorry", "Facebook sign in had errors at this time", "OK", "Understood");

        //    }
        //}



        //private void facebook_clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (UserDialogs.Instance.Loading("Connecting to FaceBook.."))
        //        {

        //            var apiRequest = $"{Constants.FacebookOAuthURL}?client_id={Constants.fbClientID}&display=popup&response_type=token&redirect_uri={Constants.redirectURI}&scope=email";
        //            var wbView = new WebView()

        //            {
        //                Source = apiRequest,
        //                HeightRequest = 1

        //            };

        //            wbView.Navigated += WbView_Navigated;
        //            Content = wbView;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = ex;
        //        LogService.LogErrors(log.ToString());
        //    }

        //}

        //private void googleBtn_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SocialMedia = "google";
        //        string clientId = null;
        //        string redirectUri = null;

        //        switch (Xamarin.Forms.Device.RuntimePlatform)
        //        {
        //            case Xamarin.Forms.Device.iOS:
        //                clientId = Constants.iOSClientId;
        //                redirectUri = Constants.iOSRedirectUrl;
        //                break;

        //            case Xamarin.Forms.Device.Android:
        //                clientId = Constants.AndroidClientId;
        //                redirectUri = Constants.AndroidRedirectUrl;
        //                break;
        //        }

        //        var authenticator = new OAuth2Authenticator(
        //            clientId,
        //            null,
        //            Constants.Scope,
        //            new Uri(Constants.AuthorizeUrl),
        //            new Uri(redirectUri),
        //            new Uri(Constants.AccessTokenUrl),
        //            null,
        //            true);

        //        authenticator.Completed += OnAuthCompleted;
        //        authenticator.Error += OnAuthError;

        //        AuthenticationState.Authenticator = authenticator;

        //        var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
        //        presenter.Login(authenticator);
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = ex;
        //        LogService.LogErrors(log.ToString());
        //    }

        //}
        #endregion
        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }
        void Password_Completed(object sender, System.EventArgs e)
        {
            txtConfirmPassword.Focus();
        }


        private async void signUp_Clicked(object sender, EventArgs e)
        {
            //LogService.LogErrors("Testing logs");
            try
            {
                if (!lvm.IsRegisterationDetailsValid())
                {
                    await DisplayAlert("Error", lvm.RegisterationError, "OK");
                    return;
                }
                GlobalStaticFields.Username = lvm.User.Username;
                var res = await lvm.Register();
                if (res)
                {
                    var tkResponse = await lvm.GetTokenFromAPI();

                    //navigate to sign in user
                    if (tkResponse)
                    {
                        lvm.PersistUserDetails();
                       // await Task.Run(() => (GlobalStaticFields.IntroModel = new IntroPageViewModel()));

                        await Application.Current.MainPage.Navigation.PushAsync(new AppLanding());



                    }
                    else
                    {
                        await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                    }

                }
                else
                {
                    await DisplayAlert("Sorry", lvm.RegisterationError, "OK");

                }
            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());
           //     var logger = new LogModel()
           //     {
           //         User = GlobalStaticFields.Username,
           //         Error = log.ToString()
           //     };
           //await APIService.PostNew<LogModel>(logger, "Error");
            }

        }

        void ConfirmPassword_Completed(object sender, System.EventArgs e)
        {
            signUp_Clicked(this, new EventArgs());
        }

     

        private  void goBack_Tapped(object sender, EventArgs e)
        {
             Navigation.PopAsync(true);

        }

       private async Task AnimateThisPage()
        {
            await googleBtn.ScaleTo(0, 0);
            await signUpFB.ScaleTo(0, 0);
            await signUp.ScaleTo(0, 0);
            await signUpParentStack.TranslateTo(-500, 0, 0);
            await signUpParentStack.TranslateTo(0, 0, 1000, Easing.SinIn);

            await googleBtn.ScaleTo(1, 500, Easing.BounceOut);
            await signUpFB.ScaleTo(1, 500, Easing.BounceOut);
            await signUp.ScaleTo(1, 500, Easing.BounceOut);
            //HasPageBeenAnimated = true;
            //await  myStack.TranslateTo(0, -150,0);         

            // await myStack.TranslateTo(0, 0, 1000, Easing.SinInOut);

        }

        protected async override void OnAppearing()
        {
            if (!HasPageBeenAnimated)
            {
                HasPageBeenAnimated = true;

                await AnimateThisPage();

            }
            base.OnAppearing();
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

      

        public string SocialMedia { get; set; }

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
                            clientId:Constants.fbClientID,
                            scope:"email",
                            authorizeUrl:new Uri( Constants.FacebookOAuthURL),
                            redirectUrl: new Uri( Constants.redirectURI)

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

                      authenticator_   = new OAuth2Authenticator(
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

                        var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

                        if (tk)
                        {
                            lvm.PersistUserDetails();

                            //await Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                            Device.BeginInvokeOnMainThread
                                       (
                                     async () => Navigation.PushAsync(new AppLanding())
                                          );
                        }
                        else
                        {
                            var reg = await lvm.Register().ConfigureAwait(false);

                            if (reg)
                            {
                                lvm.PersistUserDetails();

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

        #region SavedForFutureReview


        //void OnAuthErrorFacebook(object sender, AuthenticatorErrorEventArgs e)
        //{
        //    var authenticator = sender as OAuth2Authenticator;
        //    if (authenticator != null)
        //    {
        //        authenticator.Completed -= OnAuthCompletedFacebook;
        //        authenticator.Error -= OnAuthErrorFacebook;
        //    }

        //    Debug.WriteLine("Authentication error: " + e.Message);
        //}


        //async void OnAuthCompletedCombined(object sender, AuthenticatorCompletedEventArgs e)
        //{
        //    using (UserDialogs.Instance.Loading(""))
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= OnAuthCompleted;
        //            authenticator.Error -= OnAuthError;
        //        }

        //        User user = null;
        //        if (e.IsAuthenticated)
        //        {
        //            OAuth2Request oAuth2Request = null;
        //            string returnedJson = null;
        //            // If the user is authenticated, request their basic user data from Google
        //            // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
        //            switch (SocialMedia)
        //            {
        //                case "facebook":
        //                    oAuth2Request = new OAuth2Request("GET", new Uri(Constants.graphAPI), null, e.Account);
        //                    break;

        //                case "google":
        //                    oAuth2Request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);

        //                    break;
        //                default:
        //                    break;
        //            }
        //            //var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
        //            var response = await oAuth2Request.GetResponseAsync();
        //            if (response != null)
        //            {
        //                // Deserialize the data and store it in the account store
        //                // The users email address will be used to identify data in SimpleDB
        //                string userJson = await response.GetResponseTextAsync();
        //                switch (SocialMedia)
        //                {
        //                    case "google":
        //                        user = JsonConvert.DeserializeObject<User>(userJson);
        //                        lvm.User.Username = user.Email;
        //                        lvm.User.Password = user.Email;
        //                        lvm.User.EmailAddress = user.Email;
        //                        lvm.User.ConfirmPin = user.Email;
        //                        break;

        //                    case "facebook":

        //                       //var fbuser = JsonValue.Parse(response.GetResponseText());
        //                        //lvm.User.Username = fbuser["email"];
        //                        //lvm.User.Password = fbuser["email"];
        //                        //lvm.User.EmailAddress = fbuser["email"];
        //                        //lvm.User.ConfirmPin = fbuser["email"];
        //                        break;
        //                    default:
        //                        break;
        //                }

        //            }

        //            if (account != null)
        //            {
        //                store.Delete(account, Constants.AppName);
        //            }

        //            await store.SaveAsync(account = e.Account, Constants.AppName);
        //            //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful login"));

        //            var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

        //            if (tk)
        //            {
        //                Navigation.PushAsync(new AppLanding());
        //            }
        //            else
        //            {
        //                var reg = await lvm.Register().ConfigureAwait(false);

        //                if (reg)
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (
        //                        async () => Navigation.PushAsync(new AppLanding())
        //                             );

        //                }
        //                else
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (() =>
        //                           UserDialogs.Instance.Toast(t.ShowMyToast(Color.OrangeRed, $"Unsuccessful. {lvm.RegisterationError} ")));

        //                }
        //            }

        //        }
        //        else
        //        {
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
        //        }
        //    }

        //}


        //async void OnAuthCompleted__Main(object sender, AuthenticatorCompletedEventArgs e)
        //{
        //    using (UserDialogs.Instance.Loading(""))
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= OnAuthCompleted;
        //            authenticator.Error -= OnAuthError;
        //        }

        //        User user = null;
        //        if (e.IsAuthenticated)
        //        {
        //            // If the user is authenticated, request their basic user data from Google
        //            // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
        //            var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
        //            var response = await request.GetResponseAsync();
        //            if (response != null)
        //            {
        //                // Deserialize the data and store it in the account store
        //                // The users email address will be used to identify data in SimpleDB
        //                string userJson = await response.GetResponseTextAsync();
        //                user = JsonConvert.DeserializeObject<User>(userJson);
        //            }

        //            if (account != null)
        //            {
        //                store.Delete(account, Constants.AppName);
        //            }

        //            await store.SaveAsync(account = e.Account, Constants.AppName);
        //            //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful google login"));
        //            lvm.User.Username = user.Email;
        //            lvm.User.Password = user.Email;
        //            lvm.User.EmailAddress = user.Email;
        //            lvm.User.ConfirmPin = user.Email;
        //            var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

        //            if (tk)
        //            {
        //                Navigation.PushAsync(new AppLanding());
        //            }
        //            else
        //            {
        //                var reg = await lvm.Register().ConfigureAwait(false);

        //                if (reg)
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (
        //                        async () => Navigation.PushAsync(new AppLanding())
        //                             );

        //                }
        //                else
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (() =>
        //                           UserDialogs.Instance.Toast(t.ShowMyToast(Color.OrangeRed, $"Unsuccessful. {lvm.RegisterationError} ")));

        //                }
        //            }

        //        }
        //        else
        //        {
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
        //        }
        //    }

        //}


        //private void FacebookButton_clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SocialMedia = "facebook";
        //       var authenticator_ = new OAuth2Authenticator

        //             (
        //                 clientId: Constants.fbClientID,

        //                 scope: "email",
        //                 authorizeUrl: new Uri(Constants.FacebookOAuthURL),
        //                 redirectUrl: new Uri(Constants.redirectURI)

        //             );


        //        authenticator_.Completed += OnAuthCompleted;
        //        authenticator_.Error += OnAuthErrorFacebook;

        //        AuthenticationState.Authenticator = authenticator_;

        //        var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
        //        presenter.Login(authenticator_);

        //    }
        //    catch (Exception ex)
        //    {
        //        var log = ex;
        //        LogService.LogErrors(log.ToString());
        //    }

        //}

        //async void OnAuthCompletedFacebook(object sender, AuthenticatorCompletedEventArgs e)
        //{
        //    using (UserDialogs.Instance.Loading(""))
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= OnAuthCompletedFacebook;
        //            authenticator.Error -= OnAuthErrorFacebook;
        //        }


        //        if (e.IsAuthenticated)
        //        {
        //            OAuth2Request oAuth2Request = null;
        //            oAuth2Request = new OAuth2Request("GET", new Uri(Constants.graphAPI), null, e.Account);

        //           // string returnedJson = null;
        //            // If the user is authenticated, request their basic user data from Google
        //            // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo

        //            var response = await oAuth2Request.GetResponseAsync();
        //            if (response != null)
        //            {
        //                // Deserialize the data and store it in the account store
        //                // The users email address will be used to identify data in SimpleDB
        //                string userJson = await response.GetResponseTextAsync();

        //                //var fbuser = JsonValue.Parse(response.GetResponseText());
        //                //lvm.User.Username = fbuser["email"];
        //                //lvm.User.Password = fbuser["email"];
        //                //lvm.User.EmailAddress = fbuser["email"];
        //                //lvm.User.ConfirmPin = fbuser["email"];



        //            }

        //            if (account != null)
        //            {
        //                store.Delete(account, Constants.AppName);
        //            }

        //            await store.SaveAsync(account = e.Account, Constants.AppName);
        //            //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful login"));

        //            var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

        //            if (tk)
        //            {
        //                Navigation.PushAsync(new AppLanding());
        //            }
        //            else
        //            {
        //                var reg = await lvm.Register().ConfigureAwait(false);

        //                if (reg)
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (
        //                        async () => Navigation.PushAsync(new AppLanding())
        //                             );

        //                }
        //                else
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (() =>
        //                           UserDialogs.Instance.Toast(t.ShowMyToast(Color.OrangeRed, $"Unsuccessful. {lvm.RegisterationError} ")));

        //                }
        //            }

        //        }
        //        else
        //        {
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
        //        }
        //    }

        //}

        //async void Authenticator__Completed(object sender, AuthenticatorCompletedEventArgs e)
        //{
        //    using (UserDialogs.Instance.Loading(""))
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= Authenticator__Completed;
        //            authenticator.Error -= OnAuthErrorFacebook;
        //        }


        //        if (e.IsAuthenticated)
        //        {
        //            OAuth2Request oAuth2Request = null;
        //            oAuth2Request = new OAuth2Request("GET", new Uri(Constants.graphAPI), null, e.Account);

        //            // string returnedJson = null;
        //            // If the user is authenticated, request their basic user data from Google
        //            // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo

        //            var response = await oAuth2Request.GetResponseAsync();
        //            if (response != null)
        //            {
        //                // Deserialize the data and store it in the account store
        //                // The users email address will be used to identify data in SimpleDB
        //                string userJson = await response.GetResponseTextAsync();

        //                //var fbuser = JsonValue.Parse(response.GetResponseText());
        //                //lvm.User.Username = fbuser["email"];
        //                //lvm.User.Password = fbuser["email"];
        //                //lvm.User.EmailAddress = fbuser["email"];
        //                //lvm.User.ConfirmPin = fbuser["email"];



        //            }

        //            if (account != null)
        //            {
        //                store.Delete(account, Constants.AppName);
        //            }

        //            await store.SaveAsync(account = e.Account, Constants.AppName);
        //            //UserDialogs.Instance.Alert("", "Email address: " + user.Email + "\n fullname:" + user.Name + "\n gender:" + user.Gender, "OK");
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.Green, "Successful login"));

        //            var tk = await lvm.GetTokenFromAPI().ConfigureAwait(false);

        //            if (tk)
        //            {
        //                Navigation.PushAsync(new AppLanding());
        //            }
        //            else
        //            {
        //                var reg = await lvm.Register().ConfigureAwait(false);

        //                if (reg)
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (
        //                        async () => Navigation.PushAsync(new AppLanding())
        //                             );

        //                }
        //                else
        //                {
        //                    Device.BeginInvokeOnMainThread
        //                          (() =>
        //                           UserDialogs.Instance.Toast(t.ShowMyToast(Color.OrangeRed, $"Unsuccessful. {lvm.RegisterationError} ")));

        //                }
        //            }

        //        }
        //        else
        //        {
        //            MyToast t = new MyToast();
        //            UserDialogs.Instance.Toast(t.ShowMyToast(Color.PaleVioletRed, "Unsuccessful google login"));
        //        }
        //    }
        //}

        #endregion

    }
}
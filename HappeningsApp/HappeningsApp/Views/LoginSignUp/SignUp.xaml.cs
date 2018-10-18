using Acr.UserDialogs;
using HappeningsApp.Models;
using HappeningsApp.OAuth;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public SignUp ()
		{
			InitializeComponent ();
            lvm = new LoginViewModel();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            this.BindingContext = lvm;
           
		}
        async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
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
                                    lvm.PersistUserDetails();
                                    Application.Current.MainPage.Navigation.PushAsync(new AppLanding());
                                }

                                else
                                {
                                    var res = await lvm.Register();
                                    if (res)
                                    {
                                        var tkResponse = await lvm.GetTokenFromAPI();

                                        //navigate to sign in user
                                        if (tkResponse)
                                        {
                                            lvm.PersistUserDetails();

                                            //Navigation.PopModalAsync(true);
                                            Application.Current.MainPage.Navigation.PushAsync(new AppLanding(), true);



                                        }
                                        else
                                        {
                                            await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                                        }

                                    }
                                    else
                                    {
                                        if (await DisplayAlert("Sorry", lvm.RegisterationError, "OK", "Back to login"))
                                            Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);
                                        else
                                            Application.Current.MainPage.Navigation.PushAsync(new LoginOrSignUp(), true);


                                    }

                                }


                                //await lvm.CallProviderLoginAPI();
                            }


                        }
                    }
                }


            }
            catch (Exception ex)
            {
                var log = ex;
                LogService.LogErrors(log.ToString());

            }
        }


        private  void facebook_clicked(object sender, EventArgs e)
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
            }
        
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

        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }
        void Password_Completed(object sender, System.EventArgs e)
        {
            txtConfirmPassword.Focus();
        }


        void ConfirmPassword_Completed(object sender, System.EventArgs e)
        {
            signUp_Clicked(this, new EventArgs());
        }

     

        private  void goBack_Tapped(object sender, EventArgs e)
        {
             Navigation.PopAsync(true);

        }

        private void googleBtn_Clicked(object sender, EventArgs e)
        {
            try
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

    }
}
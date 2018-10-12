using Acr.UserDialogs;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.LoginSignUp;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using UIKit;
using Xamarin.Forms;

namespace HappeningsApp.Views
{
    public partial class LoggedOn : ContentPage
    {

        public string accessToken { get; set; }
        LoginViewModel lvm = new LoginViewModel();
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
                                        if (await DisplayAlert("Error", lvm.RegisterationError, "OK", "Back to login"))
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

            }


        }

        public LoggedOn()
        {
            InitializeComponent();
            #region commentedOut
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#FFFFFF");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Magenta;
            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;
            //var p = Application.Current.MainPage.Navigation.NavigationStack.Last().ToString();
            //var f = Application.Current.MainPage.Navigation.NavigationStack.Last(); 
            #endregion
            this.BindingContext = lvm;

        }

        private void SignUpTap_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginSignUp.SignUp(), true);
        }

        private void Dismissed_tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }

        private async void googleSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (UserDialogs.Instance.Loading(""))
                {
                    var d = lvm.OnGoogleButtonClick();
                    if (d)
                    {
                        lvm.GetTokenFromAPI();
                        if (lvm.IsSuccess)
                        {
                            await Navigation.PushAsync(new AppLanding(), true);
                        }
                        else
                        {
                            lvm.Register();
                            if (lvm.IsSuccess)
                            {
                                await Navigation.PushAsync(new AppLanding(), true);

                            }
                            else
                            {

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
    }
}

using Acr.UserDialogs;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
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
                                await lvm.CallProviderLoginAPI();
                            }
                        

                        }
                    }
                }
              
              
            }
            catch (Exception ex)
            {
                var log = ex;

            }
        }

 

        void Logon_Tapped(object sender, System.EventArgs e)
        {
        }

        void Facebook_Tapped(object sender, System.EventArgs e)
        {
          try
            {
                using (UserDialogs.Instance.Loading("Connecting to FaceBook.."))
                {

                    var apiRequest = $"{Constants.FacebookOAuthURL}?client_id={Constants.fbClientID}&display=popup&response_type=token&redirect_uri={Constants.redirectURI}";
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

        private  void SignUpTap_Tapped(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new LoginSignUp.SignUp(),true);
        }
    }
}

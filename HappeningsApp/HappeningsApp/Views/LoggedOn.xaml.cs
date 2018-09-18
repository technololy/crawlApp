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
        LoginViewModel _vm = new LoginViewModel();
        async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                var url = e.Url;
                ExtractAccessToken(url);
                if (!string.IsNullOrEmpty(accessToken))
                {
                   await _vm.SetFacebookUserProfileAsync(accessToken);
                }
              
            }
            catch (Exception ex)
            {
                var log = ex;

            }
        }

        private void ExtractAccessToken(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
              
                string[] sep =  { "access_token=", "&expires_in=" };
                var urlSplit = url.Split(sep,StringSplitOptions.None);

                accessToken = urlSplit[1].ToString();
            
            }
            else
            {
             
                accessToken = string.Empty;
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
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#FFFFFF");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Magenta;
            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;
            //var p = Application.Current.MainPage.Navigation.NavigationStack.Last().ToString();
            //var f = Application.Current.MainPage.Navigation.NavigationStack.Last();

        }
    }
}

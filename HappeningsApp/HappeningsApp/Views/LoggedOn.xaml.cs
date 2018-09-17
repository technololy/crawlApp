using Acr.UserDialogs;
using HappeningsApp.Services;
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

        async void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                var url = e.Url;
                ExtractAccessToken(url);
                using (UserDialogs.Instance.Loading("Please wait.........."))
                {

                   await Task.Delay(5000);

                }
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }

        private string ExtractAccessToken(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }

        void Logon_Tapped(object sender, System.EventArgs e)
        {
        }

        void Facebook_Tapped(object sender, System.EventArgs e)
        {
          
            var apiRequest =$"{Constants.FacebookOAuthURL}?client_id={Constants.fbClientID}&display=popup&response_type=token&redirect_uri={Constants.redirectURI}";
            var wbView = new WebView() 
            
            {
                Source = apiRequest,
                HeightRequest=1
            
            };

            wbView.Navigated += WbView_Navigated;
            Content = wbView;

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

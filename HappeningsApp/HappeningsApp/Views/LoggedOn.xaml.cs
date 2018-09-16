using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
//using UIKit;
using Xamarin.Forms;

namespace HappeningsApp.Views
{
    public partial class LoggedOn : ContentPage
    {
        //IUserDialogs UserDialogs ;
        //readonly IUserDialogs Dialog = FreshIOC.Container.Resolve<IUserDialogs>();

        void WbView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                var url = e.Url;

                using(UserDialogs.Instance.ShowLoading("Please wait....."))
                {

                    Task.Delay(5000);

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
            string ClientId = "1752084648235245";
            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";
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

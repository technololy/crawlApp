using System;
using Xamarin.Forms;
using HappeningsApp.Views;
using Xamarin.Forms.Xaml;
using HappeningsApp.Views.Carousel;
using HappeningsApp.Views.AppViews;
using System.Threading.Tasks;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Plugin.Connectivity;
using Plugin.Badge;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HappeningsApp
{
    public partial class App : Application
    {
        public string username{get;set;}

        public string password{get;set;}

        public App()
        {
            InitializeComponent();




            GetAppInstallID();

            //MainPage = new NavigationPage(new Views.LoggedOn());

            //return;
            if (IsUserLoggedOn())
            {
                try
                {
                    LoginViewModel lvmm = new LoginViewModel();
                    lvmm.User.Username = Current.Properties["username"].ToString();
                    lvmm.User.EmailAddress = lvmm.User.Username;
                    lvmm.User.Password = Current.Properties["password"].ToString();
                    GlobalStaticFields.Username = lvmm.User.EmailAddress;
                    lvmm.GetTokenFromAPI();


                    MainPage = new NavigationPage(new Views.AppLanding());
                }
                catch (Exception ex)
                {
                    MainPage = new NavigationPage(new Views.LoginSignUp.LoginOrSignUp());

                    //LogService.LogErrorsNew(error: ex.ToString(), activity: "Exception at app.xaml.cs loggin on");

                }

            }
            else
            {
                MainPage = new NavigationPage(new Views.LoginSignUp.LoginOrSignUp());

            }
            #region comment
            //MainPage = new NavigationPage(new MainPage())


            //{
            //    BarBackgroundColor = Color.FromHex("#ff5300"),
            //    BarTextColor = Color.White,

            //}
            //  ;
            #endregion
            // MainPage =new NavigationPage( new Views.test.MyPage());



        }

        private async void GetAppInstallID()
        {
            try
            {
                System.Guid? installId = await AppCenter.GetInstallIdAsync();

            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }

        private bool IsUserLoggedOn()
        {
            bool log = false;
            try
            {
                log = Convert.ToBoolean(Application.Current.Properties["IsUserLoggedOn"]);

            }
            catch (Exception ex)
            {
                var logg = ex;
                // LogService.LogErrorsNew(error:logg.ToString(),activity:"Exception at app.xaml.cs IsUserLoggedOn()");
            }
            return log;

        }
        public void PersistUsername()
        {

        }



        private void CheckIfUserIsPersistent()
        {

        }

        public static Page GetMainPage()
        {
            var mainPage = new MainPage();
            var navPage = new NavigationPage(mainPage);
            navPage.BarBackgroundColor = Color.Green;
            return navPage;
        }
        protected override void OnStart()
        {
            // This should come before AppCenter.Start() is called
            // Avoid duplicate event registration:
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    if (Xamarin.Forms.Device.RuntimePlatform== Xamarin.Forms.Device.iOS)
                    {
                        CrossBadge.Current.SetBadge(0);
                        Application.Current.MainPage.Navigation.PushAsync(new Views.Messages.MessagesLanding());
                    }
                    // Add the notification message and title to the message
                    var summary = $"Push notification received:" +
                                        $"\n\tNotification title: {e.Title}" +
                                        $"\n\tMessage: {e.Message}";

                    // If there is custom data associated with the notification,
                    // print the entries
                    //Acr.UserDialogs.UserDialogs.Instance.Alert(summary, "", "OK");
                    if (e.CustomData != null)
                    {
                        summary += "\n\tCustom data:\n";
                        foreach (var key in e.CustomData.Keys)
                        {
                            summary += $"\t\t{key} : {e.CustomData[key]}\n";
                        }
                    }

                    // Send the notification summary to debug output
                    System.Diagnostics.Debug.WriteLine(summary);
                };
            }
            // Handle when your app starts
            AppCenter.Start("ios=13747af2-67c5-4d4a-9db1-5f8a7149fc78;" + "android=0fb17a8c-75bc-4fe7-8d26-8aa2aab9a66a;", typeof(Analytics), typeof(Crashes), typeof(Push));

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
         
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            var log = 1;
        }
    }
}

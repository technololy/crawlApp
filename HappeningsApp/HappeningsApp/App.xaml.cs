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

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HappeningsApp
{
	public partial class App : Application
	{
		public string username
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }
		public App ()
		{
			InitializeComponent();

       




            //MainPage = new NavigationPage(new Views.LoginSignUp.LoginOrSignUp());


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
                    LogService.LogErrorsNew(error: ex.ToString(), activity: "Exception at app.xaml.cs loggin on");

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
		protected override void OnStart ()
		{
            // Handle when your app starts
            AppCenter.Start("ios=13747af2-67c5-4d4a-9db1-5f8a7149fc78;" + "android=0fb17a8c-75bc-4fe7-8d26-8aa2aab9a66a;", typeof(Analytics), typeof(Crashes),typeof(Push));

        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

using System;
using Xamarin.Forms;
using HappeningsApp.Views;
using Xamarin.Forms.Xaml;
using HappeningsApp.Views.Carousel;
using HappeningsApp.Views.AppViews;

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

            //CheckIfUserIsPersistent();
            if (IsUserLoggedOn())
            {
                ViewModels.LoginViewModel lvmm = new ViewModels.LoginViewModel();
                lvmm.User.Username = Current.Properties["username"].ToString();
                lvmm.User.Password = Current.Properties["password"].ToString();
               lvmm.GetTokenFromAPI();
                MainPage = new NavigationPage(new Views.AppLanding());

            }
            else
            {
                MainPage = new NavigationPage(new Views.LoginSignUp.LoginOrSignUp());

            }
            //MainPage = new NavigationPage(new MainPage())


            //{
            //    BarBackgroundColor = Color.FromHex("#ff5300"),
            //    BarTextColor = Color.White,

            //}
                ;
            //MainPage = GetMainPage();

            //MainPage = new NavigationPage(new AppLanding());
            //MainPage = new NavigationPage(new Views.LoginSignUp.LoginOrSignUp());
			//MainPage = new NavigationPage(new Deals());
            //MainPage = new NavigationPage(new Collections())

                //{
                //    BarBackgroundColor = Color.FromHex("#ff5300"),
                //    BarTextColor = Color.White,

                //}

                ;
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
            }
            return log;

        }
        public void PersistUsername()
        {

        }



        private void CheckIfUserIsPersistent()
        {
            throw new NotImplementedException();
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

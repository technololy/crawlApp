using System;
using Xamarin.Forms;
using HappeningsApp.Views;
using Xamarin.Forms.Xaml;
using HappeningsApp.Views.Carousel;
using HappeningsApp.Views.AppViews;
using System.Threading.Tasks;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;

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


            if (IsUserLoggedOn())
            {
                LoginViewModel lvmm = new LoginViewModel();
                lvmm.User.Username = Current.Properties["username"].ToString();
                lvmm.User.Password = Current.Properties["password"].ToString();
                lvmm.GetTokenFromAPI();
                //Task.Run(() => (GlobalStaticFields.IntroModel = new IntroPageViewModel()));

                MainPage = new NavigationPage(new Views.AppLanding());

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
            // MainPage =new NavigationPage( new Views.Survey.SurveyOne());



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

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
		
		public App ()
		{
			InitializeComponent();


            //MainPage = new NavigationPage(new MainPage())


            //{
            //    BarBackgroundColor = Color.FromHex("#ff5300"),
            //    BarTextColor = Color.White,

            //}
                ;
            //MainPage = GetMainPage();

            //MainPage = new NavigationPage(new AppLanding());
            MainPage = new NavigationPage(new LoggedOn());
			//MainPage = new NavigationPage(new Deals());
            //MainPage = new NavigationPage(new Collections())

                //{
                //    BarBackgroundColor = Color.FromHex("#ff5300"),
                //    BarTextColor = Color.White,

                //}

                ;
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

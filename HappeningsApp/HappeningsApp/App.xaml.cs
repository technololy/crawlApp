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


			MainPage = new NavigationPage(new MainPage());
			//MainPage = new NavigationPage(new AppLanding());
            //MainPage = new NavigationPage(new Deals());
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

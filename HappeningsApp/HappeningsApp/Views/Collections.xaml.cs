using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Collections : ContentPage
	{       
        FavViewModel fvm;
               
        public Collections ()
		{
			InitializeComponent();
         
        }
    
        async void LogOut_Clicked(object sender, System.EventArgs e)
        {
           await Application.Current.MainPage.Navigation.PopToRootAsync(true);
            
        }


        void Favourites_Clicked_3(object sender, System.EventArgs e)
        {
           //Application.Current.MainPage.Navigation.PushAsync(new AppViews.Favourites(), true);
            Application.Current.MainPage.Navigation.PushAsync(new Favourites.MyCreatedCollections(), true);
        }

        void Messages_Tapped(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.Messages.MessagesLanding(), true);

        }
    }
}
using HappeningsApp.Models;
using HappeningsApp.Services;
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
        public ObservableCollection<FavoriteModel> Favs { get; set; }
        public CollectionsModelResp Collectionz { get; set; }
        void Favourites_Clicked_3(object sender, System.EventArgs e)
        {
           Application.Current.MainPage.Navigation.PushAsync(new AppViews.Favourites(),true);
        }

      

     

        public Collections ()
		{
			InitializeComponent ();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.GreenYellow;
            GetFavs();
        }
        public async Task<ObservableCollection<FavoriteModel>> GetFavs()
        {
            FavService fs = new FavService();
            CollectionService cs = new CollectionService();
            //Favs= await fs.GetFavsTest();
            //Favs = await cs.GetUserFavs();
            Collectionz = await cs.GetUserFavsNew();

            //GlobalStaticFields.AllService.Add(Favs);
            //GlobalStaticFields.Favs = Favs;
            GlobalStaticFields.AllCollections = Collectionz;
            return Favs;
        }
        async void LogOut_Clicked(object sender, System.EventArgs e)
        {
           await Application.Current.MainPage.Navigation.PopToRootAsync(true);
            
        }
	}
}
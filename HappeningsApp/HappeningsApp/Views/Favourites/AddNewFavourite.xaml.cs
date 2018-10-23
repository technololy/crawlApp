using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Favourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewFavourite : ContentPage
	{
        FavViewModel fvm;
		public AddNewFavourite ()
		{
			InitializeComponent ();
            fvm = new FavViewModel();
            BindingContext = fvm;
		}

        private void txtName_Completed(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }

        private async void txtDescription_Completed(object sender, EventArgs e)
        {
            fvm.NewCollectionNick = fvm.NewCollectionNick;//segun said i should remove the nick name
            if (string.IsNullOrEmpty(fvm.NewCollectionNick)|| string.IsNullOrEmpty(fvm.NewCollectionName) )
            {
                return;
            }
           var b = await fvm.AddNewCollection().ConfigureAwait(false);

            if (b)
            {
                await fvm.GetListCollection();
               await Navigation.PopModalAsync(true);
                
            }
        }

        private void txtCancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
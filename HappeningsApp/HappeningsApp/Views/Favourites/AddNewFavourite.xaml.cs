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
           var b = await fvm.AddNewCollection().ConfigureAwait(false);

            if (b)
            {
                Navigation.PopModalAsync(true);
            }
        }

        private void txtCancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
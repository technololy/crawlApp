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
        MyCreatedCollectionViewModel mcvm;
        // Add an event to notify when things are updated
        public event EventHandler<EventArgs> AddOperationCompeleted;
        public AddNewFavourite ()
		{
			InitializeComponent ();
            fvm = new FavViewModel();
         mcvm=   new MyCreatedCollectionViewModel();
            BindingContext = mcvm;
		}

        private void txtName_Completed(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }

        private async void txtDescription_Completed(object sender, EventArgs e)
        {
            mcvm.NewCollectionNick = mcvm.NewCollectionName;//segun said i should remove the nick name
            if (string.IsNullOrEmpty(mcvm.NewCollectionNick)|| 
                string.IsNullOrEmpty(mcvm.NewCollectionName) )
            {
                return;
            }
           var b = await mcvm.AddNewCollection().ConfigureAwait(false);

            if (b)
            {
                //await fvm.GetListCollection();
               // MyCreatedCollectionViewModel mcvm = new MyCreatedCollectionViewModel();
                //AddOperationCompeleted?.Invoke(this, EventArgs.Empty);

                Device.BeginInvokeOnMainThread(() => Navigation.PopModalAsync(true));
                //mcvm.ActivityRunning = true;
                //Device.BeginInvokeOnMainThread(async() => await mcvm.GetListCollection().ConfigureAwait(false));

                //too tired to move it all to the newly 
                //created MyCreatedCollectionViewModel. will do so later
              
                
            }
        }

        private void txtCancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Favourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewFavourite : ContentPage
	{
		public AddNewFavourite ()
		{
			InitializeComponent ();
		}

        private void txtName_Completed(object sender, EventArgs e)
        {

        }

        private void txtDescription_Completed(object sender, EventArgs e)
        {

        }

        private void txtCancel_Clicked(object sender, EventArgs e)
        {

        }
    }
}
using HappeningsApp.Views.AppViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppLanding : ContentPage
	{
		public AppLanding ()
		{
			InitializeComponent ();
            Deals_Tapped(this, null);
		}

        private void Deals_Tapped(object sender, EventArgs e)
        {
            var page =new Deals();
         
            PlaceHolder.Content = page.Content;


        }

        private void ThisWeek_Tapped(object sender, EventArgs e)
        {

        }

        private void Categories_Tapped(object sender, EventArgs e)
        {

        }

        private void Collections_Tapped(object sender, EventArgs e)
        {

        }
    }
}
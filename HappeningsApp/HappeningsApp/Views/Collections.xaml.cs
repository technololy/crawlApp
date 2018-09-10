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
	public partial class Collections : ContentPage
	{
        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new facebooktestpage());
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new LoggedOn());
            //Navigation.PushAsync(new LoggedOn());
        }

		public Collections ()
		{
			InitializeComponent ();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.GreenYellow;

		}
	}
}
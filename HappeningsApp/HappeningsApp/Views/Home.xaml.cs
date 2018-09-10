using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
        IntroPageViewModel introPageViewMod;

		public Home ()
		{
			InitializeComponent ();
            introPageViewMod = new IntroPageViewModel();
            //nearBy = new NearByViewModel();
            BindingContext = introPageViewMod;
		}
	}
}
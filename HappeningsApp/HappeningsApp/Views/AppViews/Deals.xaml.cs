using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.AppViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Deals : ContentPage
	{
        TodaysViewModel todaysViewModel;

        public Deals ()
		{
			InitializeComponent ();
            BindingContext = todaysViewModel = new TodaysViewModel();

		}
	}
}
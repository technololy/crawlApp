using HappeningsApp.ViewModels;
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
	public partial class TodayPage : ContentPage
	{
        TodaysViewModel todaysViewModel;
        public TodayPage ()
		{
			InitializeComponent ();
            BindingContext = todaysViewModel = new TodaysViewModel();

        }
	}
}
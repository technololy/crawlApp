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
	public partial class CategoriesPage : ContentPage
	{
        CategoryViewModel categoryViewModel;
        public CategoriesPage ()
		{
			InitializeComponent ();
            //BindingContext = categoryViewModel = new CategoryViewModel();

        }
	}
}
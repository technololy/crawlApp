using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.LoginSignUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        LoginViewModel lvm;
		public SignUp ()
		{
			InitializeComponent ();
            lvm = new LoginViewModel();
            
            this.BindingContext = lvm;
           
		}

        private void signUp_Clicked(object sender, EventArgs e)
        {
             lvm.Register();
        }

        private void Done_Activated(object sender, EventArgs e)
        {

        }

        private void goBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}
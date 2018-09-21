using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
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

        private async void signUp_Clicked(object sender, EventArgs e)
        {
           var res = await lvm.Register();
            if (res)
            {
             var tkResponse =  await lvm.GetTokenFromAPI();
              
                    //navigate to sign in user
                if (tkResponse)
                {
                     Navigation.PopModalAsync(true);
                    Application.Current.MainPage.Navigation.PushAsync(new AppLanding());



                }
                else
                {
                   await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                }

            }
        }

        private void Done_Activated(object sender, EventArgs e)
        {
            
        }

        private  void goBack_Tapped(object sender, EventArgs e)
        {
             Navigation.PopModalAsync();

        }
    }
}
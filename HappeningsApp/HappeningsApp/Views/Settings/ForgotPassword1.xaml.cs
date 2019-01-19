using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgotPassword1 : ContentPage
	{
		public ForgotPassword1 ()
		{
			InitializeComponent ();
             LogService.LogErrorsNew(activity: "User landed on Forgot Password First Stage");

        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
                {
                    GlobalStaticFields.Username = txtUsername.Text.Trim();
                    var result = await Services.LoginSignUp.LoginSignUp.ForgotPassword(GlobalStaticFields.Username);
                    if (result)
                    {
                        await Navigation.PushAsync(new ForgotPassword2(), true);
                    }

                    else
                    {
                        await DisplayAlert("Info", "Unusual error occured/Email not found", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
                
            }
            
        }
    }
}
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
	public partial class ForgotPassword2 : ContentPage
	{
		public ForgotPassword2 ()
		{
			InitializeComponent ();
		}

        private void txtcode_Completed(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
                {
                    if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                    {
                        await DisplayAlert("Info", "Passwords do not match", "OK");
                        return;
                    }
                    if (txtcode.Text == "" || txtPassword.Text == "" || txtConfirmPassword.Text == "")
                    {
                        await DisplayAlert("Info", "Please enter all fields", "OK");
                        return;
                    }

                    var result = await HappeningsApp.Services.LoginSignUp.LoginSignUp.ResetPassword(GlobalStaticFields.Username, txtcode.Text.Trim(), txtPassword.Text.Trim(), txtConfirmPassword.Text.Trim());
                    if (result)
                    {
                        try
                        {
                            var existingPages = Navigation.NavigationStack.ToList();
                            foreach (var page in existingPages)
                            {
                                Navigation.RemovePage(page);
                            }
                        }
                        catch ( Exception eh) 
                        {

                            LogService.LogErrors(eh.ToString());
                        }
                        await Navigation.PushAsync(new LoggedOn(), true);
                    }

                    else
                    {
                        await DisplayAlert("Info", "Please review and try again", "OK");
                    }
                }
            }
            catch (Exception ex)
            {

                LogService.LogErrors(ex.ToString());
            }
           
        }

     
        void Password_Completed(object sender, System.EventArgs e)
        {
            txtConfirmPassword.Focus();
        }
    }
}
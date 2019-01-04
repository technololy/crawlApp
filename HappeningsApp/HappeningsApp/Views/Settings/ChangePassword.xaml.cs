using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePassword : ContentPage
	{
        ViewModels.LoginViewModel lvvm;
        public string MessageToUser { get; set; }
        public string email { get; set; }
        public string CurPass { get; set; }
        public string NewPass { get; set; }
        public string ConfirmNewPass { get; set; }
        public ChangePassword ()
		{
			InitializeComponent ();
		}

        private void txtUsername_Completed(object sender, EventArgs e)
        {
            var obj = sender as ChangePassword;
            
            txtCurrentPassword.Focus();
        }

        private void txtCurrentPassword_Completed(object sender, EventArgs e)
        {
            var obj = sender as ChangePassword;
            txtNewPassword.Focus();
        }

        private void txtNewPassword_Completed(object sender, EventArgs e)
        {
            var obj = sender as ChangePassword;
            txtConfirmNewPassword.Focus();
        }

        private async void txtConfirmNewPassword_Completed(object sender, EventArgs e)
        {
            try
            {

           
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {


                var obj = sender as ChangePassword;
                    //email = txtUsername.Text.Trim();
                    email = GlobalStaticFields.Username;
                CurPass = txtCurrentPassword.Text.Trim();
                NewPass = txtNewPassword.Text.Trim();
                ConfirmNewPass = txtConfirmNewPassword.Text.Trim();
                lvvm = new ViewModels.LoginViewModel()
                {
                    User = new Models.UserInfo
                    {
                        Username = email,
                        Password = CurPass,
                        NewPassword = NewPass,
                        ConfirmNewPassword = ConfirmNewPass
                    }


                };
                if (IsEntryValid().Result)
                {
                    bool resp = await lvvm.ChangePassword();
                    if (resp)
                    {
                        if (await DisplayAlert("Cool", "Password Changed", "OK", "Home"))
                            await Application.Current.MainPage.Navigation.PopToRootAsync(true);
                        else
                            await Application.Current.MainPage.Navigation.PopToRootAsync(true);

                    }
                }
                else
                {
                    await DisplayAlert("Info", MessageToUser, "OK");
                }
            }
            }
            catch (Exception ex)
            {
                var log = ex;
               await DisplayAlert("Error", "Error occured", "OK");
            }
        }

        private async Task<bool> IsEntryValid()
        {
            if (email==""||CurPass==""||NewPass==""||ConfirmNewPass=="")
            {
                MessageToUser = "Email is blank";
                return false;
            }
            if (NewPass!=ConfirmNewPass)
            {
                MessageToUser = "New Passwords do not match";
                return false;
            }
           
            if (await lvvm.GetTokenFromAPI())
            {
                return true;
            }
            else
            {
                MessageToUser = lvvm.RegisterationError;
                return false;
            }

            //return true;
        }
    }
}
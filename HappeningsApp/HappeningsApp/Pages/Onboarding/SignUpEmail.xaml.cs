using System;
using System.Collections.Generic;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Onboarding
{
    public partial class SignUpEmail : ContentPage
    {
        LoginViewModel lvm;

        public SignUpEmail()
        {
            InitializeComponent();
            lvm = new LoginViewModel();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            this.BindingContext = lvm;


        }

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (!lvm.IsRegisterationDetailsValid())
                {
                    await DisplayAlert("Error", lvm.RegisterationError, "OK");
                    return;
                }
                GlobalStaticFields.Username = lvm.User.Username;
                var res = await lvm.Register();
                if (res)
                {
                    var tkResponse = await lvm.GetTokenFromAPI();

                    //navigate to sign in user
                    if (tkResponse)
                    {
                        lvm.PersistUserDetails();
                        // await Task.Run(() => (GlobalStaticFields.IntroModel = new IntroPageViewModel()));

                        await Application.Current.MainPage.Navigation.PushAsync(new AppLanding());



                    }

                    else
                    {
                        await DisplayAlert("Sorry", "Sign in not successful at this time", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Sorry", lvm.RegisterationError, "OK");

                }
            }
            catch (Exception ex)
            {
                var logs = ex;
                LogService.LogErrors(logs.ToString());
            }
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }

        void Password_Completed(object sender, System.EventArgs e)
        {
            txtConfirmPassword.Focus();
        }
    }
}

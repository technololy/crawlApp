using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views;
using HappeningsApp.Views.Settings;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Xamarin.Auth;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Onboarding
{
    public partial class LoginEmail : ContentPage
    {
        Account account;
        AccountStore store;
        public string accessToken { get; set; }
        public string SocialMedia { get; set; }
        LoginViewModel lvm = new LoginViewModel();


        public LoginEmail()
        {
            InitializeComponent();
            this.BindingContext = lvm;
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }

        void Handle_Completed_1(object sender, System.EventArgs e)
        {
            Logon_Tapped(this, new EventArgs());
        }

        async void Logon_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Hi there", "It appears you have no internet access", "OK");
                    return;
                }
                using (UserDialogs.Instance.Loading(""))
                {
                    //Analytics.TrackEvent("logging on", new Dictionary<string, string> {
                    //    { "User", GlobalStaticFields.Username},
                    //    { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},

                    //   });
                    GlobalStaticFields.Username = lvm.User.Username;
                    var resp = await lvm.GetTokenFromAPI();
                    if (resp)
                    {

                        lvm.PersistUserDetails();
                        //await Navigation.PushAsync(new AppLanding());
                        await Navigation.PushAsync(new AppShell());
                        //Application.Current.MainPage = new AppShell();

                    }
                    else
                    {
                        await DisplayAlert("Error", lvm.RegisterationError + "\n Please try again", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "Error Navigating", "OK");
                var log = ex;
                LogService.LogErrors("Error Navigating" + ex.ToString());
                //Crashes.TrackError(ex, new Dictionary<string, string> {
                //            { "User", GlobalStaticFields.Username },
                //            { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},
                //            { "AppID", "no idea" },
                //             { "someMeaningfulID", "id" }
                //         });
                //test
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPassword1(), true);
        }
    }
}

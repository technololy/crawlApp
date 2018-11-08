using System;
using System.Collections.Generic;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.LoginSignUp
{
    public partial class LoginOrSignUp : ContentPage
    {
        void signup_click(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            Navigation.PushAsync(new SignUp());

        }

        void logon_click(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            Navigation.PushAsync(new LoggedOn());
        }

       LoginViewModel LoginViewModel;

        public LoginOrSignUp()
        {
            InitializeComponent();


            //if (IsUserLoggedOn())
            //{
            //    try
            //    {
            //        LoginViewModel lvmm = new LoginViewModel();
            //        lvmm.User.Username = Application.Current.Properties["username"].ToString();
            //        lvmm.User.EmailAddress = lvmm.User.Username;
            //        lvmm.User.Password = Application.Current.Properties["password"].ToString();
            //        GlobalStaticFields.Username = lvmm.User.EmailAddress;
            //        lvmm.GetTokenFromAPI();

            //        Navigation.PushAsync(new Views.AppLanding());
            //    }
            //    catch (Exception ex)
            //    {
            //        LogService.LogErrorsNew(error: ex.ToString(), activity: "Exception at loginOrSignUp");
            //        Navigation.PushAsync(new Views.LoginSignUp.LoginOrSignUp());


            //    }

            //}
            //else
            //{
            //    Navigation.PushAsync(new Views.LoginSignUp.LoginOrSignUp());

            //}



            LoginViewModel = new ViewModels.LoginViewModel();
            BindingContext = LoginViewModel;
        }



        private bool IsUserLoggedOn()
        {
            bool log = false;
            try
            {
                log = Convert.ToBoolean(Application.Current.Properties["IsUserLoggedOn"]);

            }
            catch (Exception ex)
            {
                var logg = ex;
                // LogService.LogErrorsNew(error:logg.ToString(),activity:"Exception at app.xaml.cs IsUserLoggedOn()");
            }
            return log;

        }
    }
}

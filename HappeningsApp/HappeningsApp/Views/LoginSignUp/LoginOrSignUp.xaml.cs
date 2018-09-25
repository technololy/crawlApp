using System;
using System.Collections.Generic;

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

        ViewModels.LoginViewModel LoginViewModel;
        public LoginOrSignUp()
        {
            InitializeComponent();
            LoginViewModel = new ViewModels.LoginViewModel();
            BindingContext = LoginViewModel;
        }
    }
}

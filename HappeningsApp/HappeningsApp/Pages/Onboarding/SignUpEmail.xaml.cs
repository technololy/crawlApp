using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HappeningsApp.Pages.Onboarding
{
    public partial class SignUpEmail : ContentPage
    {
        public SignUpEmail()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;

        }

        void Submit_Clicked(object sender, System.EventArgs e)
        {
          
        }
    }
}

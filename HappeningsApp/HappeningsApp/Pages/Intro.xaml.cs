using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HappeningsApp.Pages
{
    public partial class Intro : ContentPage
    {
        public List<string> Pictures { get; set; }
        public Intro()
        {
            InitializeComponent();
            Pictures = new List<string> { "ONBOARDING.png", "ONBOARDING2.png", "ONBOARDING3.png", "ONBOARDING4.png", "ONBOARDING5.png" };
            BindingContext = this;
        }

        void start_tapped(object sender, System.EventArgs e)
        {
             Navigation.PushAsync(new Pages.SignInUp());
            //DisplayAlert("Info","i have been pressed","OK");
        }
    }
}

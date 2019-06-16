using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HappeningsApp.Pages
{
    public partial class Intro : ContentPage
    {
        public List<string> Pictures { get; set; }
        int Count = 0;
        short Counter = 0;
        int SlidePosition = 0;
        public Intro()
        {
            InitializeComponent();
            Pictures = new List<string> { "ONBOARDING.png", "ONBOARDING2.png", "ONBOARDING3.png", "ONBOARDING4.png", "ONBOARDING5.png" };
            BindingContext = this;
            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                SlidePosition++;
                if (SlidePosition == Pictures.Count) SlidePosition = 0;
                cv.Position = SlidePosition;
                return true;
            });

        }

        void start_tapped(object sender, System.EventArgs e)
        {
             Navigation.PushAsync(new Pages.SignInUp());
            //DisplayAlert("Info","i have been pressed","OK");
        }

        protected override bool OnBackButtonPressed()
        {


            return true;

            //return base.OnBackButtonPressed();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyTwo : ContentPage
    {
        public SurveyTwo()
        {
            InitializeComponent();
            SportPicker.ItemsSource = new List<string>()
            {
               "Football", "BasketBall","American Football","Tennis"

            };

            OtherInterests.ItemsSource = new List<string>()
            {
               "Fashion and Beauty", "Health and Fitness","Travel","Movies","Music"

            };
        }

        async void  Handle_Clicked(object sender, System.EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                await Task.Delay(3000);
            };

            Application.Current.Properties["SurveyThree"] = true;
            await Navigation.PopModalAsync(true);
            ShowSurVeyThree();

        }
        private async Task ShowSurVeyThree()
        {
            await Task.Delay(30000);
            NowShowThree();
        }


        private async Task NowShowThree()
        {
            try
            {
                if (Convert.ToBoolean(Application.Current.Properties["SurveyThree"]) == true)
                {

                }
                else
                {
                    await Navigation.PushModalAsync(new Survey.SurveyThree());
                }
            }
            catch (Exception ex)
            {
                Application.Current.Properties["SurveyThree"] = false;
            }

        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        void Dismiss_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
            ShowSurVeyThree();

        }

    }
}

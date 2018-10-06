using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyOne : ContentPage
    {
      

        public SurveyOne()
        {
            InitializeComponent();
           

            Location.ItemsSource = new List<string>()
            {
                "Lagos","Port Harcourt","Abuja","Kaduna"
            };
            MaritalPicker.ItemsSource = new List<string>()
            {
               "Single", "Married","Divorced"
            };
            DietPicker.ItemsSource = new List<string>()
            {
                "Vegetarian","Non-vegetarian"
            };
            SmokerPicker.ItemsSource = new List<string>()
            {
                "Yes","No"
            };
            DrinkerPicker.ItemsSource = new List<string>()
            {
                "Yes","No"
            };
            MoreSmokingChoice.ItemsSource = new List<string>()
            {
               "Cigar", "Shisha","Vape"

            };
            MoreDrinkOption.ItemsSource = new List<string>()
            {
               "Wine", "Beer","Whisky"

            };
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                await Task.Delay(3000);
            };
            Application.Current.Properties["SurveyOne"] = true;

            await Navigation.PopModalAsync(true);
            ShowSurVeyTwo();

        }
        private async Task ShowSurVeyTwo()
        {
            await Task.Delay(30000);
            NowShowTwo();

        }
        private async Task NowShowTwo()
        {
            try
            {
                if (Convert.ToBoolean(Application.Current.Properties["SurveyTwo"]) == true)
                {

                }
                else
                {
                    await Navigation.PushModalAsync(new Survey.SurveyTwo());
                }
            }
            catch (Exception ex)
            {
                Application.Current.Properties["SurveyTwo"] = false;
                LogService.LogErrors(ex.ToString());

            }
        }
        void Location_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Location.SelectedIndex==0)
            {
                Location.SelectedIndex = 1;
            }
        }
        void  Dismiss_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
            ShowSurVeyTwo();

        }

        void Smoke_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           if (SmokerPicker.SelectedItem==null)
            {
                return;
            }



            if (SmokerPicker.SelectedItem.ToString()=="Yes")
            {
                MoreSmokingTypeStack.IsVisible = true;
                //LaunchMultiSelector();
            }
            else
            {
                MoreSmokingTypeStack.IsVisible = false;
            }
        }

        void Drink_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DrinkerPicker.SelectedItem == null)
            {
                return;
            }



            if (DrinkerPicker.SelectedItem.ToString() == "Yes")
            {
                DrinkYesStack.IsVisible = true;
                //LaunchMultiSelector();
            }
            else
            {
                DrinkYesStack.IsVisible = false;
            }
        }
    }
}

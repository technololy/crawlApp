using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyTwo : ContentPage
    {
        SurveyViewModel svm = new SurveyViewModel();
        public SurveyTwo()
        {
            InitializeComponent();
            //SportPicker.ItemsSource = new List<string>()
            //{
            //   "Football", "BasketBall","American Football","Tennis"

            //};

            //OtherInterests.ItemsSource = new List<string>()
            //{
            //   "Fashion and Beauty", "Health and Fitness","Travel","Movies","Music"

            //};
        }

        public SurveyTwo(SurveyViewModel survey)
        {
            InitializeComponent();
            svm = survey;
            SportPicker.ItemsSource = svm.SportPickerDS;
            OtherInterests.ItemsSource = svm.OtherInterestDS;
        }


        async void  Handle_Clicked(object sender, System.EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                await Task.Delay(1000);
                svm.surveyModel.Other_Interests = OtherInterests.SelectedItem;
                svm.surveyModel.Favourite_Spot = SportPicker.SelectedItem;

                await Navigation.PopModalAsync(true);
                ShowSurVeyThree();
            };

            Application.Current.Properties["SurveyTwo"] = true;
      

        }
        private async Task ShowSurVeyThree()
        {
           // await Task.Delay(30000);
            NowShowThree();
        }


        private async Task NowShowThree()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new Survey.SurveyThree(svm));

                //if (Convert.ToBoolean(Application.Current.Properties["SurveyThree"]) == true)
                //{

                //}
                //else
                //{
                //    await Navigation.PushModalAsync(new Survey.SurveyThree());
                //}
            }
            catch (Exception ex)
            {
                Application.Current.Properties["SurveyThree"] = false;
                LogService.LogErrors(ex.ToString());

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

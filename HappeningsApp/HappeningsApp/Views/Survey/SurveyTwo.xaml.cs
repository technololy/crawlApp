﻿using HappeningsApp.Services;
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
            HowDidYou.ItemsSource = svm.HowDidYouDS;

        }


        async void  Handle_Clicked(object sender, System.EventArgs e)
        {
            svm.surveyModel.Other_Interests = OtherInterests.SelectedItem;
            svm.surveyModel.Favourite_Spot = SportPicker.SelectedItem;
            svm.surveyModel.How_Did_You_hear = HowDidYou.SelectedItem;
            if (!CheckValidity())
            {
                await DisplayAlert("Info", "Please enter all fields", "OK");
                return;
            }
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                await Task.Delay(1000);
              

                await LogService.LogErrorsNew(activity: "User clicked on submit on Survey two");
                SurveyService.SubmitSurvey(svm);

                await Navigation.PopModalAsync(true);
                //ShowSurVeyThree();
            };

            Application.Current.Properties["SurveyTwo"] = true;
      

        }

        private bool CheckValidity()
        {
            return !string.IsNullOrEmpty(svm.surveyModel.Other_Interests) && !string.IsNullOrEmpty(svm.surveyModel.Favourite_Spot) && !string.IsNullOrEmpty(svm.surveyModel.How_Did_You_hear)
;
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
            await LogService.LogErrorsNew(activity: "User clicked on previous button on Survey two");

             Navigation.PopModalAsync(true);
             Navigation.PushModalAsync(new SurveyOne(), true);
        }

       async void Dismiss_Clicked(object sender, System.EventArgs e)
        {
            //await LogService.LogErrorsNew(activity: "User clicked on dismiss on Survey two");

           await Navigation.PopModalAsync(true);
            //ShowSurVeyThree();

        }

    }
}

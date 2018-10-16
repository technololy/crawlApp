﻿using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyThree : ContentPage
    {
        SurveyViewModel svm = new SurveyViewModel();

        public SurveyThree()
        {
            InitializeComponent();
            //HowDidYou.ItemsSource = new List<string>()
            //{
            //    "Facebook", "Twitter","Instagram","Google","Blogs"

            //};
        }

        public SurveyThree(SurveyViewModel survey)
        {
            InitializeComponent();
            svm = survey;
            HowDidYou.ItemsSource = svm.HowDidYouDS;
        }

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                svm.surveyModel.How_Did_You_hear = HowDidYou.SelectedItem;
                SurveyService.SubmitSurvey(svm);
                await Task.Delay(3000);
            };
            Application.Current.Properties["SurveyThree"] = true;
            await Navigation.PopModalAsync(true);
        }

     

        void Dismiss_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}

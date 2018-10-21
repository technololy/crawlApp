using HappeningsApp.Services;
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
            if (!CheckValidity())
            {
                await DisplayAlert("Info", "Please enter all fields", "OK");
                return;
            }
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                svm.surveyModel.How_Did_You_hear = HowDidYou.SelectedItem;
                await LogService.LogErrorsNew(activity: "User clicked on submit on Survey 3");

                SurveyService.SubmitSurvey(svm);
                await Task.Delay(3000);
            };
            Application.Current.Properties["SurveyThree"] = true;
            await Navigation.PopModalAsync(true);
        }

        private bool CheckValidity()
        {
            if
                (
                    string.IsNullOrEmpty(svm.surveyModel.How_Did_You_hear)
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

       async void Dismiss_Clicked(object sender, System.EventArgs e)
        {
            await LogService.LogErrorsNew(activity: "User clicked on dismiss on Survey three");

           await Navigation.PopModalAsync(true);
        }
    }
}

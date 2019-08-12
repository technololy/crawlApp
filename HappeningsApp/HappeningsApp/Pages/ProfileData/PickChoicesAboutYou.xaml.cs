using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.ProfileData
{
    public partial class PickChoicesAboutYou : ContentPage
    {
        private SurveyViewModel svm;
        public PickChoicesAboutYou()
        {
            InitializeComponent();
            svm = new SurveyViewModel();
            selectionView.ItemsSource = svm.Choices_Interest;
         
            BindingContext = svm;
        }
        public PickChoicesAboutYou(SurveyViewModel svmm)
        {
            InitializeComponent();
            svm = new SurveyViewModel();

            svm = svmm;
            selectionView.ItemsSource = svm.Choices_Interest;
            BindingContext = svmm;


        }

        async void Skip_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);

        }
        async void submit_Clicked(object sender, System.EventArgs e)
        {
            var seleted = selectionView.SelectedItems;
            svm.surveyModel.Other_Interests = Newtonsoft.Json.JsonConvert.SerializeObject(seleted);

             //Navigation.PopModalAsync(true);
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

    }

  
}

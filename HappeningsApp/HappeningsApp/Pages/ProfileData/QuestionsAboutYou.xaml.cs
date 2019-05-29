using System;
using System.Collections.Generic;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.ProfileData
{
    public partial class QuestionsAboutYou : ContentPage
    {
        private SurveyViewModel svm;

        public QuestionsAboutYou()
        {
            InitializeComponent();
            svm = new SurveyViewModel();
            this.BindingContext = svm;
        
          
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            drpCity.ItemsSource = svm.LocationDS;
            DietPicker.ItemsSource = svm.DietDS;
            FavSports.ItemsSource = svm.SportPickerDS;
            drpHearAboutUs.ItemsSource = svm.MoreSmokingDS;
            drpHearAboutUs.ItemsSource = svm.HowDidYouDS;
        

        }

        async void Skip_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);

        }


        void submit_Clicked(object sender, System.EventArgs e)
        {
            svm.surveyModel.UserName = GlobalStaticFields.Username;

            svm.surveyModel.City = drpCity.SelectedItem; 
            svm.surveyModel.Marital_Status = Marital_Status.SelectedItem.ToString();
            //svm.surveyModel.Smoker = SmokerPicker.SelectedItem;
            //svm.surveyModel.Smoking_Preference = string.IsNullOrEmpty(MoreSmokingChoice.SelectedItem) ? "n/a" : MoreSmokingChoice.SelectedItem;
            svm.surveyModel.User_Id = GlobalStaticFields.Username;
            svm.surveyModel.Drinker = string.IsNullOrEmpty(DrinkerPicker.SelectedItem.ToString()) ? "n/a" : DrinkerPicker.SelectedItem.ToString();
            //svm.surveyModel.Drinking_Preference = string.IsNullOrEmpty(MoreDrinkOption.SelectedItem) ? "n/a" : MoreDrinkOption.SelectedItem;
            if (!CheckValidity())
            {
                 DisplayAlert("Info", "Please enter all fields", "OK");
                return;

            }
        }


        private bool CheckValidity()
        {
         
            if
                (string.IsNullOrEmpty(svm.surveyModel.Marital_Status) ||
                 string.IsNullOrEmpty(svm.surveyModel.Smoker) ||
                 string.IsNullOrEmpty(svm.surveyModel.Drinker)
                 || string.IsNullOrEmpty(svm.surveyModel.City) ||
                 string.IsNullOrEmpty(svm.surveyModel.SelectedLocation)
                )
            {
                return false;
            }
            else if (svm.surveyModel.Smoker.ToLower() == "yes" && string.IsNullOrEmpty(svm.surveyModel.Smoking_Preference))
            {
                return false;
            }
            else if (svm.surveyModel.Drinker.ToLower() == "yes" && string.IsNullOrEmpty(svm.surveyModel.Drinking_Preference))
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}

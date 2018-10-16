using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyOne : ContentPage
    {

        SurveyViewModel svm ;
        public SurveyOne()
        {
            InitializeComponent();

            svm = new SurveyViewModel();
           // BindingContext = new SurveyViewModel();
            this.BindingContext = this.svm;
            Location.ItemsSource = svm.LocationDS;
            MaritalPicker.ItemsSource = svm.MaritalDS;
            DietPicker.ItemsSource = svm.DietDS;
            SmokerPicker.ItemsSource = svm.SmokerDS;
            MoreSmokingChoice.ItemsSource = svm.MoreSmokingDS;
            DrinkerPicker.ItemsSource = svm.DrinkerDS;
            MoreDrinkOption.ItemsSource = svm.MoreDrinkDS;
            //Location.ItemsSource = new List<string>()
            //{
            //    "Lagos","Port Harcourt","Abuja","Kaduna"
            //};
            
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
           //svm.SelectedLocation= Location.SelectedItem;
          

            //svm.SubmitSurveyOne();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
                await Task.Delay(3000);
                svm.surveyModel.Marital_Status = MaritalPicker.SelectedItem;
                svm.surveyModel.Smoker = SmokerPicker.SelectedItem;
                svm.surveyModel.Smoking_Preference = MoreSmokingChoice.SelectedItem;
                svm.surveyModel.User_Id = GlobalStaticFields.Username;
                svm.surveyModel.Drinker = DrinkerPicker.SelectedItem;
                svm.surveyModel.Drinking_Preference = MoreDrinkOption.SelectedItem;
                svm.surveyModel.City = Location.SelectedItem;
                svm.surveyModel.SelectedLocation = Location.SelectedItem;

            };
            Application.Current.Properties["SurveyOne"] = true;

            await Navigation.PopModalAsync(true);
            await Application.Current.MainPage.Navigation.PushModalAsync(new Survey.SurveyTwo(svm),true);

            await ShowSurVeyTwo();

        }
        private async Task ShowSurVeyTwo()
        {
            //await Task.Delay(30000);
         await  NowShowTwo();

        }
        private async Task NowShowTwo()
        {
            try
            {
                await Navigation.PushModalAsync(new Survey.SurveyTwo(svm));

                //if (Convert.ToBoolean(Application.Current.Properties["SurveyTwo"]) == true)
                //{

                //}
                //else
                //{
                //    await Navigation.PushModalAsync(new Survey.SurveyTwo());
                //}
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

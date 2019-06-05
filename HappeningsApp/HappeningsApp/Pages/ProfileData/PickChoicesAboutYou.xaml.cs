using System;
using System.Collections.Generic;
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
            selectionView.ItemsSource = svmm.Choices_Interest;
            BindingContext = svmm;


        }

        async void Skip_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);

        }
        void submit_Clicked(object sender, System.EventArgs e)
        {
            var seleted = selectionView.SelectedItems;
            var seleted2 = selectionView.SelectedItem;
             Navigation.PopModalAsync(true);

        }

    }

  
}

using System;
using System.Collections.Generic;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.ProfileData
{
    public partial class PickChoicesAboutYou : ContentPage
    {
        public PickChoicesAboutYou()
        {
            InitializeComponent();
            BindingContext = new SurveyViewModel();
        }
    }
}

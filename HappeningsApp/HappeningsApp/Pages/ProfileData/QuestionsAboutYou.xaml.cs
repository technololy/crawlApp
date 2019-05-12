using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HappeningsApp.Pages.ProfileData
{
    public partial class QuestionsAboutYou : ContentPage
    {
        public QuestionsAboutYou()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;

        }

        async void Skip_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);

        }
    }
}

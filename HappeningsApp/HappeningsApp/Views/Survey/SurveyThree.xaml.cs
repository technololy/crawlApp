using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappeningsApp.Views.Survey
{
    public partial class SurveyThree : ContentPage
    {
        public SurveyThree()
        {
            InitializeComponent();
            HowDidYou.ItemsSource = new List<string>()
            {
                "Facebook", "Twitter","Instagram","Google","Blogs"

            };
        }

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading(""))
            {
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

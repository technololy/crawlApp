using System;
using System.Collections.Generic;
using System.Windows.Input;
using HappeningsApp.Models;
using Xamarin.Forms;

namespace HappeningsApp.ViewModels
{
    public class SurveyViewModel: BaseViewModel
    {
        public SurveyModel surveyModel{get;set;}
        public ICommand SubmitFirstSurvey { get; set; }
        public SurveyViewModel()
        {
            surveyModel = new SurveyModel();
            SubmitFirstSurvey = new Command(_submitFirstSurvey);
        }

        private void _submitFirstSurvey()
        {
           
        }

        public List<string> LocationDS = new List<string>()
            {
                "Lagos","Port Harcourt","Abuja","Kaduna"
            };
        public List<string> SportPickerDS = new List<string>()
            {
               "Football", "BasketBall","American Football","Tennis"

            };

        public List<string> OtherInterestDS = new List<string>()
            {
               "Fashion and Beauty", "Health and Fitness","Travel","Movies","Music"

            };

        public List<string> MaritalDS = new List<string>()
            {
               "Single", "Not Single"
            };
        public List<string> DietDS = new List<string>()
            {
                "Vegetarian","Non-vegetarian"
            };
        public List<string>  SmokerDS = new List<string>()
            {
                "Yes","No"
            };
        public List<string>  DrinkerDS = new List<string>()
            {
                "Yes","No"
            };
        public List<string> MoreSmokingDS = new List<string>()
            {
               "Cigarette","Cigar", "Shisha","Vape","Others"

            };
        public List<string>  MoreDrinkDS = new List<string>()
            {
               "Wine", "Beer","Whisky","Spirit","Champagne"

            };
        public List<string> HowDidYouDS = new List<string>()
            {
                "Facebook", "Twitter","Instagram","Google","Blogs"

            };


        public List<string> Choices_Interest = new List<string>()
            {
                "Surfing", "Play Pool","Reading","Quiet Spots","Clubbing"

            };
        internal void SubmitSurveyOne()
        {
            string a = "g";
        }
    }
}

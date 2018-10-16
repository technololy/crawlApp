using System;
using System.Collections.Generic;
using HappeningsApp.Models;

namespace HappeningsApp.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
        }

        public SurveyModel surveyModel
        {
            get;
            set;
        }

        
        public List<string> Location = new List<string>()
            {
                "Lagos","Port Harcourt","Abuja","Kaduna"
            };
        //public string SelectedLocation
        //{
        //    get;
        //    set;
        //}
        //public string MaritalStatus
        //{
        //    get;
        //    set;
        //}
        //public string DietaryChoice
        //{
        //    get;
        //    set;
        //}
        //public bool Smoker
        //{
        //    get;
        //    set;
        //}
        //public string SmokingChoice
        //{
        //    get;
        //    set;
        //}
        //public bool Drinker
        //{
        //    get;
        //    set;
        //}
        //public string DrinkingChoice
        //{
        //    get;
        //    set;
        //}

        internal void SubmitSurveyOne()
        {
            string a = "g";
        }
    }
}

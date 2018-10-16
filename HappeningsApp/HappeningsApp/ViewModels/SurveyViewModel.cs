using HappeningsApp.Models;
using System;
using System.Collections.Generic;

namespace HappeningsApp.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyModel surveyModel { get; set; }
        public List<string> Location = new List<string>()
            {
                "Lagos","Port Harcourt","Abuja","Kaduna"
            };
        public SurveyViewModel()
        {
            surveyModel = new SurveyModel();
        }

     
        public string SelectedLocation
        {
            get;
            set;
        }

        internal void SubmitSurveyOne()
        {
            string a = "g";
        }
    }
}

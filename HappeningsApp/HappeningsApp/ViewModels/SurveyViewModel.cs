using System;
using System.Collections.Generic;

namespace HappeningsApp.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
        }
        public List<string> Location = new List<string>()
            {
                "Lagos","Port Harcourt","Abuja","Kaduna"
            };
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

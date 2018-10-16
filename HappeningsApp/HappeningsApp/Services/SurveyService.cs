using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Services
{
   public class SurveyService
    {
        public async static void SubmitSurvey(SurveyViewModel svm)
        {
            await APIService.PostNew<SurveyModel>(svm.surveyModel, "api/survey");
        }
    }
}

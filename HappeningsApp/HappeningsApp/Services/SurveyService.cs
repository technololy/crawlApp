using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HappeningsApp.Services
{
   public class SurveyService
    {
        public async static void SubmitSurvey(SurveyViewModel svm)
        {
          var respo =  await APIService.PostNew<SurveyModel>(svm.surveyModel, "api/survey");

            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();

                if (content.ToLower().Contains("success"))
                {
                    Application.Current.Properties["DidSurveySubmitOk"] = true;

                }
                else
                {
                    Application.Current.Properties["DidSurveySubmitOk"] = false;

                }




            }
            else
            {
                Application.Current.Properties["DidSurveySubmitOk"] = false;

            }

        }
    }
}

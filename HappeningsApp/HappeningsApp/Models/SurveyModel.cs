using System;
namespace HappeningsApp.Models
{
    public class SurveyModel
    {
      
       
            public string User_Id { get; set; }
            public string UserName { get; set; }
            public string City { get; set; }
            public string Marital_Status { get; set; }
            public string Smoker { get; set; }
            public string Smoking_Preference { get; set; }
            public string Drinker { get; set; }
            public string Drinking_Preference { get; set; }
            public string Favourite_Spot { get; set; }
            public string Other_Interests { get; set; }
            public string How_Did_You_hear { get; set; }
            public DateTime Created { get; set; }

    }
}

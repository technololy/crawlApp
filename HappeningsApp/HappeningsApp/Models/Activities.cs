using System;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{
  


        public class Activities
        {


            public int Id { get; set; }
            public string Name { get; set; }
            public string type { get; set; }
            public string Details { get; set; }
            public string Category { get; set; }
            public int Owner_Id { get; set; }
            public string Owner_Location{get;set;}
            public string Owner { get; set; }
            public DateTime Expiration_Date { get; set; }
            public string ImagePath { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
            public string User_Id { get; set; }
            public string CategoryId { get; set; }



        }

        public class ActivitiesRootObject
        {
            public string Message { get; set; }
            public ObservableCollection<Activities> ObvActivities { get; set; }
        }

}

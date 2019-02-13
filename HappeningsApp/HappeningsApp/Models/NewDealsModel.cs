using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{
    public class NewDealsModel
    {
        public class Picture
        {
            public string picture { get; set; }
        }

        public class Hostopeninghours
        {
            public string Days { get; set; }
            public string OpeningTime { get; set; }
            public string ClosingTime { get; set; }
        }

        public class Deal
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Details { get; set; }
            public int Category_Id { get; set; }
            public string Category { get; set; }
            public int Owner_Id { get; set; }
            public string Owner { get; set; }
            public string Owner_Location { get; set; }
            public string OpeningHours { get; set; }
            public DateTime Expiration_Date { get; set; }
            public string ImagePath { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
            public ObservableCollection<Picture> Pictures { get; set; }
            public Hostopeninghours Hostopeninghours { get; set; }
            public object HostCategories { get; set; }
            public List<DaysOpen> DaysOpen { get; set; }

        }



        public class DaysOpen
        {
            public string Day { get; set; }
            public string Time { get; set; }
        }

        public class RootObject
        {
            public string Message { get; set; }
            public ObservableCollection<Deal> Deals { get; set; }
        }
    }
}

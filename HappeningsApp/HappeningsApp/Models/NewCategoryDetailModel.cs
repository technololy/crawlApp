using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{
    public class NewCategoryDetailModel
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

        public class HostCategory
        {
            public string Category { get; set; }
        }

        public class Deal
        {
            public string type { get; set; }
            public string Id { get; set; }
            public string User_Id { get; set; }
            public string Name { get; set; }
            public string Details { get; set; }
            public string Coordinates { get; set; }
            public string CategoryId { get; set; }
            public string Category { get; set; }
            public string Owner_Id { get; set; }
            public string Owner { get; set; }
            public string Owner_Location { get; set; }
            public string Status { get; set; }
            public string OpeningHours { get; set; }
            public string Expiration_Date { get; set; }
            public string ImagePath { get; set; }
            public ObservableCollection<Picture> Pictures { get; set; }
            public Hostopeninghours Hostopeninghours { get; set; }
            public ObservableCollection<HostCategory> HostCategories { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
        }

        public class RootObject
        {
            public string Message { get; set; }
            public ObservableCollection<Deal> Deals { get; set; }
        }
    }
}

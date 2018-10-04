using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{
    public class Activity
    {
        public string type { get; set; }
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Coordinates { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int Owner_Id { get; set; }
        public string Owner { get; set; }
        public string Owner_Location { get; set; }
        public string Status { get; set; }
        public DateTime Expiration_Date { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class Activity_RootObject
    {
        public string Message { get; set; }
        public ObservableCollection<Activity> Activities { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{


    public class Deals
    {
    

        public int Id { get; set; }
        public string Name { get; set; }
        // public string type { get; set; }
        public string Category_Id { get; set; }
        public string Details { get; set; }
        public string Category { get; set; }
        public int Owner_Id { get; set; }
        public string Owner_Location
        {
            get;
            set;
        }
        public string Owner { get; set; }
        public DateTime Expiration_Date { get; set; }
        public string ImagePath { get; set; }
        public List<Picture> Pictures { get; set; }
        public Hostopeninghours Hostopeninghours { get; set; }
        public object HostCategories { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string User_Id { get; set; }
        public string CategoryId { get; set; }
        public string OpeningHours
        {
            get;
            set;
        }

        //public string OpeningHoursDate
        //{
        //    get
        //    {
        //        return OpeningHours == null ? "10-10-2100" : OpeningHours.Date.ToString("dd-MM-yyyy");
        //    }

        //}

        //public string OpeningTime
        //{
        //    get
        //    {
        //        return OpeningHours == null? "12:00:00": OpeningHours.TimeOfDay.ToString();
        //    }
            
        //}

       
    }

    public class DealRootObject
    {
        public string Message { get; set; }
        public ObservableCollection<Deals> Deals { get; set; }
    }

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
}

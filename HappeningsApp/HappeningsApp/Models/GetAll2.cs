using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Models
{
    public class GetAll2
    {

        //public class GetAll
        //{
        //    public string Message { get; set; }
        //    public List<Deal> Deals { get; set; }
        //}

        //public class Deal
        //{
        //    public string type { get; set; }
        //    public string Id { get; set; }
        //    public object User_Id { get; set; }
        //    public string Name { get; set; }
        //    public string Details { get; set; }
        //    public object Coordinates { get; set; }
        //    public string CategoryId { get; set; }
        //    public string Category { get; set; }
        //    public string Owner_Id { get; set; }
        //    public string Owner { get; set; }
        //    public string Owner_Location { get; set; }
        //    public string Status { get; set; }
        //    public DateTime Expiration_Date { get; set; }
        //    public string ImagePath { get; set; }
        //    public DateTime Created { get; set; }
        //    public DateTime Modified { get; set; }

        //    public string OpeningHours { get; set; }

        //    public List<Picture> Pictures { get; set; }
        //    public Hostopeninghours Hostopeninghours { get; set; }
        //    public List<HostCategory> HostCategories { get; set; }


        //}

        //public class Picture
        //{
        //    public string picture { get; set; }
        //}

        //public class Hostopeninghours
        //{
        //    public string Days { get; set; }
        //    public string OpeningTime { get; set; }
        //    public string ClosingTime { get; set; }
        //}

        //public class HostCategory
        //{
        //    public string Category { get; set; }
        //}
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
            public string type { get; set; }
            public string Id { get; set; }
            public object User_Id { get; set; }
            public string Name { get; set; }
            public string Details { get; set; }
            public object Coordinates { get; set; }
            public string CategoryId { get; set; }
            public string Category { get; set; }
            public string Owner_Id { get; set; }
            public string Owner { get; set; }
            public string Owner_Location { get; set; }
            public string Status { get; set; }
            public object OpeningHours { get; set; }
            public DateTime Expiration_Date { get; set; }
            public string ImagePath { get; set; }
            public List<Picture> Pictures { get; set; }
            public Hostopeninghours Hostopeninghours { get; set; }
            public object HostCategories { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
        }

        public class GetAll
        {
            public string Message { get; set; }
            public List<Deal> Deals { get; set; }
        }

    }
}

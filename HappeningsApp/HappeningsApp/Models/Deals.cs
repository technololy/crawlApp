using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{


    public class Deals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Category { get; set; }
        public int Owner_Id { get; set; }
        public string Owner { get; set; }
        public DateTime Expiration_Date { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class DealRootObject
    {
        public string Message { get; set; }
        public ObservableCollection<Deals> Deals { get; set; }
    }
}

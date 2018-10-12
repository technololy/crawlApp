using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Models
{
    public class CollectionsRootObject
    {
        public string Message { get; set; }
        public List<Collections> collections { get; set; }
    }

    public class Collections
    {
        public string User_id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public List<Details> Details { get; set; }
    }



    public class Details
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }


}

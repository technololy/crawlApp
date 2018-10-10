using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Models
{
    public class CollectionsModel
    {
        public string User_id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public List<Favourite> Details { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Modified { get; set; }
    }


    public class CollectionsModelResp
    {
        public string Message { get; set; }
        public List<CollectionsResp> Collections { get; set; }
    }
    public class CollectionsResp
    {
        public int Id { get; set; }
        public string User_id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public List<Favourite> Details { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Modified { get; set; }
    }

    public class Favourite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}

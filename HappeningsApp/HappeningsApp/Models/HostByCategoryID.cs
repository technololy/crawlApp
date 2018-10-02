using System;
using System.Collections.Generic;

namespace HappeningsApp.Models
{
    public class HostByCategoryID
    {
        public class Host
        {
            public int Id { get; set; }
            public string User_Id { get; set; }
            public string Host_Name { get; set; }
            public string Address { get; set; }
            public string Coordinates { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public string Picture { get; set; }
            public string Status { get; set; }
            public DateTime Date_Registered { get; set; }
            public DateTime Date_Modified { get; set; }
        }

        public class HostRootObject
        {
            public string Message { get; set; }
            public List<Host> Hosts { get; set; }
        }
    }
}

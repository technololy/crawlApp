using System;
using System.Collections.Generic;

namespace HappeningsApp.Models
{
    public class MyMessages
    {
        public string Message { get; set; }
        public List<Pushresp> Pushresp { get; set; }
    }

   

    public class Pushresp
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        public string Createdby { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}

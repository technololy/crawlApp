using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Models
{
  public  class CollectionsRootObject
    {
        public string Message { get; set; }
        public List<Collections> collections { get; set; }
    }

    public class Collections
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}

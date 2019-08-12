using System;
using System.Collections.Generic;

namespace HappeningsApp.Models
{
    public class OpeningDaysList
    {
        public IEnumerable<string> OpeningWeekDays { get; set; }
        public string OpeningClosingTime { get; set; }
     
    }
}

using System;
using System.Collections.Generic;

namespace HappeningsApp.Models
{
    public class RegisterationResponse
    {

       
		public class ModelState
        {
            public List<string> Responses { get; set; }
        }

        public class RootObject
        {
            public string Message { get; set; }
            //public ModelState ModelState { get; set; }
            public Dictionary<string, ModelState> modelstate { get; set; }
        }
    }
}

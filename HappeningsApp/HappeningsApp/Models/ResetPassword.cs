using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Models
{
    public class ResetPassword
    {        
            public string Code { get; set; }
            public string ConfirmPassword { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }      

    }
}

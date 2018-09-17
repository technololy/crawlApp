using System;
using System.Collections.Generic;
using System.Text;

namespace HappeningsApp.Services
{
    public static class Constants
    {
        public  const string FacebookOAuthURL= "https://www.facebook.com/v3.1/dialog/oauth";
        public const string fbClientID = "1752084648235245";
        public const string redirectURI = "https://www.facebook.com/connect/login_success.html";
        public const string graphAPI = "https://graph.facebook.com/v3.0/me/?fields=name,picture,work,website,religion,location,age_range,email,first_name,last_name,gender,hometown";
    }
}

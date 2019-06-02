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
        public const string graphAPIs = "https://graph.facebook.com/v3.1/me?fields=id,name,email,first_name,last_name,middle_name";
        //public const string CrawlAPI = "http://crawlapi.azurewebsites.net/";
        public const string CrawlAPI = "http://Crawlwebapi.azurewebsites.net/";



        public static string AppName = "Crawl";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "34982577863-toktu2f2edt4hva673f1n1jce6mlt207.apps.googleusercontent.com";
        //public static string AndroidClientId = "<insert Android client ID here>";
        public static string AndroidClientId = "34982577863-i5gcivpseeomlnksdcpnglfc0g20v7v8.apps.googleusercontent.com";
        //public static string AndroidClientId = "com.googleusercontent.apps.300639273565-176fm4vcielnggf5jtbkecoqhn7p8d0l";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.34982577863-toktu2f2edt4hva673f1n1jce6mlt207:/oauth2redirect";
        //public static string AndroidRedirectUrl = "<insert Android redirect URL here>:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.34982577863-i5gcivpseeomlnksdcpnglfc0g20v7v8:/oauth2redirect";
        //34982577863-i5gcivpseeomlnksdcpnglfc0g20v7v8.apps.googleusercontent.com



    }
}

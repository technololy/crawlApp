using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services.LoginSignUp
{
    public class LoginSignUp
    {
        public LoginSignUp()
        {
        }
        
        internal async static Task<Dictionary<bool,string>> RegisterLocal(Registeration reg)
        {
            string result = "";
            Dictionary<bool, string> keyValuePairs = new Dictionary<bool, string>();

            try
            {
                result = await APIService.RegisterLocal(reg);
               // var testresult = await APIService.RegisterLocalNew(reg);
                //var testCont = await testresult.Content.ReadAsStringAsync();
                //result = result.Replace("","Responses");
                //var content = JsonConvert.DeserializeObject<RegisterationResponse.RootObject>(result);
                var dict = new Dictionary<bool, string>() { };

               // var message = content.
                if (result.ToLower().Contains("invalid"))
                {
                    //dict.Add(false, content.Message);
                    dict.Add(false, "Error");
                }
                else if(result.ToLower().Contains("successful"))
                {
                    dict.Add(true, "Success");

                }
                return dict;

            }
            catch (Exception ex)
            {
                var log = ex;
                return keyValuePairs;

            }




            //return keyValuePairs;
        }




    }
}

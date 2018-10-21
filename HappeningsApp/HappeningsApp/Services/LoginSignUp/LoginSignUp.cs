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
               var testresult = await APIService.RegisterLocalNew(reg);
                var testCont = await testresult.Content.ReadAsStringAsync();
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



        internal async static Task<Models.RegisterationResponse.RootObject> RegisterLoco(Registeration reg)
        {
            RegisterationResponse.RootObject registResp = new RegisterationResponse.RootObject();
            string content = "";
            try
            {
                var testresult = await APIService.RegisterLocalNew(reg);
                content = await testresult.Content.ReadAsStringAsync();
                if (content.ToLower().Contains("success"))
                {
                    registResp.Message = "success";
                }
                else
                {
                    registResp = JsonConvert.DeserializeObject<RegisterationResponse.RootObject>
                                       (content);
                }
               
               

               
                return registResp;

            }
            catch (Exception ex)
            {
                var log = ex;
                return registResp ;

            }




        }

        internal async static Task<bool> ChangePassword(ChangePassword reg)
        {
            RegisterationResponse.RootObject registResp = new RegisterationResponse.RootObject();
            string content = "";
            try
            {

                var testresult = await APIService.PostNew<ChangePassword>(reg,"");
                content = await testresult.Content.ReadAsStringAsync();
                if (content.ToLower().Contains("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }




               // return registResp;

            }
            catch (Exception ex)
            {
                var log = ex;
                return false;

            }




        }

        internal async static Task<bool> ForgotPassword(string username)
        {
            string content = "";
            try
            {
                var forgot = new ForgotPassword() { Email = username };
                var testresult = await APIService.PostNew<ForgotPassword>(forgot, "/api/Account/ForgotPassword");
                content = await testresult.Content.ReadAsStringAsync();
                if (content.ToLower().Contains("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }




                // return registResp;

            }
            catch (Exception ex)
            {
                var log = ex;
                return false;

            }




        }

        internal async static Task<bool> ResetPassword(string username,string code, string pass, string confirmPass)
        {
            string content = "";
            try
            {
                var reset = new ResetPassword() { Email = username, Code = code, Password=pass,ConfirmPassword = confirmPass };
                var testresult = await APIService.PostNew<ResetPassword>(reset, "");
                content = await testresult.Content.ReadAsStringAsync();
                if (content.ToLower().Contains("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }




                // return registResp;

            }
            catch (Exception ex)
            {
                var log = ex;
                return false;

            }




        }
    }
}

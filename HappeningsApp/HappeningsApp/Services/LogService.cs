using System;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Plugin.Connectivity;

namespace HappeningsApp.Services
{
    public class LogService
    {
        public LogService()
        {
        }

        public async static void LogErrors(string error)
        {
            //var logger = new LogModel()
            //{
            //    User = GlobalStaticFields.Username,
            //    Error = error
            //};
           // await APIService.LogClient<LogModel>(logger, "api/Error");

            //commented this on 15 may 2019  so that i can work offline
            //var l=await  Task.Run(async()=> await APIService.LogAsync(error));
            //var cont = await l.Content.ReadAsStringAsync();
        }

        public async static Task LogErrorsNew(string error="",string request="",
                                              string response="", string url="",
                                              string activity="")
        {
           
            var errorx = new Errors()
            {
                Error = error,
                Response  = response,
                Request = request,
                 User = (string.IsNullOrEmpty(GlobalStaticFields.Username) ? "NA" : GlobalStaticFields.Username),
                URL = url,
                Activity = activity
            
            };
            if (CrossConnectivity.Current.IsConnected)
            {
                var l = await Task.Run(() => APIService.LogNewAsync(errorx));
                try
                {
                    var cont = await l.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    var log = ex;
                }
            }
        
        }
    }
}

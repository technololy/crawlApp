using System;
using HappeningsApp.Models;

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
        var l=    await APIService.LogAsync(error);
            var cont = await l.Content.ReadAsStringAsync();
        }

        public async static void LogErrorsNew(string error,string request, string response)
        {
            //var logger = new LogModel()
            //{
            //    User = GlobalStaticFields.Username,
            //    Error = error
            //};
            var errorx = new Errors()
            {
                Error = error,
                Response  = response,
                Request = request,
                 User = (string.IsNullOrEmpty(GlobalStaticFields.Username) ? "NA" : GlobalStaticFields.Username)
            
            };
            var l = await APIService.LogNewAsync(errorx);
            var cont = await l.Content.ReadAsStringAsync();
        }
    }
}

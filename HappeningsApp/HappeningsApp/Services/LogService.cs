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
            var logger = new LogModel()
            {
                User = GlobalStaticFields.Username,
                Error = error
            };
            await APIService.LogClient<LogModel>(logger, "api/Error");
        }
    }
}

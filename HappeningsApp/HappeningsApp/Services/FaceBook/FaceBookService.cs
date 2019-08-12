using System;
using System.Net.Http;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services.FaceBook
{
    public class FaceBookService
    {
        public FaceBookService()
        {
        }

        public async Task<FaceBookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = $"{Constants.graphAPIs}&access_token={accessToken}";
            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);
            //LogService.LogErrors(userJson);

            var facebookProfile = JsonConvert.DeserializeObject<FaceBookProfile>(userJson);
            LogFaceBook(facebookProfile);
            return facebookProfile;

        }

        private static void LogFaceBook(FaceBookProfile facebookProfile)
        {
            if (facebookProfile != null)
            {
                LogService.LogErrors("Successfully pulled facebook details for " + facebookProfile?.Email);

            }
            else
            {
                LogService.LogErrors("failed to pulled facebook details");

            }
        }
    }
}

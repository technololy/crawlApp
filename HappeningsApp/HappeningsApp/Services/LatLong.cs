using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HappeningsApp.Services
{
    public class LatLong
    {
        public LatLong()
        {
        }

        public async Task<string> ReturnUberDeepLinkNonPrecise(string address)
        {
            string MapsAddress = null;
            string UberDeepLink = "";
            List<Uber> longlat = new List<Uber>();
            Uber uber = new Uber();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //first break it down by comma
                PermuteAgain pa = new PermuteAgain();
                var commaAddress = address.Split(',');
               var possiblePermute= pa.CreateSubsets(commaAddress);
                foreach (var item in possiblePermute)
                {

                    if (item.Contains(commaAddress[0]))
                    {
                        MapsAddress = item[0];
                       var lt =await CalcLongLat(MapsAddress);
                        if (!string.IsNullOrEmpty(lt.Longitude.ToString()) && !string.IsNullOrEmpty(lt.Latitude.ToString()))
                        {

                          UberDeepLink=  uber.GetUberDeepLink(lt.Longitude, lt.Latitude, address);
                            break;
                        }
                    }
                }
                //var p = possiblePermute.Where(r => r.FirstOrDefault().Contains(commaAddress[0]));

            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                MapsAddress = HttpUtility.UrlEncode(address);
                var lt = await CalcLongLat(MapsAddress);
                if (!string.IsNullOrEmpty(lt.Longitude.ToString()) && !string.IsNullOrEmpty(lt.Latitude.ToString()))
                {

                    UberDeepLink = uber.GetUberDeepLink(lt.Longitude, lt.Latitude, MapsAddress);
                    
                }

            }

            else
            {

            }

         


            return UberDeepLink;

        }


        public async Task<Uber> CalcLongLat(string address)
        {
            List<Uber> longlat = new List<Uber>();
            Uber uber = new Uber();
            var loc = await Xamarin.Essentials.Geocoding.GetLocationsAsync(address).ConfigureAwait(false);
            var point = loc?.FirstOrDefault();
            if (point != null)
            {
               uber.Longitude  = point.Longitude;
                uber.Latitude = point.Latitude;
            }


            return uber;
        }
    }
}

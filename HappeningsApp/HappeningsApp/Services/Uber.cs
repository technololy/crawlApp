using System;
namespace HappeningsApp.Services
{
    public class Uber
    {
        public Uber()
        {
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        public string GetUberDeepLink(double longi, double lat, string MapsAddress, string name="")
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                MapsAddress = "";
            }
            return "https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]="
            + lat + "&dropoff[longitude]=" + longi + "&dropoff[nickname]="
                + MapsAddress + "&dropoff" +
        "[formatted_address]=" + MapsAddress + "&link_text=View%20team%20roster&partner_deeplink" +
        "=partner%3A%2F%2Fteam%2F9383\n";
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class DealsService
    {
        public DealsService()
        {
        }

        public async Task<ObservableCollection<Deals>> GetDeals()
        {
            DealRootObject deal = new DealRootObject();
            ObservableCollection<Deals> listofdeals = new ObservableCollection<Deals>();
            ObservableCollection<DealRootObject> deallist = new ObservableCollection<DealRootObject>();

            var respo = await APIService.Get("api/deals");
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();

                deal = JsonConvert.DeserializeObject<DealRootObject>(content);
                if (deal.Message.Contains("Success"))
                {
                    listofdeals = deal.Deals;
                }



            }
            else
            {
                var content = await respo.Content.ReadAsStringAsync();

            }
            return listofdeals;
        }
    }
}

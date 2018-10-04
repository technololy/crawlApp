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

        public async Task<ObservableCollection<Deals>> GetDeals(string id="")
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


        public async Task<ObservableCollection<Activity>> GetAllByCategoryID(int id =0)
        {
            Activity_RootObject actv = new Activity_RootObject();
            ObservableCollection<Activity> actvList = new ObservableCollection<Activity>();
            ObservableCollection<Activity_RootObject> ActvRootList = new ObservableCollection<Activity_RootObject>();

            var respo =  APIService.Get($"api/all/getallbycategoryid?id={id}").Result;
            if (respo.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await respo.Content.ReadAsStringAsync();

                actv = JsonConvert.DeserializeObject<Activity_RootObject>(content);
                if (actv.Message.Contains("Success"))
                {
                    //ActvRootList = JsonConvert.DeserializeObject<ObservableCollection<Activity_RootObject>>(content);

                    actvList = actv.Activities;
                }
              



            }
            else
            {
                var content = await respo.Content.ReadAsStringAsync();

            }
            return actvList;
        }

    }
}

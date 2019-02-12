using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class GetAllService
    {
        internal async Task<ObservableCollection<NewDealsModel.Deal>> GetAll2()
        {
            ObservableCollection<NewDealsModel.Deal> every = new ObservableCollection<NewDealsModel.Deal>();
            var resp = await APIService.Get("api/All/GetAll2");
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cont = await resp.Content.ReadAsStringAsync();
                var EveryThing = JsonConvert.DeserializeObject<NewDealsModel.RootObject>(cont);
                if (EveryThing.Message.ToLower().Contains("success"))
                {
                   every = EveryThing.Deals;
                }
                else
                {

                }
            }
            else
            {
                var cont = await resp.Content.ReadAsStringAsync();

            }

            return every;
        }

        internal async Task<ObservableCollection<NewDealsModel.Deal>> GetAllSearch()
        {
            ObservableCollection<NewDealsModel.Deal> search = new ObservableCollection<NewDealsModel.Deal>();
            var resp = await APIService.PostNew<NewDealsModel.Deal>(null, "api/All/Search");
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cont = await resp.Content.ReadAsStringAsync();
                var AllSearch = JsonConvert.DeserializeObject<NewDealsModel.RootObject>(cont);
                if (AllSearch.Message.ToLower().Contains("success"))
                {
                    search = AllSearch.Deals;
                }
                else
                {

                }
            }
            else
            {
                var cont = await resp.Content.ReadAsStringAsync();

            }

            return search;
        }



        internal async Task<ObservableCollection<NewDealsModel.Deal>> GetThisWeek()
        {
            ObservableCollection<NewDealsModel.Deal> week = new ObservableCollection<NewDealsModel.Deal>();
            var resp = await APIService.Get("api/All/GetThisWeek");
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cont = await resp.Content.ReadAsStringAsync();
                var ThisWeek = JsonConvert.DeserializeObject<NewDealsModel.RootObject>(cont);
                if (ThisWeek.Message.ToLower().Contains("success"))
                {
                    week = ThisWeek.Deals;
                }
                else
                {

                }
            }
            else
            {
                var cont = await resp.Content.ReadAsStringAsync();

            }

            return week;
        }


        internal async Task<List<GetAll2.Deal>> GetAll2Original()
        {
            var gtAll = new GetAll2.GetAll();
            var allDeal = new List<GetAll2.Deal>();
            var resp = await APIService.Get("api/All/GetAll2");
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cont = await resp.Content.ReadAsStringAsync();
                var all = JsonConvert.DeserializeObject<GetAll2.GetAll>(cont);
                if (all.Message.ToLower().Contains("success"))
                {
                    allDeal = all.Deals;
                }
                else
                {

                }
            }
            else
            {
                var cont = await resp.Content.ReadAsStringAsync();

            }

            return allDeal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class GetAllService
    {
        internal async Task<List<GetAll2.Deal>> GetAll2()
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

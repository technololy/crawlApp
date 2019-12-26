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

        public async Task<ObservableCollection<NewDealsModel.Deal>> GetDeals(string id = "")
        {

            ObservableCollection<NewDealsModel.Deal> listOfAllDeals = new ObservableCollection<NewDealsModel.Deal>();

            try
            {
                var respo = await APIService.Get("api/deals");
                if (respo.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await respo.Content.ReadAsStringAsync();

                    var dealls = JsonConvert.DeserializeObject<NewDealsModel.RootObject>(content);

                    //deal = JsonConvert.DeserializeObject<DealRootObject>(content);
                    if (dealls.Message.ToLower().Contains("success"))
                    {
                        listOfAllDeals = dealls.Deals;
                    }



                }
                else
                {
                    var content = await respo.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            return listOfAllDeals;
        }
        public async Task<ObservableCollection<Deals>> GetDealsOriginal(string id = "")
        {
            DealRootObject deal = new DealRootObject();
            ObservableCollection<Deals> listofdeals = new ObservableCollection<Deals>();
            ObservableCollection<DealRootObject> deallist = new ObservableCollection<DealRootObject>();
            try
            {

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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            return listofdeals;
        }


        public async Task<ObservableCollection<Activity>> GetAllByCategoryID(int id = 0)
        {
            Activity_RootObject actv = new Activity_RootObject();
            ObservableCollection<Activity> actvList = new ObservableCollection<Activity>();
            ObservableCollection<Activity_RootObject> ActvRootList = new ObservableCollection<Activity_RootObject>();

            try
            {
                var respo = await APIService.Get($"api/all/getallbycategoryid?id={id}");
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            return actvList;
        }


        public async Task<ObservableCollection<NewCategoryDetailModel.Deal>> GetAllByCategoryID2(int id = 0)
        {
            ObservableCollection<NewCategoryDetailModel.Deal> deals = new ObservableCollection<NewCategoryDetailModel.Deal>();
            DealRootObject dealRootObject = new DealRootObject();
            try
            {
                var respo = await APIService.Get($"api/all/getallbycategoryid2?id={id}");
                if (respo.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await respo.Content.ReadAsStringAsync();

                    var categoryDetail = JsonConvert.DeserializeObject<NewCategoryDetailModel.RootObject>(content);
                    if (categoryDetail.Message.Contains("Success"))
                    {
                        //ActvRootList = JsonConvert.DeserializeObject<ObservableCollection<Activity_RootObject>>(content);

                        deals = categoryDetail.Deals;
                    }




                }
                else
                {
                    var content = await respo.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            return deals;
        }

        public async Task<ObservableCollection<Deals>> GetAllByCategoryID2Original(int id = 0)
        {
            ObservableCollection<Deals> deals = new ObservableCollection<Deals>();
            DealRootObject dealRootObject = new DealRootObject();
            try
            {
                var respo = await APIService.Get($"api/all/getallbycategoryid2?id={id}");
                if (respo.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await respo.Content.ReadAsStringAsync();

                    dealRootObject = JsonConvert.DeserializeObject<DealRootObject>(content);
                    if (dealRootObject.Message.Contains("Success"))
                    {
                        //ActvRootList = JsonConvert.DeserializeObject<ObservableCollection<Activity_RootObject>>(content);

                        deals = dealRootObject.Deals;
                    }




                }
                else
                {
                    var content = await respo.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            return deals;
        }
    }
}

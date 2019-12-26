using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    public class CollectionService
    {
        public async Task<ObservableCollection<Models.FavoriteModel>> GetUserFavs()
        {
            try
            {
                /**/
                string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
                //string endpoint = "api/GetByUserID?User_id=" + GlobalStaticFields.Username;
                string endpoint = "GetByUserID?User_id=" + testuser;
                var response = await APIService.Get(endpoint);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model1 = JsonConvert.DeserializeObject<Models.CollectionsModelResp>(content);
                    var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                    return model;
                }

                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var empty = new ObservableCollection<Models.FavoriteModel>();
                    return empty;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ObservableCollection<FavoriteModel>();

            }
        }


        public async Task<Models.CollectionsModelResp> GetUserFavsNew()
        {
            try
            {
                /**/        //string testuser = "07d1b055-ae7a-43aa-b7a7-f44fb84ede02";
                string endpoint = "/GetByUserID?User_id=" + GlobalStaticFields.Username;
                //string endpoint = "GetByUserID?User_id=" + testuser;
                var response = await APIService.Get(endpoint);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Models.CollectionsModelResp>(content);
                    //var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                    return model;
                }

                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var empty = new Models.CollectionsModelResp();
                    return empty;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new CollectionsModelResp();

            }
        }

        internal async Task<bool> CreateNewCollection(string newCollectionName, string newCollectionNick)
        {
            try
            {
                /**/
                var newCollection = new Collections
                {
                    User_id = GlobalStaticFields.Username,
                    Name = newCollectionName,
                    NickName = newCollectionNick
                };
                var resp = await APIService.PostNew<Collections>(newCollection, "api/Collections");
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var cont = await resp.Content.ReadAsStringAsync();
                    if (cont.Contains("Record Added Successfully"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;

            }

        }




        internal async Task<bool> DeleteCollection(CollectionsResp coll)
        {
            try
            {

                string endpoint = "api/Collections?id=" + coll.Id;

                var resp = await APIService.Delete(endpoint);
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var cont = await resp.Content.ReadAsStringAsync();
                    if (cont.ToLower().Contains("success"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;

            }

        }




        internal async Task<ObservableCollection<CollectionsResp>> GetUserListCollection()
        {
            ObservableCollection<CollectionsResp> Col = new ObservableCollection<CollectionsResp>();
            try
            {
                /**/
                string endpoint = "/GetByUserID?User_id=" + GlobalStaticFields.Username;
                var response = await APIService.Get(endpoint, "Get Users Collection List");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<CollectionsModelResp>(content);
                    if (model.Message.ToLower().Contains("success"))
                    {
                        Col = model.Collections;
                    }
                    //var model = JsonConvert.DeserializeObject<ObservableCollection<Models.FavoriteModel>>(content);
                    return Col;
                }

                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var empty = new ObservableCollection<CollectionsResp>();
                    return empty;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ObservableCollection<CollectionsResp>();
            }
        }

        internal async Task<bool> UpdateCollectionWithDetails(CollectionsResp ctl)
        {
            bool response = false;
            try
            {
                string endpoint = $"api/Collections?User_Id={ctl.User_id}&collection_id={ctl.Id}";
                var add = await APIService.Put<CollectionsResp>(ctl, endpoint);
                if (add.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var content = await add.Content.ReadAsStringAsync();
                    if (content.ToLower().Contains("success"))
                        response = true;
                }

                else
                {
                    var content = await add.Content.ReadAsStringAsync();

                    response = false;
                }
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
                response = false;
            }

            return response;
        }



        internal async Task<bool> DeleteCollectionWithDetails(Favourite ctl, string CategoryID)
        {
            bool response = false;
            try
            {
                string endpoint = $"DeleteFavorite?id={CategoryID}";
                var add = await APIService.PostNew<Favourite>(ctl, endpoint);
                if (add.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var content = await add.Content.ReadAsStringAsync();
                    if (content.ToLower().Contains("success"))
                        response = true;
                }

                else
                {
                    var content = await add.Content.ReadAsStringAsync();

                    response = false;
                }
            }
            catch (Exception ex)
            {
                LogService.LogErrors(ex.ToString());
                response = false;
            }

            return response;
        }


    }


}

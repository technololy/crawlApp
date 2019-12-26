using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HappeningsApp.Models;
using Newtonsoft.Json;

namespace HappeningsApp.Services
{
    internal class CategoriesService
    {
        internal async Task<ObservableCollection<Category>> GetCategories()
        {
            Category c = new Category();
            ObservableCollection<Category> ct = new ObservableCollection<Category>();
            try
            {
                var resp = await APIService.Get("api/category");
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var cont = await resp.Content.ReadAsStringAsync();
                    var catObj = JsonConvert.DeserializeObject<CategoryRootobject>(cont);
                    if (catObj.Message.ToLower().Contains("success"))
                    {
                        ct = catObj.categories;
                    }
                    else
                    {

                    }
                }
                else
                {
                    var cont = await resp.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return ct;

        }
    }
}
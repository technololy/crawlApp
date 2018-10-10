using HappeningsApp.Models;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using HappeningsApp.Views.AppViews;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppLanding : ContentPage
	{
        IntroPageViewModel ivm;
        public ObservableCollection<Grouping<string, Models.Deals>> GroupedDeals = new ObservableCollection<Grouping<string, Models.Deals>>();
        public ObservableCollection<Grouping<string, GetAll2.Deal>> GetAllGrouped = new ObservableCollection<Grouping<string, GetAll2.Deal>>();
       public AppLanding ()
		{
			InitializeComponent ();
            ivm = new IntroPageViewModel();
            ivm.GetDeals();
            ivm.GetCategories();
            ivm.GetAll();
            Deals_Tapped(this, null);
            ShowSurVeyOne();
         
          
		}

    

   

        private async Task ShowSurVeyOne()
        {
          await Task.Delay(30000);
           await NowShowOne();
        }

        private async Task NowShowOne()
        {
            try
            {
                if (Convert.ToBoolean(Application.Current.Properties["SurveyOne"]) == true)
                {

                }
                else
                {
                    await Navigation.PushModalAsync(new Survey.SurveyOne());
                }
            }
            catch (Exception ex)
            {
                var log = ex;
                Application.Current.Properties["SurveyOne"] = false;
            }
        }

  

        private void Deals_Tapped(object sender, EventArgs e)
        {
            var page =new DealsList();

            page.Content.BackgroundColor = Color.FromHex("#000015");
            PlaceHolder.Content = page.Content;
            //lblDeals.TextColor = Color.Magenta;
            //lblThisWeek.TextColor = Color.White;
            //lblCategories.TextColor = Color.White;
            //lblCollections.TextColor = Color.White;
            bxVwDeals.BackgroundColor = Color.FromHex("#3498db");

            bxVwCat.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;
     
            BindingContext = GlobalStaticFields.dealsfromAPI ;

        }

     
        private void ThisWeek_Tapped(object sender, EventArgs e)
        {
            var page = new ThisWeek();
            PlaceHolder.Content = page.Content;
            bxVwthisWeek.BackgroundColor = Color.FromHex("#3498db");
            bxVwCat.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwDeals.BackgroundColor = Color.Black;
        
            var groupByDate = GroupListByDate();
            GlobalStaticFields.GetAllGrouping = groupByDate;
            BindingContext = groupByDate;
        }

        private ObservableCollection<Grouping<string, GetAll2.Deal>> GroupListByDate()
        {
            var grp = from h in ivm.getAll
                    orderby h.Expiration_Date
                    group h by h.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                    select new Grouping<string, GetAll2.Deal>(ThisWeeksGroup.Key, ThisWeeksGroup);

            foreach (var g in grp)
            {
                GetAllGrouped.Add(g);
            }
            return GetAllGrouped;
        }


        private ObservableCollection<Grouping<string, Models.Deals>> GroupListByDateBkUp()
        {
            var grp = from h in ivm.dealsfromAPI
                      orderby h.Expiration_Date
                      group h by h.Expiration_Date.DayOfWeek.ToString() into ThisWeeksGroup
                      select new Grouping<string, Models.Deals>(ThisWeeksGroup.Key, ThisWeeksGroup);

            foreach (var g in grp)
            {
                GroupedDeals.Add(g);
            }
            return GroupedDeals;
        }
        private void Categories_Tapped(object sender, EventArgs e)
        {
            var page = new CategoriesPage();
            page.Content.BackgroundColor = Color.FromHex("#000015");

            PlaceHolder.Content = page.Content;
       
            bxVwCat.BackgroundColor = Color.FromHex("#3498db");
            bxVwDeals.BackgroundColor = Color.Black;
            bxVwCol.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;
            BindingContext = ivm;

        }

        private void Collections_Tapped(object sender, EventArgs e)
        {
            var page = new Collections();
            page.Content.BackgroundColor = Color.FromHex("#000015");

            PlaceHolder.Content = page.Content;
            bxVwCol.BackgroundColor = Color.FromHex("#3498db");
            bxVwCat.BackgroundColor = Color.Black;
            bxVwDeals.BackgroundColor = Color.Black;
            bxVwthisWeek.BackgroundColor = Color.Black;
            BindingContext = ivm;
          

        }

        protected  override  bool OnBackButtonPressed()
        {
            return true;
        }

        
        void settings_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Settings.SettingsPage(),true);
        }
    }
}
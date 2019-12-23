using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckWhereToNavigateTo();
        }

        private async void CheckWhereToNavigateTo()
        {
            if (IsUserLoggedOn())
            {
                try
                {
                    LoginViewModel lvmm = new LoginViewModel();
                    lvmm.User.Username = Utility.GetFromAppSettings("username");
                    lvmm.User.EmailAddress = lvmm.User.Username;
                    lvmm.User.Password = Utility.GetFromAppSettings("password");
                    GlobalStaticFields.Username = lvmm.User.EmailAddress;
                    await lvmm.GetTokenFromAPI();

                    await Shell.Current.GoToAsync("//main");

                }
                catch (Exception)
                {

                    //await Shell.Current.GoToAsync("//intro");
                    await Navigation.PushAsync(new Pages.Intro(), true);


                }

            }
            else
            {
                //await Shell.Current.GoToAsync("//intro");
                await Navigation.PushAsync(new Pages.Intro(), true);



            }
        }

        private bool IsUserLoggedOn()
        {
            bool log = false;
            try
            {
                log = Convert.ToBoolean(Application.Current.Properties["IsUserLoggedOn"]);

            }
            catch (Exception ex)
            {
                var logg = ex;
            }
            return log;

        }
    }
}
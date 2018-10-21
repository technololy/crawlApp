using HappeningsApp.Services;
using HappeningsApp.Views.LoginSignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ExtendedLabel_ItemTapped(object sender, Custom.ExtendedLabelTappedEvent e)
        {
            var text = e.Text;

            switch (text)
            {
                case "Log Out":
                    await LogService.LogErrorsNew(activity: "User clicked Log Out");

                    Application.Current.Properties["username"] = null;
                    Application.Current.Properties["password"] = null;
                    var pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    var before = Navigation.NavigationStack.ToList()[0];

                    Navigation.InsertPageBefore(new LoginOrSignUp(),before);
                    GlobalStaticFields.Username = "";
                    await Navigation.PopToRootAsync(true);
                    break;
                case "Change Password":
                    await LogService.LogErrorsNew(activity: "User click change password");

                    await Navigation.PushAsync(new Settings.ChangePassword(),true);
                    break;

                case "Found a bug?":
                    await LogService.LogErrorsNew(activity: "User click Found a Bug");

                   // await Navigation.PushAsync(new Settings.ChangePassword(), true);
                    break;
                case "Found a place?":
                    await LogService.LogErrorsNew(activity: "User click Found a place");

                    // await Navigation.PushAsync(new Settings.ChangePassword(), true);
                    break;
                case "About Crawl":
                    await LogService.LogErrorsNew(activity: "User click About Crawl");

                    // await Navigation.PushAsync(new Settings.ChangePassword(), true);
                    break;
            }
        }

    }
}
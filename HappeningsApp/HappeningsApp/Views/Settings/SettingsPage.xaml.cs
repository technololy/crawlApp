using HappeningsApp.Services;
using HappeningsApp.Views.LoginSignUp;
using Microsoft.AppCenter.Analytics;
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
                    try
                    {
                       // await LogService.LogErrorsNew(activity: "User clicked Log Out");

                        Application.Current.Properties["username"] = null;
                        Application.Current.Properties["password"] = null;
                        var pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        var before = Navigation.NavigationStack.ToList()[0];

                        Navigation.InsertPageBefore(new LoginOrSignUp(), before);
                        GlobalStaticFields.Username = "";
                        await Navigation.PopToRootAsync(true);
                    }
                    catch (Exception ex)
                    {
                        await LogService.LogErrorsNew( activity: "User clicked Log Out and got error", error: ex.ToString());

                        await Navigation.PopToRootAsync(true);

                    }
                    break;
                case "Change Password":
                    //await LogService.LogErrorsNew(activity: "User click change password").ConfigureAwait(true);
                    Analytics.TrackEvent("user changed password", new Dictionary<string, string> {
                        { "User", GlobalStaticFields.Username },
                        { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},
                        { "AppID", GlobalStaticFields.InstallID.ToString() }
                       });
                    await Navigation.PushAsync(new Settings.ChangePassword(),true);
                    break;

                case "Found a bug? Suggestions":
                    //await LogService.LogErrorsNew(activity: "User click Found a Bug");
                    var url = new Uri("mailto:segunaina@gmail.com?subject=Found a bug");
                    Device.OpenUri(url);
                    break;
                case "Found a place?":
                    //await LogService.LogErrorsNew(activity: "User click Found a place");
                    var uri =new Uri("mailto:segunaina@gmail.com?subject=Found a Place");
                    Device.OpenUri(uri);

                    // await Navigation.PushAsync(new Settings.ChangePassword(), true);
                    break;
                case "About Crawl":
                    //await LogService.LogErrorsNew(activity: "User click About Crawl");

                    // await Navigation.PushAsync(new Settings.ChangePassword(), true);
                    break;
            }
        }

    }
}
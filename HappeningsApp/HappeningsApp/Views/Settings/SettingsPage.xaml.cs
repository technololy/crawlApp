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
                    Application.Current.Properties["username"] = null;
                    Application.Current.Properties["password"] = null;
                    var pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    var before = Navigation.NavigationStack.ToList()[0];
                    Navigation.InsertPageBefore(new LoginOrSignUp(),before);
                    await Navigation.PopToRootAsync(true);
                    break;
                case "Change Password":
                    await Navigation.PushAsync(new Settings.ChangePassword(),true);
                    break;
            }
        }

    }
}
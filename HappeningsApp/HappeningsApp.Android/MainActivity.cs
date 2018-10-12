using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DLToolkit.Forms.Controls;
using FFImageLoading.Forms.Droid;
using Plugin.GoogleAnalytics;

namespace HappeningsApp.Droid
{
    [Activity(Label = "HappeningsApp", Icon = "@drawable/Crawl_white_blue", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);

            FlowListView.Init();
            Acr.UserDialogs.UserDialogs.Init(this);
            //google analytics
            GoogleAnalytics.Current.Config.TrackingId = "UA-XXXXXXXX-2";
            GoogleAnalytics.Current.Config.AppId = "GASample";
            GoogleAnalytics.Current.Config.AppName = "Google Analytics Sample";
            GoogleAnalytics.Current.Config.AppVersion = "1.0.0.0";
            GoogleAnalytics.Current.InitTracker();
            LoadApplication(new App());
        }
    }
}


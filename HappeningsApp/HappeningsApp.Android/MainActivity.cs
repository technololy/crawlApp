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
using Plugin.CurrentActivity;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Svg.Forms;

namespace HappeningsApp.Droid
{
    [Activity(Label = "Crawl", Icon = "@drawable/crawl_lettera_box_black", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //CrossCurrentActivity.Current.Activity.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code
                                                                        //...
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CarouselViewRenderer.Init();
            var ignore = typeof(SvgCachedImage);
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


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}


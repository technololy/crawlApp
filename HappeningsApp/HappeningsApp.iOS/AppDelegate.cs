using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace HappeningsApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            //UITabBar.Appearance.SelectedImageTintColor = UIColor.Magenta;

            //UINavigationBar.Appearance.BarTintColor = UIColor.Red;
            //UINavigationBar.Appearance.TintColor = UIColor.Green;
            //UITabBar.Appearance.TintColor = new UIColor(90 / 255f, 36 / 255f, 191 / 255f, 1.0f);
            //UITabBar.Appearance.BarTintColor = new UIColor(245 / 255f, 246 / 255f, 247 / 255f, 1.0f);
            //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            //{
            //    TextColor = new UIColor(112 / 255f, 112 / 255f, 112 / 255f, 1.0f)
            //});
            CachedImageRenderer.Init();
            LoadApplication(new App());
            //UINavigationBar.Appearance.BarTintColor = UIColor.Red;
            //UINavigationBar.Appearance.TintColor = UIColor.Green;
           // UINavigationBar.Appearance.BarTintColor = new UIColor(245 / 255f, 246 / 255f, 247 / 255f, 1.0f);
           // UINavigationBar.Appearance.TintColor = new UIColor(90 / 255f, 36 / 255f, 191 / 255f, 1.0f);
            return base.FinishedLaunching(app, options);
        }
    }
}

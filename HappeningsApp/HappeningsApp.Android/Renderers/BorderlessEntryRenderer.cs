using System;
using Android.Content;
using HappeningsApp.Custom;
using HappeningsApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer)) ]
namespace HappeningsApp.Droid.Renderers
{
    public class BorderlessEntryRenderer: EntryRenderer
    {
       

        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }


    }
}

using System;
using HappeningsApp.Custom;
using HappeningsApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer)) ]
namespace HappeningsApp.Droid.Renderers
{
    public class BorderlessEntryRenderer: EntryRenderer
    {
        public BorderlessEntryRenderer()
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
